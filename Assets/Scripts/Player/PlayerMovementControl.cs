using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] private LayerMask groundLayer;
    private BoxCollider2D boxCollider;
    [SerializeField] private float moveSpeed;  // can set player movement speed
    [SerializeField] private float jump;       // can set player jump height
    [SerializeField] private float coyoteTime; // can set coyote time (allow jump how many seconds after leaving a platform)
    [SerializeField] private float bufferTime; // can set jump buffer time (allow jump how many seconds before touching the ground)
    private float coyoteTimeCounter;
    private float bufferTimeCounter;
    private float moveHorizontal;       // take horizontal movement input from keyboard
    private float moveVertical;         // take vertical movement input from keyboard

    private Transform currentPlatform;
    private Vector3 previousPlatformPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();  // storing rigidbody component in a variable
        animator = gameObject.GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (rb == null || animator == null || boxCollider == null)
        {
            Debug.LogError("Missing component in PlayerMovementControl");
        }
    }

    // Update is called once per frame
    void Update()
    {

        moveHorizontal = Input.GetAxisRaw("Horizontal");  // taking input from the keyboard (-1f is for left and 1f is for right)
        moveVertical = Input.GetAxisRaw("Vertical");      // taking input from the keyboard (-1f is for down and 1f is for up)

        if (isGrounded())  // calculation for coyote time
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (moveVertical > 0f)   // calculation for buffer time
        {
            bufferTimeCounter = bufferTime;
        }
        else
        {
            bufferTimeCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && bufferTimeCounter > 0f)  // implementing jump
        {
            rb.velocity = new Vector2(rb.velocity.x, jump); // rb.velocity.x maintains the x-axis velocity rigidbody is currently travelling at
            coyoteTimeCounter = 0f;
            bufferTimeCounter = 0f;
        }

        if (currentPlatform != null)
        {
            Vector3 platformMovement = currentPlatform.transform.position - previousPlatformPosition;
            transform.position += platformMovement;
            previousPlatformPosition = currentPlatform.transform.position;
        }

        animator.SetBool("RunSimple", moveHorizontal != 0f && isGrounded()); //run animation
        animator.SetBool("Grounded", isGrounded()); //jump animation

        if (moveHorizontal != 0f)
        {
            rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y); // rb.velocity.y maintains the y-axis velocity rigidbody is currently travelling at

            //flipping the player sprite
            if (moveHorizontal > 0.01f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (moveHorizontal < -0.01f)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
        else if (isGrounded())
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }


    }

    private void FixedUpdate()
    {
        if (currentPlatform != null)
        {
            Vector3 platformMovement = currentPlatform.transform.position - previousPlatformPosition;
            transform.position += platformMovement;
            previousPlatformPosition = currentPlatform.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MovingPlatform"))
        {
            currentPlatform = collision.transform;
            previousPlatformPosition = currentPlatform.position;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MovingPlatform"))
        {
            currentPlatform = null;
        }
    }
    public bool isGrounded()
    {
        if (boxCollider == null)
        {
            Debug.LogWarning("BoxCollider2D is not attached");
            return false;
        }
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.2f, groundLayer);
        return raycastHit.collider != null;
    }
    public Vector2 FacingDirection
    {
        get
        {
            //positive scale of x means facing right and negative means facing left
            return transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        }
    }
}
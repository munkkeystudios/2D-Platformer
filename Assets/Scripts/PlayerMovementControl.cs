using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControl : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed;  // can set player movement speed
    [SerializeField] private float jump;       // can set player jump height
    [SerializeField] private float coyoteTime; // can set coyote time (allow jump how many seconds after leaving a platform)
    [SerializeField] private float bufferTime; // can set jump buffer time (allow jump how many seconds before touching the ground)
    private float coyoteTimeCounter;
    private float bufferTimeCounter;
    private bool isGrounded;            // to know if the player is grounded, useful to disable double jump
    private float moveHorizontal;       // take horizontal movement input from keyboard
    private float moveVertical;         // take vertical movement input from keyboard

    private MovingHorizontalPlatform currentPlatform;
    private Vector3 previousPlatformPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();  // storing rigidbody component in a variable
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");  // taking input from the keyboard (-1f is for left and 1f is for right)
        moveVertical = Input.GetAxisRaw("Vertical");      // taking input from the keyboard (-1f is for down and 1f is for up)

        if (isGrounded)  // calculation for coyote time
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

        if (moveHorizontal != 0f)
        {
            rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y); // rb.velocity.y maintains the y-axis velocity rigidbody is currently travelling at
        }
    }

    private void FixedUpdate()
    {
        // Handle platform movement
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
            currentPlatform = collision.GetComponent<MovingHorizontalPlatform>();
            previousPlatformPosition = currentPlatform.transform.position;
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MovingPlatform"))
        {
            currentPlatform = null;
            isGrounded = false;
        }
    }
}

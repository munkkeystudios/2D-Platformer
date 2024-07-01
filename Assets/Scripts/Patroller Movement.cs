using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.UIElements;

public class PatrollerMovement : MonoBehaviour
{
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    [SerializeField] private float speed;
    [SerializeField] private float distanceFromPlayer = 1.0f;
    private Rigidbody2D rb;
    private Transform currentpoint;

    Animator anim;

    bool facingRight = false;

    bool idle = false;

    [SerializeField] private GameObject player;
    [SerializeField] private float chaseSpeed;
    private float distance;

    private bool chase = false;

    void Start()
    {
        Flip();
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on " + gameObject.name);
        }

        if (pointB == null)
        {
            Debug.LogError("PointB is not assigned on " + gameObject.name);
        }
        else
        {
            currentpoint = pointB.transform;
        }

        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogError("Player GameObject is not assigned.");
            return;
        }

        Vector2 tempA = transform.position - player.transform.position;
        distance = Vector2.SqrMagnitude(tempA);

        if (currentpoint == null)
        {
            Debug.LogError("Current point is not assigned.");
            return;
        }

        if (currentpoint == pointB.transform)
        {
            if (idle)
            {
                Debug.Log("HERE");
                anim?.SetBool("Idle", false);
            }
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            if (idle)
            {
                Debug.Log("HERE");
                anim?.SetBool("Idle", false);
            }
            rb.velocity = new Vector2(-speed, 0);
        }

        chase = false;
        if (distance < 500000f)
        {
            if (player.transform.position.x > pointB.transform.position.x && player.transform.position.x < pointA.transform.position.x)
            {
                if (distance > distanceFromPlayer)
                {
                    chase = true;
                    Vector2 newPosition = new Vector2(player.transform.position.x, this.transform.position.y);
                    transform.position = Vector2.MoveTowards(this.transform.position, newPosition, chaseSpeed * Time.deltaTime);
                }
                else
                {
                    chase = true;
                    anim?.SetBool("Idle", true);
                    idle = true;
                    rb.velocity = new Vector2(0, 0);
                }
            }
        }

        HandleFlip();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Patroller Point"))
        {
            currentpoint = currentpoint == pointB.transform ? pointA.transform : pointB.transform;
        }
    }

    void Flip()
    {
        Vector3 currScale = gameObject.transform.localScale;
        currScale.x *= -1;
        gameObject.transform.localScale = currScale;

        facingRight = !facingRight;
    }

    void HandleFlip()
    {
        if (chase)
        {
            if (player.transform.position.x < transform.position.x && facingRight)
            {
                Debug.Log("flip1");
                Flip();
            }
            else if (player.transform.position.x > transform.position.x && !facingRight)
            {
                Debug.Log("flip2");
                Flip();
            }
        }
        else
        {
            if (currentpoint == pointB.transform && !facingRight)
            {
                Debug.Log("flip3");
                Flip();
            }
            else if (currentpoint == pointA.transform && facingRight)
            {
                Debug.Log("flip4");
                Flip();
            }
        }
    }
}

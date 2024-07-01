using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladder_movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isClimbing;
    private float verticalInput;

    [SerializeField] private float climbSpeed = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on this object");
        }
    }

    private void Update()
    {
        verticalInput = Input.GetAxisRaw("Vertical");

        if (isClimbing)
        {
            rb.velocity = new Vector2(rb.velocity.x, verticalInput * climbSpeed);
            rb.gravityScale = 0; // Disable gravity while climbing
        }
        else
        {
            rb.gravityScale = 4; // Restore gravity when not climbing
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = false;
        }
    }
}

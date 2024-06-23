using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Windows;

public class PlayerMovementControl : MonoBehaviour
{

    private Rigidbody2D rb;

    private float moveSpeed = 0.5f;
    private float jump = 5f;
    private bool isGrounded = true;
    private float moveHorizontal;
    private float moveVertical;


     //Start is called before the first frame update
    void Start()
    {
       rb = gameObject.GetComponent<Rigidbody2D>();
    }
    
     //Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
    }

     void FixedUpdate()
    {
        if(moveHorizontal > 0f || moveHorizontal < 0f)
        {
            rb.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }

       if (moveVertical > 0f && isGrounded)
        {
            rb.AddForce(new Vector2(0f, moveVertical * jump), ForceMode2D.Impulse);
        }
    }
     void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }
    }

   void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = false;
        }
    }
    
}

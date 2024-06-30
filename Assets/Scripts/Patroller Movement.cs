using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.UIElements;
//using static UnityEngine.RuleTile.TilingRuleOutput;

public class PatrollerMovement : MonoBehaviour
{
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    [SerializeField] private float speed;
    [SerializeField] private float distanceFromPlayer = 1.0f;
    private Rigidbody2D rb;
    private Transform currentpoint;

    bool facingRight = false;



    [SerializeField] private GameObject player;
    [SerializeField] private float chaseSpeed;
    private float distance;

    private bool chase = false;

    // Start is called before the first frame update
    void Start()
    {
        Flip();
        rb = GetComponent<Rigidbody2D>();
        currentpoint = pointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 tempA = transform.position - player.transform.position;
        distance = Vector2.SqrMagnitude(tempA);

        
        if (currentpoint == pointB.transform)
        {
          rb.velocity = new Vector2(speed, 0);
        }
        else
        {

            rb.velocity = new Vector2(-speed, 0); 
        }

        chase = false;
     if(distance < 500000f)
        {
            if (player.transform.position.x > pointB.transform.position.x)
            {
                if (player.transform.position.x < pointA.transform.position.x)
                {
                    if (distance > distanceFromPlayer)
                    {
                        //chase
                        chase = true;
                        Vector2 newPosition = new Vector2(player.transform.position.x, this.transform.position.y); //to restrict movements in y direction
                        transform.position = Vector2.MoveTowards(this.transform.position, newPosition, chaseSpeed * Time.deltaTime);
                    }
                    else
                    {
                        chase = true;
                        rb.velocity = new Vector2(0, 0);
                    }
                   
                }
            }
        }

     if(chase)
        {
            if(player.transform.position.x < transform.position.x && facingRight)
            {
                Debug.Log("flip1");
                Flip();
            }
            if (player.transform.position.x > transform.position.x && !facingRight)
            { 
                Debug.Log("flip2");
                Flip(); 
            }
        }
     else
        {
            if(currentpoint == pointB.transform && !facingRight)
            {

                Debug.Log("flip3");
                Flip();
            }
            if(currentpoint == pointA.transform && facingRight)
            {
                Debug.Log("flip4");
                Flip();
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentpoint == pointB.transform && collision.CompareTag("Patroller Point"))
        {
            currentpoint = pointA.transform;
        }
        else if (currentpoint == pointA.transform && collision.CompareTag("Patroller Point"))
        {
            currentpoint = pointB.transform;
        }
    }

    void Flip()
    {
        Vector3 currScale = gameObject.transform.localScale;
        currScale.x *= -1;
        gameObject.transform.localScale = currScale;

        facingRight = !facingRight;
    }
}

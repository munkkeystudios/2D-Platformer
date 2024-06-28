using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//using static UnityEngine.RuleTile.TilingRuleOutput;

public class SlimeMovement : MonoBehaviour
{
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    [SerializeField] private float speed;
    [SerializeField] private float distanceFromPlayer;
    [SerializeField] float temp;
    private Rigidbody2D rb;
    private Transform currentpoint;


    [SerializeField] private GameObject player;
    [SerializeField] private float chaseSpeed;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
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

     if(distance < 25)
        {
            if (player.transform.position.x > pointB.transform.position.x)
            {
                if (player.transform.position.x < pointA.transform.position.x)
                {
                    //chase
                    Vector2 newPosition = new Vector2(player.transform.position.x, this.transform.position.y); //to restrict movements in y direction
                    if (distance > 0.5f)
                    {
                        transform.position = Vector2.MoveTowards(this.transform.position, newPosition, chaseSpeed * Time.deltaTime);
                    }
                   
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentpoint == pointB.transform && collision.CompareTag("Patroller Point"))
        {
            currentpoint = pointA.transform;
        }
        else if (temp < 1f && currentpoint == pointA.transform && collision.CompareTag("Patroller Point"))
        {
            currentpoint = pointB.transform;
        }
    }
}

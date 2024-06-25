using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    [SerializeField] private float speed;
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
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < 4)
        {
            Vector2 newPosition = new Vector2(player.transform.position.x, this.transform.position.y); //to restrict movements in y direction
            transform.position = Vector2.MoveTowards(this.transform.position, newPosition, chaseSpeed * Time.deltaTime);
        }
        else
        {
            if (currentpoint == pointB.transform)
            {
                rb.velocity = new Vector2(speed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0);
            }

            if (Vector2.Distance(transform.position, currentpoint.position) < 0.5f && currentpoint == pointB.transform)
            {
                currentpoint = pointA.transform;
            }
            else if (Vector2.Distance(transform.position, currentpoint.position) < 1f && currentpoint == pointA.transform)
            {
                currentpoint = pointB.transform;
            }
        }

    }
}

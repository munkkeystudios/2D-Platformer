using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vill1 : MonoBehaviour
{
    [SerializeField] private GameObject point1;
    [SerializeField] private GameObject point2;

    private Rigidbody2D rb;

    private bool facingRight = true;

    private float speed = 2f;
    private GameObject currpoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(point1 == null)
        {
            Debug.LogError("point1 not assigned to villager");
            return;
        }
        else if(point2 == null)
        {
            Debug.LogError("point2 not assigned to villager");
            return;
        }
        else if(rb == null)
        {
            Debug.LogError("couldnt get rigidbody2d");
            return;
        }

        currpoint = point1;
    }

    // Update is called once per frame
    void Update()
    {
        if (currpoint == point2.transform)
        {
            if(!facingRight)
            {
                Flip();
            }
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            if (facingRight)
            {
                Flip();
            }
            rb.velocity = new Vector2(-speed, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Patroller Point"))
        {
            if(currpoint == point1)
            {
                currpoint = point2;
            }
            else
            {
                currpoint = point1;
            }
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

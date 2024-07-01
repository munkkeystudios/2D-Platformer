using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
//using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class SlimeMovement : MonoBehaviour
{

    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    [SerializeField] float speed;
    private Animator anim;
    private Rigidbody2D rb;

    bool facingRight = false;
    [SerializeField] private GameObject reset_point;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (anim == null || rb ==null || pointA == null ||pointB == null || reset_point == null|| player == null)
        {
            Debug.LogError("One or more required components not assigned in"+ gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Move", false);
        Vector2 tempA = transform.position - player.transform.position;
        float distanceToPlayer = Vector2.SqrMagnitude(tempA);

        if (distanceToPlayer < 50)
        {
            if (player.transform.position.x > pointB.transform.position.x)
            {
                if (player.transform.position.x < pointA.transform.position.x)
                {
                    //chase
                    Vector2 newPosition = new Vector2(player.transform.position.x, this.transform.position.y); //to restrict movements in y direction
                    if (distanceToPlayer > 2.5f)
                    {
                        anim.SetBool("Move", true);
                        transform.position = Vector2.MoveTowards(this.transform.position, newPosition, speed * Time.deltaTime);

                        if (transform.position.x < player.transform.position.x && !facingRight)
                        {
                            Flip();
                        }
                        if (transform.position.x > player.transform.position.x && facingRight)
                        {
                            Flip();
                        }

                    }
                    else
                    {

                        anim.SetBool("Move", false);
                        rb.velocity = new Vector2(0, 0);
                    }

                }

            }
        }
        else if (player.transform.position.x < pointB.transform.position.x || player.transform.position.x > pointA.transform.position.x) 
        {
            anim.SetBool("Move", true);
            if(reset_point.transform.position.x > this.transform.position.x && !facingRight)
            {
                Flip();
            }
            else if(reset_point.transform.position.x < this.transform.position.x && facingRight)
            {
                Flip();
            }
            Vector2 newPosition = new Vector2(reset_point.transform.position.x, this.transform.position.y);
            transform.position = Vector2.MoveTowards(this.transform.position, newPosition, speed * Time.deltaTime);
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


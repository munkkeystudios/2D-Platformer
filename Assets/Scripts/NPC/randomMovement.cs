using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine.Editor;
using UnityEngine;

public class randomMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private float changeDirectionCooldown;
    private bool facingRight = true;

    Animator anim;

    private Rigidbody2D rb;


    Vector2 waypoint;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not assigned to villager");
            return;
        }
        if(anim == null)
        {
            Debug.LogError("Animator not assigned to villager");
            return;
        }
        setNewDest();
    }

    // Update is called once per frame
    void Update()
    {
        setNewDest();
        if (waypoint.x % 2f == 0 && waypoint.x > 50)
        {
            ActivateLayer("Idle Layer");
            rb.velocity = new Vector2(0, 0);

        }
        else if (waypoint.x > transform.position.x)
        {
            if(!facingRight)
            {
                Flip();
            }
            ActivateLayer("Walk Layer");
            rb.velocity = new Vector2(speed, 0);
        }
        else if (waypoint.x < transform.position.x)
        {
            if (facingRight)
            {
                Flip();
            }
            ActivateLayer("Walk Layer");
            rb.velocity = new Vector2(-speed, 0);
        }
       
    }

    void setNewDest()
    {
        changeDirectionCooldown -= Time.deltaTime;
        if(changeDirectionCooldown <= 0 )
        {
            waypoint = new Vector2(Random.Range(0, 100), 0);
            changeDirectionCooldown = Random.Range(0,7);
        }
    }
    void Flip()
    {
        Vector3 currScale = gameObject.transform.localScale;
        currScale.x *= -1;
        gameObject.transform.localScale = currScale;

        facingRight = !facingRight;
    }

    private void ActivateLayer(string layerName)
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            {
                anim.SetLayerWeight(i, 0);

            }


            anim.SetLayerWeight(anim.GetLayerIndex(layerName), 1);
        }
    }
}

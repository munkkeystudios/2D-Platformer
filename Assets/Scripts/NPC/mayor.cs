using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mayor : MonoBehaviour
{
    private bool played = false;

    private bool facingRight = true;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject maincam;
    [SerializeField] private GameObject custscenecam;

    [SerializeField] private float speed;

    private float changeDirectionCooldown;

    Vector2 waypoint;


    private Rigidbody2D rb;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator not assigned to mayor");
            return;
        }
        if (player == null)
        {
            Debug.LogError("player not assigned to mayor");
            return;
        }
        if (maincam == null)
        {
            Debug.LogError("maincam not assigned");
            return;
        }
        if (custscenecam == null)
        {
            Debug.LogError("cutscenecam not assigned");
            return;
        }
       if (rb == null)
        {
            Debug.LogError("Rigidbody not assigned to mayor");
            return;
        }

        custscenecam.SetActive(false);
        maincam.SetActive(true);

        ActivateLayer("Idle Layer");
    }

    // Update is called once per frame
    void Update()
    {
        setNewDest();
        Debug.Log(waypoint.x);
        if(waypoint.x % 2f == 0 && waypoint.x > 50)
        {
            rb.velocity = new Vector2(0, 0);
            ActivateLayer("Idle Layer");

        }
        else if (waypoint.x > transform.position.x)
        {
            if (!facingRight)
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
        if (changeDirectionCooldown <= 0)
        {
            waypoint = new Vector2(Random.Range(0, 100), 0);
            changeDirectionCooldown = Random.Range(0, 7);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Invoke("Play", 0.5f);
        }
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

    void Play()
    {
        played = true;
        maincam.SetActive(false);
        custscenecam.SetActive(true);
        Invoke("switchBack", 103f);
    }

    void switchBack()
    {
        custscenecam.SetActive(false);
        maincam.SetActive(true);
    }
    void Flip()
    {
        Vector3 currScale = gameObject.transform.localScale;
        currScale.x *= -1;
        gameObject.transform.localScale = currScale;

        facingRight = !facingRight;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCheck : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private bool facingRight = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > player.transform.position.x && facingRight)
        {
            Flip();
        }
        else if (transform.position.x < player.transform.position.x && !facingRight)
        {
            Flip();
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

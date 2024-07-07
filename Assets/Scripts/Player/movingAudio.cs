using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingAudio : MonoBehaviour
{

    audiomanager audioManager;
    private bool jumping = false;

    [SerializeField] private LayerMask groundLayer;
    private BoxCollider2D boxCollider;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audiomanager>();

        boxCollider = GetComponent<BoxCollider2D>();

        if (audioManager == null)
        {
            Debug.LogError("Audio not initialised");
        }

        if (rb == null || boxCollider == null)
        {
            Debug.LogError("Missing component in movingAudio");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.y > 0)
        {
            jumping = true;
        }
        if(rb.velocity.x > 0 || rb.velocity.x<0) 
         {
            Debug.Log("playing run");
            audioManager.PlaySFX(audioManager.playerRun);
         }
        else if(jumping == true && isGrounded())
        {
            audioManager.PlaySFX(audioManager.playerJump);
        }
    }
    public bool isGrounded()
    {
        if (boxCollider == null)
        {
            Debug.LogWarning("BoxCollider2D is not attached");
            return false;
        }
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.2f, groundLayer);
        return raycastHit.collider != null;
    }

}

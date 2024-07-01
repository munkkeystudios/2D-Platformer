using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float bounce;
    private Rigidbody2D playerRb = null;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerRb == null)
            {
                playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerRb == null)
                {
                    Debug.LogError("Rigidbody2D component not found on the player object.");
                    return;
                }
            }
            playerRb.AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleKiller : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                // Set health to 0 to trigger death
                playerHealth.TakeDamage(playerHealth.CurrentHealth);
            }
        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerLivesManager playerLivesManager = collision.GetComponent<PlayerLivesManager>();
            if (playerLivesManager != null)
            {
                playerLivesManager.UpdateCheckpoint(transform);
            }
        }
    }
}

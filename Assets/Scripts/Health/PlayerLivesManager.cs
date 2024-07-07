using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLivesManager : MonoBehaviour
{
    [SerializeField] private int lives = 3;
    private Vector3 respawnPosition;
    private Vector3 startPosition;
    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        if (playerHealth == null)
        {
            Debug.LogError("PlayerLivesManager: Missing Health component");
        }
        startPosition = transform.position; // Capture the start position
        respawnPosition = startPosition;
    }



  

    private void OnEnable()
    {
        playerHealth.OnDied += HandleDeath;
    }
    private void OnDisable()
    {
        playerHealth.OnDied -= HandleDeath;
    }

    private IEnumerator RespawnDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the death animation to play
        Respawn();
    }

    public void ReducePlayerSize()
    {
        transform.localScale = Vector3.zero;
    }
    private void HandleDeath()
    {
        lives--;
        if (lives > 0)
        {
            StartCoroutine(RespawnDelay(2.0f));
        }
        else
        {
            Debug.Log("Game Over");
            // Implement game over logic here
        }
    }
    
    public void UpdateCheckpoint(Transform checkpointTransform)
    {
        respawnPosition = checkpointTransform.position;
    }


    private void Respawn()
    {
        gameObject.SetActive(true); // Ensure the player is active

        // Restore the player's size
        transform.localScale = Vector3.one;

        // Move player to the last checkpoint or start position
        transform.position = respawnPosition;

        // Fully heal the player
        playerHealth.Heal(playerHealth.MaxHealth);

        // Reset animations or other states as needed
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("Death", false);
            animator.Play("Idle"); // Adjust as necessary
        }
    }
}

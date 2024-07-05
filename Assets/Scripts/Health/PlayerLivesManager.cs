using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLivesManager : MonoBehaviour
{
    [SerializeField] private int lives = 3;
    [SerializeField] private Transform checkpoint;
    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        if (playerHealth == null || checkpoint == null)
        {
            Debug.LogError("PlayerLivesManager: Missing component or reference");
        }
    }

    private void OnEnable()
    {
        playerHealth.OnDied += HandleDeath;
    }
    private void OnDisable()
    {
        playerHealth.OnDied -= HandleDeath;
    }

    private void HandleDeath()
    {
        lives--;
        if (lives>0)
        {
            RespawnAtCheckpoint();
        }

        else
        {
            Debug.Log("Game Over");
        }
    }
    public void RespawnAtCheckpoint()
    {
        // Ensure the player is active
        gameObject.SetActive(true);

        // Move player to checkpoint
        transform.position = checkpoint.position;

        // Fully heal the player
        playerHealth.Heal(playerHealth.MaxHealth);

        // Optionally, reset other states as needed (e.g., animations)
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("Death", false);
            animator.Play("Idle"); // Assuming "Idle" is a base state, adjust as necessary
        }
    }
}

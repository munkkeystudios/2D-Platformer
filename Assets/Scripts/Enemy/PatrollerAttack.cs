using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollerAttack : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Health playerHealth;

    [SerializeField] private float damage = 10f;//damage to player
    [SerializeField] private float attackRange = 1.5f;//attack range of enemy
    [SerializeField] private float attackInterval = 2f;//time after which enemy can attack again
    private float attackTimer;//to check if the interval has completed

    private void Awake()
    {
        if (player != null)
        {
            playerHealth =player.GetComponent<Health>();
            if (playerHealth == null)
            {

                Debug.LogError("Health comp not found on player.");
            }
        }
        else
        {
            Debug.LogError("Player is not assigned in " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth == null)
        {
            return;
        }

        float distanceSquared = (this.transform.position - player.transform.position).sqrMagnitude;

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;//updating attack timer
        }
        if (distanceSquared <= attackRange * attackRange && attackTimer <= 0)//attack if in range and timer is up
        {
            Attack();
            attackTimer = attackInterval;
        }
    }

    void Attack()
    {
        playerHealth.TakeDamage(damage);
        Debug.Log("attacking");
    }
}

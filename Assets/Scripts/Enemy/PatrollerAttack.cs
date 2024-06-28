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
    [SerializeField] private float attackTimer;//to check if the interval has completed

    // Start is called before the first frame update
    void Start()
    {
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth == null)
        {
            return;
        }

        float distanceSquared = Vector2.SqrMagnitude(this.transform.position - player.transform.position);

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

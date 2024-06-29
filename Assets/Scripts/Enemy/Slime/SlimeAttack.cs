using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Health playerHealth;

    [SerializeField] private float damage = 10f;//damage to player
    [SerializeField] private float attackRange = 1.5f;//attack range of enemy
    [SerializeField] private float attackInterval = 2f;//time after which enemy can attack again
    [SerializeField] private float attackTimer;//to check if the interval has completed
    

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
        }
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth == null)
        {
            return;
        }

        Vector2 tempA = transform.position - player.transform.position;
        float distanceToPlayer = Vector2.SqrMagnitude(tempA);
        
        bool isTimerNull = false;

        if (attackTimer > 0)
        {
            isTimerNull = false;
            attackTimer -= Time.deltaTime;//updating attack timer
        }
        if (distanceToPlayer <= attackRange && attackTimer <= 0)//attack if in range and timer is up
        {
            isTimerNull = true;
            Attack();
            attackTimer = attackInterval;
        }
        if (isTimerNull)
        {
            anim.SetBool("isAttacking", true);
        }
        
    }

    void Attack()
    {
        playerHealth.TakeDamage(damage);
    }
}

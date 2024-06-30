using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyAttack : MonoBehaviour
{
    public Transform player;
    private Health playerHealth;

    public float damage = 10f;//damage to player
    public float attackRange = 1.5f;//attack range of enemy
    public float attackInterval = 2f;//time after which enemy can attack again
    public float attackTimer;//to check if the interval has completed

    private bool isAttacking;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        isAttacking = false;
        if (playerHealth == null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;//updating attack timer
        }
        if (distanceToPlayer <= attackRange && attackTimer <= 0)//attack if in range and timer is up
        {
            isAttacking = true;
            Attack();
            attackTimer = attackInterval;
        }

        if (isAttacking)
        {
            StartCoroutine(StartAttack());
        }
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("Attack", true);
        StartCoroutine(EndAttack());
    }

    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("Attack", false);
    }

        void Attack()
    {
        playerHealth.TakeDamage(damage);
    }
}

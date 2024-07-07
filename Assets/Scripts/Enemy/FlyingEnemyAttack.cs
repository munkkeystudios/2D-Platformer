using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyAttack : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Health playerHealth;
    private Health health;

    [SerializeField] private float damage = 10f;//damage to player
    [SerializeField] private float attackRange = 1.5f;//attack range of enemy
    [SerializeField] private float attackInterval = 2f;//time after which enemy can attack again
    private float attackTimer;//to check if the interval has completed

    private bool isAttacking;
    private Animator anim;
     private audiomanager audioManager;

    // Start is called before the first frame update
    private void Awake()
    {
        anim =GetComponent<Animator>();
        health = GetComponent<Health>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audiomanager>();

        if (audioManager == null)
        {
            Debug.LogError("Audio not initialised");
        }
        if (anim == null || health == null)
        {
            Debug.LogError("Animator or health component not found in " + gameObject.name);
        }

        if (player!= null)
        {
            playerHealth = player.GetComponent<Health>();
            if (playerHealth ==null)
            {
                Debug.LogError("health component not found on player");
            }
        }
        else
        {
            Debug.LogError("Player transform is not assigned in " +gameObject.name);
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
        if (distanceToPlayer <= attackRange && attackTimer <= 0 && health.CurrentHealth!=0 )//attack if in range and timer is up
        {
            isAttacking = true;
            audioManager.PlaySFX(audioManager.enemyAttack);
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

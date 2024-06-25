using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damage = 10f;
    public float attackRange = 1.5f;
    private float attackTimer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AttackNearestEnemy();
        }
    }

    void AttackNearestEnemy()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Enemy"));
        Collider2D closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (var enemy in hitEnemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            Health enemyH = closestEnemy.GetComponent<Health>();
            if (enemyH!= null)
            {
                enemyH.TakeDamage(damage);
            }
        }

    }
    /*
     ATTEMPT AT DOING ALL THREE ATTACKS, wasn't working so currently implemented only 1 attack for first draft
    public AttackDefinitions[] attacks;
    private float[] lastAttackTimes;

    private void Awake()
    {
        lastAttackTimes = new float[attacks.Length];
        foreach (var attack in attacks)
        {
            if (attack.attackCollider!= null)
            {
                attack.attackCollider.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(ActivateAttackCollider(0));
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(ActivateAttackCollider(1));
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(ActivateAttackCollider(2));
        }
    }

    IEnumerator ActivateAttackCollider(int attackIndex)
    {
        if (Time.time -lastAttackTimes[attackIndex] <attacks[attackIndex].cooldownTime)
        {
            yield break;
        }

        GameObject collider = attacks[attackIndex].attackCollider;
        if (collider !=null)
        {   collider.SetActive(true);
            yield return new WaitForSeconds(0.8f);
            collider.SetActive(false);
        }
        lastAttackTimes[attackIndex] = Time.time;
    }

    */
}
    
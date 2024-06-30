using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private AttackDefinitions[] attacks;
    [SerializeField] private LayerMask enemyLayer;
    private float[] attackCooldowns;
    private Animator animator;

    private Vector2 facingDirection = Vector2.right;//default, to be set correctly for player

    private int lastAttackIndex = 0;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (attacks != null)
        {
            attackCooldowns = new float[attacks.Length];
        }
        else
        {
            Debug.LogWarning("Attack Array is not initialized.");
        }
    }

    private void Update()
    {
        if (attackCooldowns == null) return;

        for (int i = 0; i < attackCooldowns.Length; i++)
        {
            if (attackCooldowns[i] > 0)
            {
                attackCooldowns[i] -= Time.deltaTime;
            }
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Z key pressed. Attempting attack 0.");
            PerformAttack(0);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("X key pressed. Attempting attack 1.");
            PerformAttack(1);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C key pressed. Attempting attack 2.");
            PerformAttack(2);
        }
    }

    public void PerformAttack(int attackIndex)
    {
        lastAttackIndex = attackIndex;
        if (attacks == null || attackIndex < 0 ||attackIndex> attacks.Length || attackCooldowns[attackIndex] > 0 || attackCooldowns == null)
        {
            Debug.Log("Invalid attack index or attack on cooldown.");
            return;
        }

        PlayerMovementControl movementControl = GetComponent<PlayerMovementControl>();
        if (movementControl != null)
        {
            facingDirection = movementControl.FacingDirection;
        }
        else
        {
            Debug.LogWarning("PlayerMovementControl component not found.");
        }

        AttackDefinitions attack = attacks[attackIndex];
        if (animator != null)
        {
            animator.SetBool(attack.animationTrigger, true);
        }
        else
        {
            Debug.LogWarning("Animator component not found.");
        }

        Vector2 boxCastSize = new Vector2(attack.width, attack.height);
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxCastSize, 0f, facingDirection, attack.range, enemyLayer);


        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Health enemyHealth = hit.collider.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(attack.damage);
                    continue;
                }
                
            }
            attackCooldowns[attackIndex] = attack.cooldownTime;
        }
        //animator.ResetTrigger(attack.animationTrigger);
        
    }

    public void ResetAttack()
    {/*
        animator.ResetTrigger("Attack1");
        animator.ResetTrigger("Attack2");
        animator.ResetTrigger("Attack3");
        animator.SetTrigger("IdleTrigger");*/
    }

    private void OnDrawGizmos()
    {
        if (attacks == null || attacks.Length == 0 || lastAttackIndex < 0 || lastAttackIndex>= attacks.Length)
        {
            return;
        }

        AttackDefinitions attack = attacks[lastAttackIndex];
        Vector2 boxPos = (Vector2)transform.position + facingDirection * attack.range / 2;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxPos, new Vector2(attack.width, attack.height));
    }

}
/*


 ATTEMPT AT DOING ALL THREE ATTACKS, wasn't working so currently implemented only 1 attack for first draft


    public float damage = 10f;
    public float attackRange = 1.5f;
    private float attackTimer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("playerATTACK");
            AttackNearestEnemy();
        }
    }

    void AttackNearestEnemy()
    {
                Debug.Log("giving damage");
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
            if (enemyH != null)
            {
                enemyH.TakeDamage(damage);
            }
        }
    }


    public AttackDefinitions[] attacks;
    private float[] lastAttackTimes;

    private void Awake()
    {
        lastAttackTimes = new float[attacks.Length];
        foreach (var attack in attacks)
        {
            if (attack.attackCollider != null)
            {
                Debug.Log($"Collider for attack {attack.name} is correctly assigned.");
                attack.attackCollider.SetActive(false);
            }
            else
            {
                Debug.LogWarning($"Collider for attack {attack.name} is not assigned.");
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Z key pressed. Attempting attack 0.");
            StartCoroutine(ActivateAttackCollider(0));
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("X key pressed. Attempting attack 1.");
            StartCoroutine(ActivateAttackCollider(1));
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C key pressed. Attempting attack 2.");
            StartCoroutine(ActivateAttackCollider(2));
        }
    }

    IEnumerator ActivateAttackCollider(int attackIndex)
    {
        Debug.Log($"Attempting to activate collider for attack {attackIndex}.");
        if (Time.time - lastAttackTimes[attackIndex] < attacks[attackIndex].cooldownTime)
        {
            Debug.Log($"Attack {attackIndex} is on cooldown.");
            yield break;
        }

        GameObject collider = attacks[attackIndex].attackCollider;
        if (collider != null)
        {
            Debug.Log($"Activating collider for attack {attackIndex}.");
            Debug.Log($"Enemy Layer Mask: {LayerMask.GetMask("Enemy")}");
            collider.SetActive(true);
            //Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(collider.transform.position, collider.GetComponent<BoxCollider2D>().size, 0, LayerMask.GetMask("Enemy"));
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(collider.transform.position, 1f, LayerMask.GetMask("Enemy"));

            if (hitEnemies.Length == 0)
            {
                Debug.Log("No enemies hit.");
            }
            else
            {                 
                Debug.Log($"Hit {hitEnemies.Length} enemies.");
            }
            foreach (var enemy in hitEnemies)
            {
                Debug.Log($"Hit enemy: {enemy.name}");
                Health enemyHealth = enemy.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(attacks[attackIndex].damage);
                }
            }
            yield return new WaitForSeconds(3f);
            collider.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"Collider for attack {attackIndex} is null.");
        }
        lastAttackTimes[attackIndex] = Time.time;
    }
*/
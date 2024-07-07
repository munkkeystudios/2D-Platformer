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

    audiomanager audioManager;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audiomanager>();

        if(audioManager == null )
        {
            Debug.LogError("Audio not initialised");
        }
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
            if (attackCooldowns[0] <=0)
            {
            audioManager.PlaySFX(audioManager.sword1);
            }
            PerformAttack(0);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("X key pressed. Attempting attack 1.");
            if (attackCooldowns[1] <= 0)
            {
                audioManager.PlaySFX(audioManager.sword2);
            }
            PerformAttack(1);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C key pressed. Attempting attack 2.");
            if (attackCooldowns[2] <= 0)
            {
                audioManager.PlaySFX(audioManager.celestialStrike);
            }
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
        attackCooldowns[attackIndex] = attacks[attackIndex].CooldownTime;

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
            animator.SetBool(attack.AnimationTrigger, true);
        }
        else
        {
            Debug.LogWarning("Animator component not found.");
        }

        Vector2 boxCastSize = new Vector2(attack.Width, attack.Height);
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxCastSize, 0f, facingDirection, attack.Range, enemyLayer);


        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Health enemyHealth = hit.collider.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(attack.Damage);
                    continue;
                }
                
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        if (attacks == null || attacks.Length == 0 || lastAttackIndex < 0 || lastAttackIndex>= attacks.Length)
        {
            return;
        }

        AttackDefinitions attack = attacks[lastAttackIndex];
        Vector2 boxPos = (Vector2)transform.position + facingDirection * attack.Range / 2;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxPos, new Vector2(attack.Width, attack.Height));
    }

}

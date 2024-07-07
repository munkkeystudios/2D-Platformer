using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossNonscriptableobjectscript : MonoBehaviour
{
    //set up attack types using scriptable object logic, ask talha

    [SerializeField] private float attack_range;
    [SerializeField] private float boss_room_width;
    private float distance_from_player;

    public bool facing_right;
    [SerializeField] private float move_speed;

    private bool withinAggroRange = false;
    private bool withinAttackingRange = false;
    [SerializeField] private bool attack_dealt = false;

    private bool vulnerable_after_attack;
    [SerializeField] private float vulnerable_after_attack_timer;
    [SerializeField] private float time_elapsed_since_attack;
    [SerializeField] private float time_elapsed_in_vulnerable_state;
    [SerializeField] private float attack_cooldown_timer;
    [SerializeField] private float[] attack_width;
    [SerializeField] private float[] attack_height;
    [SerializeField] private float[] attack_damage;
    [SerializeField] private int attack_type;

    private Rigidbody2D rb;
    [SerializeField] private GameObject player;
    private Transform boss_transform;
    private Transform player_transform;

    private Health health;
    [SerializeField] private LayerMask playerLayer;
    private Vector2 facingDirection = Vector2.left;
    private Animator anim;
    public SpriteRenderer spriteRenderer;

    //implement scritable object similar to talhas implementation, each attack type should hold attack height, attack width for the raycast, vulnerable timers for both attacks, and damage values
    //implement change in script to ensure that boss does not have any jankiness in movement, does not teleport around player character

    private void Awake()
    {
        facing_right = true;

        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody not found");
        }

        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("player not found");
        }

        player_transform = player.transform;

        anim = GetComponent<Animator>();

        if (anim == null)
        {
            Debug.LogError("anim not found");
        }

        boss_transform = GetComponent<Transform>();

        if (boss_transform == null)
        {
            Debug.LogError("boss transform not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        distance_from_player = distanceFromPlayer(player_transform, boss_transform);

        if (distance_from_player <= boss_room_width)
        {
            anim.SetBool("is_moving", true);
            setAggroRange(true);

            if (distance_from_player <= attack_range)
            {
                //anim.SetTrigger("Attack");
                anim.SetBool("is_moving", false);
                setAttackRange(true);
            }

        }
        else
        {
            anim.SetBool("is_moving", false);
            setAggroRange(false);
        }


    }

    private void FixedUpdate()
    {
        if(attack_dealt == true && time_elapsed_in_vulnerable_state <= vulnerable_after_attack_timer)
        {
            //anim.SetBool("is_idle", true);
            //rb.velocity = Vector2.zero;
            time_elapsed_in_vulnerable_state += Time.deltaTime;
        }
        else
        {
            attack_dealt = false;
            //time_elapsed_in_vulnerable_state = 0;
            //anim.SetBool("is_idle", false);
        }

        if (withinAggroRange && attack_dealt == false)
        {
            distance_from_player = distanceFromPlayer(player_transform, boss_transform);
            Vector2 move_direction = (player_transform.position - boss_transform.position).normalized;

            if(distance_from_player > 1f)
            {
                MoveEnemy(move_direction * move_speed * Time.deltaTime);
            }
        }
        else
        {
            Vector2 move_direction = (player_transform.position - boss_transform.position).normalized;

            MoveEnemy(move_direction * Time.deltaTime);
        }

        if (withinAttackingRange && time_elapsed_since_attack >= attack_cooldown_timer && attack_dealt == false)
        {

            distance_from_player = distanceFromPlayer(player_transform, boss_transform);
            //anim.SetTrigger("Attack");
            //anim.SetFloat("distance_to_player", distance_from_player);



            //setup logic for raycast here, if within bounds of attack (set with attack height, width), then player takes damage

            StartCoroutine(PostAttackBehavior());

            time_elapsed_since_attack = 0;
        }

        time_elapsed_since_attack += Time.deltaTime;
    }

    public void MoveEnemy(Vector2 velocity)
    {
        rb.velocity = velocity;
        CheckFaceDirection(velocity);
    }

    public void CheckFaceDirection(Vector2 velocity)
    {
        if (boss_transform.position.x < player.transform.position.x && !facing_right)
        {
            Flip();
        }
        else if (boss_transform.position.x > player.transform.position.x && facing_right)
        {
            Flip();
        }
    }

    void Flip()
    {
        spriteRenderer.flipX = rb.velocity.x < 0;

        facing_right = !facing_right;
    }

    public void setAggroRange(bool start_chase)
    {
        withinAggroRange = start_chase;
    }

    public void setAttackRange(bool attack_check)
    {
        anim.SetBool("is_moving", false);
        withinAttackingRange = attack_check;
    }


    private IEnumerator PostAttackBehavior()
    {
        int attack_type = Random.Range(0, 2);

        //Debug.LogError(attack_type);

        //if (attack_type == 0)
        //{
        //    anim.SetTrigger("Attack");
        //}
        //else
        //{
        //    anim.SetTrigger("Attack1");
        //}
        //Debug.LogError("Running coroutine");
        //anim.SetTrigger("Attack");
        //anim.SetFloat("distance_to_player", distace_from_player);
        //yield return new WaitForSeconds(slowdown_duration);

        //Vector2 boxCastSize = new Vector2(attack_width[attack_type], attack_height[attack_type]);
        //RaycastHit2D hit = Physics2D.BoxCast(boss_transform.position, boxCastSize, 0f, facingDirection, attack_width[attack_type], playerLayer);

        //OnDrawGizmos();

        AttackProcess(attack_type); 

        
        StartCoroutine(VulnerablePhase());
        // Reset attack range
        withinAttackingRange = false;
        attack_dealt = true;

        yield break;
    }

    private void AttackProcess(int attack_type)
    {
        Vector2 boxCastSize = new Vector2(attack_width[attack_type], attack_height[attack_type]);
        RaycastHit2D hit = Physics2D.BoxCast(boss_transform.position, boxCastSize, 0f, facingDirection, attack_width[attack_type], playerLayer);

        if (attack_type == 0)
        {
            anim.SetTrigger("Attack");
        }
        else
        {
            anim.SetTrigger("Attack1");
        }

        if (hit.collider.CompareTag("Player"))
        {
            Health playerHealth = hit.collider.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attack_damage[attack_type]);
            }

        }

        attack_dealt = true;

        //anim.SetBool("Attack 0", false);
        //anim.SetBool("Attack 1", false);
    }

    private IEnumerator VulnerablePhase()
    {
        Debug.LogError("In Vulnerable Phase");
        //anim.SetBool("is_idle", true);
        anim.SetBool("is_moving", false);
        //rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(vulnerable_after_attack_timer);
        //anim.SetBool("is_idle", false);
        anim.SetBool("is_moving", true);
    }

    public float distanceFromPlayer(Transform player, Transform self)
    {
        return Mathf.Abs(player.position.x - self.position.x);
    }

    private void OnDrawGizmos()
    {
        Vector2 boxPos = (Vector2)boss_transform.position + facingDirection * attack_width[attack_type] / 2;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxPos, new Vector2(attack_width[attack_type], attack_height[attack_type]));
    }

}

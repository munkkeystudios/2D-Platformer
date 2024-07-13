using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BossNonscriptableobjectscript : MonoBehaviour
{
    //set up attack types using scriptable object logic, ask talha

    [SerializeField] private float attack_range;
    [SerializeField] private float boss_room_width;
    private float distance_from_player;

    public bool facing_right;
    [SerializeField] private float move_speed;
    [SerializeField] private float speed_change;
    [SerializeField] private float attack_increase;

    private bool withinAggroRange = false;
    private bool withinAttackingRange = false;
    [SerializeField] private bool attack_dealt;

    [SerializeField] private float time_elapsed_since_attack;
    [SerializeField] private float attack_cooldown_change;
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
    float currentHealth;
    float maxHealth;

    private bool check_for_phase2;
    [SerializeField] private LayerMask playerLayer;
    private Vector2 facingDirection;
    private Animator anim;
    public SpriteRenderer spriteRenderer;

    new_audio_manager audioManager;

    private void Awake()
    {
        health = GetComponent<Health>();
        if(health == null)
        {
            Debug.LogError("health not found in components");
        }

        maxHealth = health.MaxHealth;

        check_for_phase2 = false;
        attack_dealt = false;

        facingDirection = Vector2.left;
        facing_right = false;

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


        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<new_audio_manager>();

        if (audioManager == null)
        {
            Debug.LogError("Audio not initialised");
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = health.CurrentHealth;

        if(currentHealth == (maxHealth/2)  && !check_for_phase2)
        {
            StartCoroutine(startPhase2());
            check_for_phase2 = true;
        }

        if (time_elapsed_since_attack >= attack_cooldown_timer)
        {
            attack_dealt = false;
        }

        distance_from_player = distanceFromPlayer(player_transform, boss_transform);

        if (distance_from_player <= boss_room_width)
        {
            anim.SetBool("is_moving", true);
            setAggroRange(true);

            if (distance_from_player <= attack_range)
            {
                setAttackRange(true);
            }
            else
            {
                setAttackRange(false);
            }

        }

        time_elapsed_since_attack += Time.deltaTime;

    }

    private void FixedUpdate()
    {

        if (withinAggroRange)
        {
            distance_from_player = distanceFromPlayer(player_transform, boss_transform);
            //audioManager.PlaySFX(audioManager.boss_move);
            Vector2 move_direction = (player_transform.position - boss_transform.position).normalized;

            if(distance_from_player > 0.2f)
            {
                MoveEnemy(move_direction * move_speed * Time.deltaTime);
            }

        }

        if (withinAttackingRange && attack_dealt == false)
        {
            attack_dealt = true;
            StartCoroutine(PostAttackBehavior());
            time_elapsed_since_attack = 0;
        }
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
        withinAttackingRange = attack_check;
    }


    private IEnumerator PostAttackBehavior()
    {
        int attack_type = Random.Range(0, 2);

        AttackProcess(attack_type);

        yield return new WaitForSeconds(0.5f);
        anim.SetBool("is_moving", true);
        //withinAttackingRange = false;
        //attack_dealt = true;
        Debug.LogError("Reached this part of co routine");
        //time_elapsed_since_attack = 0;
        yield break;
    }

    private void AttackProcess(int attack_type)
    {
        if (attack_type == 0)
        {
            anim.SetTrigger("Attack");
            audioManager.PlaySFX(audioManager.boss_attack1);
        }
        else
        {
            audioManager.PlaySFX(audioManager.boss_attack2);
            anim.SetTrigger("Attack1");
        }

        Vector2 boxCastSize = new Vector2(attack_width[attack_type], attack_height[attack_type]);
        Vector2 facingDirection = (player_transform.position - boss_transform.position).normalized;
        RaycastHit2D hit = Physics2D.BoxCast(boss_transform.position, boxCastSize, 0f, facingDirection, attack_width[attack_type], playerLayer);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Health playerHealth = hit.collider.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attack_damage[attack_type]);
            }
        }
    }

    public float distanceFromPlayer(Transform player, Transform self)
    {
        return Mathf.Abs(player.position.x - self.position.x);
    }

    private IEnumerator startPhase2()
    {
        anim.SetTrigger("Phase2");
        move_speed += speed_change;
        attack_cooldown_timer -= attack_cooldown_change;

        attack_damage[0] += attack_increase;
        attack_damage[1] += attack_increase;

        yield return new WaitForSeconds(1f);
        //instantiation of flying demons in boss arena?
    }



}

using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Enemy : MonoBehaviour, Enemy_damageable, Enemy_moveable, TriggerCheckable
{
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }
    public Rigidbody2D Rb { get; set;}

    public bool facing_right { get; set; } = false;
    public bool WithinAttackDistance { get; set; }
    public bool WithinAggroRange { get; set; }

    public Animator anim;


    #region State Machine Variables

    public Enemy_State_Machine StateMachine { get; set; }

    public Enemy_IdleState IdleState { get; set; }

    public Enemy_ChaseState ChaseState { get; set; }
    public Enemy_AttackState AttackState { get; set; }



    #endregion

    #region ScriptableObjectVariables

    [SerializeField] private EnemyIdleSObase EnemyIdleBase;
    [SerializeField] private EnemyChaseSObase EnemyChaseBase;
    [SerializeField] private EnemyAttackSObase EnemyAttackBase;

    public EnemyIdleSObase EnemyIdleBaseInstance { get; set; }
    public EnemyChaseSObase EnemyChaseBaseInstance { get; set; }    
    public EnemyAttackSObase EnemyAttackBaseInstance { get; set; }  
    #endregion

    private void Awake()
    {
        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(EnemyChaseBase);
        EnemyAttackBaseInstance = Instantiate(EnemyAttackBase);
          
        StateMachine = new Enemy_State_Machine();

        IdleState = new Enemy_IdleState(this, StateMachine);
        ChaseState = new Enemy_ChaseState(this, StateMachine);
        AttackState = new Enemy_AttackState(this, StateMachine);
    }

    void Start()
    {
        CurrentHealth = MaxHealth;

        Rb = GetComponent<Rigidbody2D>();

        if (Rb == null)
        {
            Debug.LogError("Rigidbody was not found");
        }

        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }

    #region Distance_checks

    public void SetStrikingDistanceBool(bool withinAttackDistance)
    {
        WithinAttackDistance = withinAttackDistance;
    }
    public void SetAggroRangeBool(bool withinAgrroRange)
    {
        WithinAggroRange = withinAgrroRange;
    }

    #endregion
    #region  Animation triggers

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentEnemyState.AnimataionTriggerEvent(triggerType);
    }


    public enum AnimationTriggerType
    {
        EnemyDamaged,
        EnemyDeath,
        EnemyChase,
        EnemyAttack,
        PlayFootstepSound
    }


    #endregion

    #region Health/Death functions

    public void Damage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        throw new System.NotImplementedException();
    }

    #endregion

    #region Movement Functions
    public void MoveEnemy(Vector2 velocity)
    {
        Rb.velocity = velocity;
        CheckFaceDirection(velocity);
    }

    public void CheckFaceDirection(Vector2 velocity)
    {
        if (facing_right && velocity.x < 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, 0f);
            transform.rotation = Quaternion.Euler(rotator);
            facing_right = !facing_right;
        }
        else if (!facing_right && velocity.x > 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, 0f);
            transform.rotation = Quaternion.Euler(rotator);
            facing_right = !facing_right;
        }
    }

    #endregion
}

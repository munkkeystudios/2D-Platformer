using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseSObase : ScriptableObject
{
    protected Enemy enemy;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform player_transform;

    private float Move_speed = 3f;

    public virtual void Initialize(GameObject gameObject, Enemy enemy)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;

        player_transform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void DoEnterLogic()
    {

    }

    public virtual void DoExitLogic() { ResetValues(); }

    public virtual void DoFrameUpdateLogic()
    {
        if (enemy.WithinAttackDistance)
        {
            enemy.StateMachine.ChangeState(enemy.AttackState);
        }

        Vector2 move_direction = (player_transform.position - transform.position).normalized;

        enemy.MoveEnemy(move_direction * Move_speed);
    }

    public virtual void DoPhysicsLogic()
    {

    }

    public virtual void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {

    }

    public virtual void ResetValues()
    {

    }
}

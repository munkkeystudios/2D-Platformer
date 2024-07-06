using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleSObase : ScriptableObject
{
    protected Enemy enemy;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform player_transform;

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
        if (enemy.WithinAggroRange)
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }
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

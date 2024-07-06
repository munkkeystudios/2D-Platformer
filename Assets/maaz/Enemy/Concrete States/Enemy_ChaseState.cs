using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ChaseState : Enemy_State
{


    public Enemy_ChaseState(Enemy enemy, Enemy_State_Machine enemy_state_machine) : base(enemy, enemy_state_machine)
    {
    }

    public override void AnimataionTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimataionTriggerEvent(triggerType);

        enemy.EnemyChaseBaseInstance.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.LogError("entered chase state");

        enemy.EnemyChaseBaseInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();

        enemy.EnemyChaseBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        enemy.EnemyChaseBaseInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemy.EnemyChaseBaseInstance.DoPhysicsLogic();
    }
}

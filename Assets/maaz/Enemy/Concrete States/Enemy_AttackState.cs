using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AttackState : Enemy_State
{


    public Enemy_AttackState(Enemy enemy, Enemy_State_Machine enemy_state_machine) : base(enemy, enemy_state_machine)
    {

    }

    public override void AnimataionTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimataionTriggerEvent(triggerType);

        enemy.EnemyAttackBaseInstance.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        enemy.EnemyAttackBaseInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();

        enemy.EnemyAttackBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        enemy.EnemyAttackBaseInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemy.EnemyAttackBaseInstance.DoPhysicsLogic();
    }
}

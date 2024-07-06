using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;


//update script in order to ensure that movement is as intended, 2d platformer, no movement in z or y direction
public class Enemy_IdleState : Enemy_State
{
    
    public Enemy_IdleState(Enemy enemy, Enemy_State_Machine enemy_state_machine) : base(enemy, enemy_state_machine)
    {
    }

    public override void AnimataionTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimataionTriggerEvent(triggerType);

        enemy.EnemyIdleBaseInstance.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        enemy.EnemyIdleBaseInstance.DoEnterLogic();

    }

    public override void ExitState()
    {
        base.ExitState();

        enemy.EnemyIdleBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        enemy.EnemyIdleBaseInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemy.EnemyIdleBaseInstance.DoPhysicsLogic();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_State
{
    protected Enemy enemy;
    protected Enemy_State_Machine enemy_state_machine;

    public Enemy_State(Enemy enemy, Enemy_State_Machine enemy_state_machine)
    {
        this.enemy = enemy;
        this.enemy_state_machine = enemy_state_machine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimataionTriggerEvent(Enemy.AnimationTriggerType triggerType) { }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-close range", menuName = "Enemy Logic/Attack Logic/Close slam attack")]
public class EnemyAttackVariant1 : EnemyAttackSObase
{
    [SerializeField] private float chase_range_timer = 3f;
    [SerializeField] private float distance_to_count_exit = 3f;
    [SerializeField] private float attack_cooldown = 2.0f;

    private float timer;
    private float exit_timer;


    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        enemy.MoveEnemy(Vector2.zero);

        if (timer > attack_cooldown)
        {
            timer = 0f;

            Vector2 direction = (player_transform.position - enemy.transform.position).normalized;
        }

        if (Vector2.Distance(player_transform.position, enemy.transform.position) > distance_to_count_exit)
        {
            exit_timer += Time.deltaTime;

            if (exit_timer > chase_range_timer)
            {
                enemy.StateMachine.ChangeState(enemy.ChaseState);
            }
            else
            {
                exit_timer = 0f;
            }
        }

        timer += Time.deltaTime;
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}

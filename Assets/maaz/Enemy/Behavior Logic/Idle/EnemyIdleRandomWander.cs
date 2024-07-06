using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Random Wander", menuName = "Enemy Logic/Idle Logic/Random Wander")]
public class EnemyIdleRandomWander : EnemyIdleSObase
{

    //[SerializeField] private float RandomMovementRange = 5f;
    //[SerializeField] private float RandomMovementSpeed = 1f;

    //private Vector3 targetPos;
    //private Vector3 direction;

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        //targetPos = GetRandomPointInSquare();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        transform.position += Vector3.zero;
        //direction = (targetPos - direction).normalized;
        //enemy.MoveEnemy(direction * RandomMovementSpeed);

        //if ((enemy.transform.position - targetPos).sqrMagnitude < 0.1f)
        //{
        //    targetPos = GetRandomPointInSquare();
        //}
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

    //private Vector3 GetRandomPointInSquare()
    //{
    //    var position = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0f, 0f);
    //    return enemy.transform.position + position * RandomMovementRange;
    //}

}

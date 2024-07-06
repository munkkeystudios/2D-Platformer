using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TriggerCheckable
{
    bool WithinAggroRange { get; set; }
    bool WithinAttackDistance { get; set; }

    void SetAggroRangeBool(bool withinAgrroRange);
    void SetStrikingDistanceBool(bool withinAttackDistance);
}

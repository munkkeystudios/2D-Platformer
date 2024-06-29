using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//for three attack types, not in use rn
[CreateAssetMenu(fileName = "New Attack", menuName = "Attack/AttackDefinitions")]
public class AttackDefinitions : ScriptableObject
{
    public float damage;
    public float range;
    public float cooldownTime;
    public float width;
    public float height;
    public string animationTrigger;
}



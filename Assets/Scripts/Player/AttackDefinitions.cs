using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "New Attack", menuName = "Attack/AttackDefinitions")]
public class AttackDefinitions : ScriptableObject
{

    public float damage { get; private set; }
    public float range { get; private set; }
    public float cooldownTime { get; private set; }
    public float width { get; private set; }
    public float height { get; private set; }
    public string animationTrigger { get; private set; }
}



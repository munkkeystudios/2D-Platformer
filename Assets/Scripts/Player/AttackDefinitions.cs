using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "New Attack", menuName = "Attack/AttackDefinitions")]
public class AttackDefinitions : ScriptableObject
{

    [SerializeField] public float damage; //{ get; private set; }
    [SerializeField] public float range;// { get; private set; }
    [SerializeField] public float cooldownTime; //{ get; private set; }
    [SerializeField] public float width; //{ get; private set; }
    [SerializeField] public float height;// { get; private set; }
    [SerializeField] public string animationTrigger;// { get; private set; }
}



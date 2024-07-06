using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack/AttackDefinitions")]
public class AttackDefinitions : ScriptableObject
{
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float cooldownTime;
    [SerializeField] private float width;
    [SerializeField] private float height;
    [SerializeField] private string animationTrigger;

    private void Awake()
    {
        if ( animationTrigger == null)
        {
            Debug.LogError("AttackDefinitions has null values.");
        }
    }

    public float Damage => damage;
    public float Range => range;
    public float CooldownTime => cooldownTime;
    public float Width => width;
    public float Height => height;
    public string AnimationTrigger => animationTrigger;

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
}

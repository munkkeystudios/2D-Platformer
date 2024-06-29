using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHealth : MonoBehaviour
{
    public float maxHealth; //allow only geetting but not setting
    [SerializeField] private float currentHealth;

    public delegate void HealthChanged(float currentHealth);//delegate for health change event (similar to function pointer in c++ )
    public event HealthChanged OnHealthChanged;// event for health change based on delegate, will notify the subscriber methods when health changes.

    public delegate void Died();//similarly a delegate for died event
    public event Died OnDied;//for death notifications

    private Animator anim;
    void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    void Update()
    {

    }
    public void TakeDamage(float damage)
    {
        if (currentHealth - damage <= 0)//checking if the damage results in death
        {
            anim.SetBool("isDead", true);
            currentHealth = 0;//setting health to 0
            OnHealthChanged?.Invoke(currentHealth);//notifying subscriber about the health change
            OnDied?.Invoke();//notifying subscriber about the death
        }
        else
        {
            anim.SetBool("isHurt", true);
            currentHealth -= damage;//reducing health and notifying subscriber
            OnHealthChanged?.Invoke(currentHealth);
        }
    }

    public void Heal(float healAmount)
    {

        if (currentHealth + healAmount >= maxHealth)//same for healing, changes health and notifies subscriber, making sure to not go above max health
        {
            currentHealth = maxHealth;
            OnHealthChanged?.Invoke(currentHealth);
        }
        else
        {
            currentHealth += healAmount;
            OnHealthChanged?.Invoke(currentHealth);
        }
    }

}

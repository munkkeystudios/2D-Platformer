using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth; //allow only geetting but not setting
    [SerializeField]private float currentHealth;

    public delegate void HealthChanged(float currentHealth);
    public event HealthChanged OnHealthChanged;

    public delegate void Died();
    public event Died OnDied;
    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(10);
        }
    }
    public void TakeDamage(float damage)
    {
        if (currentHealth - damage <= 0)
        {
            currentHealth = 0;
            OnHealthChanged?.Invoke(currentHealth);
            OnDied?.Invoke();
        }
        else
        {
            currentHealth -= damage;
            OnHealthChanged?.Invoke(currentHealth);
        }
    }

    public void Heal(float healAmount)
    {

       if (currentHealth + healAmount >= maxHealth)
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

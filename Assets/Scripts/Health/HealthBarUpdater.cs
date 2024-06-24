using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUpdater : MonoBehaviour
{
    public Health healthComponent;//the player or enemy whose health bar is to be updated
    public Image healthBar;

    private void OnEnable()//called when gameobject created
    {
        healthComponent.OnHealthChanged += UpdateHealthBar;//subscribing to the health change event of the healthComponent, meaning UpdateHealthBar will be called everytime health changes
        healthComponent.OnDied += HandleDeath;//same for death calling handledeath
    }

    private void OnDisable()//called when gameobject destroyed
    {
        healthComponent.OnHealthChanged -= UpdateHealthBar;//unsubscribe to prevent memory leaks like for pointers in c++
        healthComponent.OnDied -= HandleDeath;
    }

    private void UpdateHealthBar(float currentHealth)
    {
        if (healthComponent.maxHealth > 0)
        {
            healthBar.fillAmount = currentHealth / healthComponent.maxHealth;//calculating fill amount of health bar based on current and maximum health
        }
    }

    private void HandleDeath()//handle death
    {
        Destroy(gameObject);//currently just destroying the gameobject to be replaced with other logic
    }
}

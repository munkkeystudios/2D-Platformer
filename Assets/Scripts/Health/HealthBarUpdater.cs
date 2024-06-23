using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUpdater : MonoBehaviour
{
    public Health healthComponent;
    public Image healthBar;

    private void OnEnable()
    {
        healthComponent.OnHealthChanged += UpdateHealthBar;
        healthComponent.OnDied += HandleDeath;
    }

    private void OnDisable()
    {
        healthComponent.OnHealthChanged -= UpdateHealthBar;
        healthComponent.OnDied -= HandleDeath;
    }

    private void UpdateHealthBar(float currentHealth)
    {
        if (healthComponent.maxHealth > 0)
        {
            healthBar.fillAmount = currentHealth / healthComponent.maxHealth;
        }
    }

    private void HandleDeath()
    {
        Destroy(gameObject);
    }
}

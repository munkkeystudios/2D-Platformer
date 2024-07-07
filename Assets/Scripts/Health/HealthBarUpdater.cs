using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUpdater : MonoBehaviour
{
    [SerializeField] private Health healthComponent;//the player or enemy whose health bar is to be updated
    [SerializeField] private Image healthBar;
    private Animator anim;

    audiomanager audioManager;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audiomanager>();

        if (audioManager == null)
        {
            Debug.LogError("Audio not initialised");
        }
        if (anim == null)
        {
            Debug.LogError("Animator not found in " + gameObject.name);
        }
        if (healthComponent == null)
        {
            Debug.LogError("Health component not assigned in " + gameObject.name);
        }
        else if (healthBar == null)
        {
            Debug.LogError("HealthBar Image not assigned in " + gameObject.name);
        }
    }
    private void OnEnable()//called when gameobject created
    {
        if (healthComponent != null)
        {
            healthComponent.OnHealthChanged += UpdateHealthBar;//subscribing to the health change event of the healthComponent, meaning UpdateHealthBar will be called everytime health changes
            healthComponent.OnDied += HandleDeath;//same for death calling handledeath
        }
    }

    private void OnDisable()//called when gameobject destroyed
    {
        if (healthComponent != null)
        {
            healthComponent.OnHealthChanged -= UpdateHealthBar;//unsubscribe to prevent memory leaks like for pointers in c++
            healthComponent.OnDied -= HandleDeath;
        }
    }
    public void UpdateHealthBar(float currentHealth)
    {
        if (healthComponent.MaxHealth > 0)
        {
            healthBar.fillAmount = currentHealth / healthComponent.MaxHealth;//calculating fill amount of health bar based on current and maximum health
        }
    }
    public void SetHealthComponent(Health health)
    {
        if (healthComponent != null)
        {
            // Unsubscribe from the old Health component's events to prevent memory leaks
            healthComponent.OnHealthChanged -= UpdateHealthBar;
            healthComponent.OnDied -= HandleDeath;
        }

        healthComponent = health;

        // Subscribe to the new Health component's events
        healthComponent.OnHealthChanged += UpdateHealthBar;
        healthComponent.OnDied += HandleDeath;

        // Immediately update the health bar UI to reflect the current health state
        UpdateHealthBar(healthComponent.CurrentHealth);
    }
    private void HandleDeath()//handle death
    {
        if (anim != null)
        {
            anim.SetBool("Death", true);
            if (this.CompareTag("Enemy"))
            {

                audioManager.PlaySFX(audioManager.enemyDeath);
            }
            else
            {

                audioManager.PlaySFX(audioManager.playerDeath);
            }
        }
    }
    private void Deactivate()
    {
        LootBag lootBag = GetComponent<LootBag>();
        if (lootBag != null)
        {
            lootBag.DropLoot(transform.position);
        }
        gameObject.SetActive(false);
    }
}

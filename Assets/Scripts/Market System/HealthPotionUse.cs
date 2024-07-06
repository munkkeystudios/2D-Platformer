using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionUse : MonoBehaviour
{
    [SerializeField] private int healAmount = 50;
    [SerializeField] Loot healthPotionLoot;
    Inventory inventory = Inventory.Instance;

    private void Awake()
    {
        if (inventory == null || healthPotionLoot == null)
        {
            Debug.LogError("Inventory or health potion loot is null.");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (inventory.HasItem(healthPotionLoot))
            {
                Debug.Log("Add the priest scene where the peist asks to use the health potion");
                GameObject player = collision.gameObject;
                if (player != null)
                {
                    UseHealthPotion(player);
                }
            }
            else
            {
                Debug.Log("No health potion in inventory");
            }
        }

    }

    private void UseHealthPotion(GameObject player)
    {
        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount);
            inventory.Remove(healthPotionLoot);
            Debug.Log("Health potion used.");
        }
    }

}

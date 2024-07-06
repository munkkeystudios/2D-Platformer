using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    public static event Action<List<InventoryItem>> InventoryChanged;

    [SerializeField] private List<InventoryItem> inventory = new List<InventoryItem>();
    public List<InventoryItem> InventoryList => inventory;
    private Dictionary<Loot, InventoryItem> lootDictionary;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        inventory = new List<InventoryItem>();
        lootDictionary = new Dictionary<Loot, InventoryItem>();
    }
    private void OnEnable()
    {
        SlimeGel.OnGelPicked += Add;
        DemonWing.OnWingPicked += Add;
        WolfPelt.OnPeltPicked += Add;
    }
    public void Add(Loot loot)
    {
        Debug.Log($"Attempting to add {loot.ItemName}");

        if (lootDictionary.TryGetValue(loot, out InventoryItem item))
        {
            item.AddToStack();
            Debug.Log($"{loot.ItemName} exists. Stack size is now {item.StackSize}.");
            InventoryChanged?.Invoke(inventory);
        }
        else
        {
            Debug.Log($"{loot.ItemName} does not exist. Adding new.");
            InventoryItem newLoot = new InventoryItem(loot);
            inventory.Add(newLoot);
            lootDictionary.Add(loot, newLoot);
            Debug.Log($"New {loot.ItemName} loot added with stack size of {newLoot.StackSize}.");
            InventoryChanged?.Invoke(inventory);
        }
    }

    public void Remove(Loot loot)
    {
        if (lootDictionary.TryGetValue(loot, out InventoryItem item))
        {
            item.RemoveFromStack();
            if (item.StackSize == 0)
            {
                inventory.Remove(item);
                lootDictionary.Remove(loot);
                Debug.Log($"{loot.ItemName} total stack is now {item.StackSize}");
            }
            InventoryChanged?.Invoke(inventory);
        }
    }

}

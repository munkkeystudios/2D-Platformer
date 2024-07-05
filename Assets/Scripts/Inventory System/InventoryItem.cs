using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventoryItem
{
    [SerializeField] private Loot lootData;
    [SerializeField] private int stackSize;
    public Loot LootData => lootData;
    public int StackSize => stackSize;

    public InventoryItem(Loot loot)
    {
        lootData = loot;
        AddToStack();
    }

    public void AddToStack()
    {
        stackSize++;
    }
    public void RemoveFromStack()
    {
        if (stackSize > 0)
        {
            stackSize--;
        }
        else
        {
            Debug.LogWarning("Attempted to remove from stack when stack size is 0.");
        }
    }
}


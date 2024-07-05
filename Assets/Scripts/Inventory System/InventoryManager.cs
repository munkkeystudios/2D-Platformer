using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private List<InventorySlot> slots = new List<InventorySlot>(6);

    public void OnEnable()
    {
        if (slotPrefab == null)
        {
            Debug.LogError("Slot Prefab is not assigned in InventoryManager.");
            return;
        }
        Inventory.InventoryChanged += DrawInventory;
    }

    public void OnDisable()
    {
        Inventory.InventoryChanged -= DrawInventory;
    }
    void ResetInventory()
    {   
        foreach (Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }
        slots = new List<InventorySlot>(6);
    }

    void DrawInventory(List<InventoryItem> inventory)
    {
        ResetInventory();
        /*
        for (int i = 0; i < slots.Capacity; i++)
        {
            CreateInventorySlot();
        }

        for (int i=0; i <inventory.Count - 1; i++)
        {
            slots[i].DrawSlot(inventory[i]);
        }*/
        for (int i = 0; i < inventory.Count; i++)
        {
            if (i >= slots.Count)
            {
                CreateInventorySlot();
            }
            slots[i].DrawSlot(inventory[i]);
        }


    }
    void CreateInventorySlot()
    {
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.transform.SetParent(transform, false);

        InventorySlot slot = newSlot.GetComponent<InventorySlot>();
        slot.ClearSlot();
        slots.Add(slot);
    }
}

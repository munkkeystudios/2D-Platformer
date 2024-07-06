using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTrigger : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;

    private void Awake()
    {
        if (inventoryPanel == null)
        {
            Debug.LogError("InventoryTrigger: Missing reference");
        }
    }
    public void TriggerInventory()
    {
    if (inventoryPanel != null)
    {
        bool isActive = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(isActive);

        if (isActive)
        {
            InventoryManager.instance.DrawInventory(Inventory.Instance.InventoryList);
        }
    } 
    }
}

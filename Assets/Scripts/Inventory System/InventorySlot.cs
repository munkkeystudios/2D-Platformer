using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI stackSizeText;


    private void Start()
    {
        if (icon == null || itemNameText == null || stackSizeText == null)
        {
            Debug.LogError("One or more UI components are not assigned in InventorySlot.");
        }
    }
    public void ClearSlot()
    {
        icon.enabled = false;
        itemNameText.enabled = false;
        stackSizeText.enabled = false;
    }

    public void DrawSlot(InventoryItem item)
    {
        if (item == null)
        {
            ClearSlot();
            return;
        }

        icon.enabled = true;
        itemNameText.enabled = true;
        stackSizeText.enabled = true;

        icon.sprite = item.LootData.Icon;
        itemNameText.text = item.LootData.ItemName;
        stackSizeText.text = item.StackSize.ToString();
    }    
}

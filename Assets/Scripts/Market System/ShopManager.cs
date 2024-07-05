using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Serializable]
    public class ShopItem
    {
        public Loot item;
        public int buyPrice;
        public int sellPrice;
    }

    public List<ShopItem> shopItems = new List<ShopItem>();
    public Inventory inventory;

    public void BuyItem(Loot item)
    {
        ShopItem shopItem = shopItems.Find(i => i.item == item);
        if(shopItem != null && CoinManager.Instance.SpendCoins(shopItem.buyPrice))
        {
            inventory.Add(item);
            Debug.Log($"Bought {item.ItemName} for {shopItem.buyPrice} coins.");
        }
        else
        {
            Debug.Log("Not enough coins or item not found.");
        }
    }

    public void SellItem(Loot item)
    {

        ShopItem shopItem = shopItems.Find(i =>i.item == item);
        if (shopItem != null && inventory.InventoryList.Exists(i => i.LootData == item))
        {
            inventory.Remove(item);
            CoinManager.Instance.AddCoins(shopItem.sellPrice);
            Debug.Log($"Sold {item.ItemName} for {shopItem.sellPrice} coins.");

        }
        else
        {
            Debug.Log("Item not found in inventory.");
        }    
    }
}

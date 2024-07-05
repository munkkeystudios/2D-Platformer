using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{

    [SerializeField] private Loot lootToDrop;
    public void DropLoot(Vector3 spawnPos)
    {
        if (lootToDrop != null)
        {
            Vector3 enemyScale = transform.localScale;
            lootToDrop.CreateLootObject(spawnPos, enemyScale);
            Debug.Log("loot dropped");
        }
        else
        {
            Debug.LogError("No loot item assigned to drop.");
        }
    }
}

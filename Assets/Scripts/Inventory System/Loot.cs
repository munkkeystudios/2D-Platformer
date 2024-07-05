using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Loot : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private string itemName;

    public Sprite Icon => icon;
    public string ItemName => itemName;

    public abstract GameObject CreateLootObject(Vector3 position, Vector3 scale);
}

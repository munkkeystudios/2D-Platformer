using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "DemonWing", menuName = "Loot Item/DemonWing")]
public class DemonWingLoot : Loot
{
    public override GameObject CreateLootObject(Vector3 position, Vector3 scale)
    {
        if (Icon == null)
        {
            Debug.LogError("Icon not assigned for " + ItemName);
            return null;
        }

        GameObject lootObject = new GameObject(ItemName);
        lootObject.transform.position = position;
        lootObject.transform.localScale = scale;

        SpriteRenderer rendered = lootObject.AddComponent<SpriteRenderer>();
        rendered.sprite = Icon;

        BoxCollider2D collider = lootObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;

        DemonWing demonWingComponent = lootObject.AddComponent<DemonWing>();
        demonWingComponent.SetWingData(this);

        lootObject.layer = LayerMask.NameToLayer("IgnorePicker");

        demonWingComponent.StartJumpEffect();

        return lootObject;
    }
}

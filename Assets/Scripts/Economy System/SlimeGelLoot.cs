using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlimeGel", menuName = "Loot Item/SlimeGel")]
public class SlimeGelLoot : Loot
{
    public override GameObject CreateLootObject(Vector3 position, Vector3 scale)
    {
        GameObject lootObject = new GameObject(ItemName);
        lootObject.transform.position = position;
        lootObject.transform.localScale = scale;

        SpriteRenderer rendered = lootObject.AddComponent<SpriteRenderer>();
        rendered.sprite = Icon;

        BoxCollider2D collider = lootObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;

        SlimeGel slimeGelComponent = lootObject.AddComponent<SlimeGel>();
        slimeGelComponent.SetGelData(this);

        lootObject.layer = LayerMask.NameToLayer("IgnorePicker");

        return lootObject;
    }
}

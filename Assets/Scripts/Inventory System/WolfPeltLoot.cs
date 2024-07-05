using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WolfPelt", menuName = "Loot Item/Wolf Pelt")]
public class WolfPeltLoot : Loot
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

        WolfPelt wolfPeltComponent = lootObject.AddComponent<WolfPelt>();
        wolfPeltComponent.SetPeltData(this);

        lootObject.layer = LayerMask.NameToLayer("IgnorePicker");

        wolfPeltComponent.StartJumpEffect();

        return lootObject;
    }
}

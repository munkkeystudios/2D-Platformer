using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Picker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("IgnorePicker"))
        {
            if (collision.gameObject == null || collision.gameObject.Equals(null))
            {
                return;
            }
            StartCoroutine(EnablePickup(collision.gameObject));
        }
    }

    IEnumerator EnablePickup(GameObject lootObject)
    {
        yield return new WaitForSeconds(1f);

        if (lootObject != null && lootObject.activeInHierarchy)
        {
            lootObject.layer = LayerMask.NameToLayer("Default");
            TryPickup(lootObject);
        }
    }

    private void TryPickup(GameObject lootObject)
    {
        if (lootObject == null || lootObject.Equals(null))
        {
            return;
        }
        IPickable pickable = lootObject.GetComponent<IPickable>();
        if (pickable != null )
        {
            Debug.Log("Picking up " + lootObject.name);
            pickable.Pick();
        }
    }
}

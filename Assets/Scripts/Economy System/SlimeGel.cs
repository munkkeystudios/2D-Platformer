using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGel: MonoBehaviour, IPickable
{
    public static event GelPicked OnGelPicked;
    public delegate void GelPicked(Loot loot);
    [SerializeField] private Loot gelData;
    private bool isBeingPickedUp = false;
    public void Pick()
    {
        if (isBeingPickedUp) return;
        isBeingPickedUp = true;
        gameObject.GetComponent<Collider2D>().enabled = false;
        OnGelPicked?.Invoke(gelData);
        gameObject.SetActive(false);
    }

    public void SetGelData(Loot newGelData)
    {
        if (newGelData != null)
        {
            gelData = newGelData;
        }
        else
        {
            Debug.LogError("Attempted to set gelData with null");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WolfPelt : MonoBehaviour, IPickable
{
    public static event PeltPicked OnPeltPicked;
    public delegate void PeltPicked(Loot loot);
    [SerializeField] private Loot peltData;
    private bool isBeingPickedUp = false;

    public void Pick()
    {
        if (isBeingPickedUp) return;
        isBeingPickedUp = true;
        gameObject.GetComponent<Collider2D>().enabled = false;
        OnPeltPicked?.Invoke(peltData);
        gameObject.SetActive(false);
    }

    public void SetPeltData(Loot newPeltData)
    {
        if (newPeltData != null)
        {
            peltData = newPeltData;
        }
        else
        {
            Debug.LogError("Attempted to set peltData with null");
        }
    }
}


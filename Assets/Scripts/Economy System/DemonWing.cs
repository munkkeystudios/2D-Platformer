using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonWing : MonoBehaviour, IPickable
{

    public static event WingPicked OnWingPicked;
    public delegate void WingPicked(Loot loot);

    [SerializeField] private Loot wingData;

    private bool isBeingPickedUp = false;

    private void Start()
    {
        if (wingData == null)
        {
            Debug.LogError("Wing data not assigned in " + gameObject.name);
        }
    }
    public void Pick()
    {
        if (isBeingPickedUp) return;
        Debug.Log("Picking up wing");
        isBeingPickedUp = true;
        gameObject.GetComponent<Collider2D>().enabled = false;
        OnWingPicked?.Invoke(wingData);
        gameObject.SetActive(false);
    }

    public void SetWingData(Loot newWingData)
    {
        if (newWingData != null)
        {
            wingData = newWingData;
        }
        else
        {
            Debug.LogError("Attempted to set wingData with null");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private List<InventorySlot> slots = new List<InventorySlot>(6);
    public static InventoryManager instance; // Declare the static instance variable
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void OnEnable()
    {
        if (slotPrefab == null)
        {
            Debug.LogError("Slot Prefab is not assigned in InventoryManager.");
            return;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        Inventory.InventoryChanged += DrawInventory;
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Inventory.InventoryChanged -= DrawInventory;
    }
    
    void ResetInventory()
    {   
        foreach (Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }
        slots = new List<InventorySlot>(6);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        DrawInventory(Inventory.Instance.InventoryList);
    }

    public void DrawInventory(List<InventoryItem> inventory)
    {
        ResetInventory();
        for (int i = 0; i < inventory.Count; i++)
        {
            if (i >= slots.Count)
            {
                CreateInventorySlot();
            }
            slots[i].DrawSlot(inventory[i]);
        }


    }
    void CreateInventorySlot()
    {
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.transform.SetParent(transform, false);

        InventorySlot slot = newSlot.GetComponent<InventorySlot>();
        slot.ClearSlot();
        slots.Add(slot);
    }

}

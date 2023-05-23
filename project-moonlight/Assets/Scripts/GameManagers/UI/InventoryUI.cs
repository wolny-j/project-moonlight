using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    InventorySlot[] slots;
    public static InventoryUI Instance;

    [SerializeField] Transform inventoryGrid;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        inventory = Inventory.Instance;
        slots = inventoryGrid.GetComponentsInChildren<InventorySlot>();
        inventory.onItemChangedCallback += UpdateUI;
        InventoryUI.Instance.UpdateUI();
        InitializeInventory();
        this.gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearItem();
            }
        }
    }

    public void InitializeInventory()
    {
        for(int i = slots.Length -1; i >= inventory.space; i--)
        {
            slots[i].gameObject.SetActive(false);
        }
    }

    

    public void UpgradeInventory()
    {
        slots[inventory.space - 1].gameObject.SetActive(true);
        InitializeInventory();
    }
}

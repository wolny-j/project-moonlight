using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInventoryUI : MonoBehaviour
{
    ChestInventory inventory;
    InventorySlot[] slots;
    public static ChestInventoryUI Instance;

    [SerializeField] Transform chestInventoryGrid;
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
        inventory = ChestInventory.Instance;
        slots = chestInventoryGrid.GetComponentsInChildren<InventorySlot>();
        inventory.onItemChangedCallback += UpdateUI;
        Instance.UpdateUI();
        //InitializeInventory();
        this.gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
                slots[i].CheckState();
            }
            else
            {
                slots[i].ClearItem();
                slots[i].CheckState();
            }
        }
    }

    public void InitializeInventory()
    {
        for (int i = slots.Length - 1; i >= inventory.space; i--)
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

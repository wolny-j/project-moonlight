using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    InventorySlot[] slots;
    public static InventoryUI Instance;

    [SerializeField] Transform inventoryGrid;
    void Start()
    {
        Instance= this;
        inventory = Inventory.Instance;
        inventory.onItemChangedCallback += UpdateUI;
        slots = inventoryGrid.GetComponentsInChildren<InventorySlot>();
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
}

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
       
    }
    void Start()
    {
        Instance = this;
        inventory = Inventory.Instance;
        slots = inventoryGrid.GetComponentsInChildren<InventorySlot>();
        inventory.onItemChangedCallback += UpdateUI;
        InventoryUI.Instance.UpdateUI();
        this.gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        Debug.Log("DOne");
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

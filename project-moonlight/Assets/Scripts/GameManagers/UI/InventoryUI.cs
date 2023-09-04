using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    InventorySlot[] slots;
    public static InventoryUI Instance;

    [SerializeField] Transform inventoryGrid;

    [SerializeField] Text cloverCounter;
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
        Instance.UpdateUI();
        //InitializeInventory();
    }

    public void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)
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

    public void UpdtaeClover()
    {
        cloverCounter.text = PlayerStats.Instance.luck.ToString();
    }


    public void InitializeInventory()
    {
        inventory = Inventory.Instance;
        slots = inventoryGrid.GetComponentsInChildren<InventorySlot>();
        inventory.onItemChangedCallback += UpdateUI;
        Instance.UpdateUI();
        for (int i = slots.Length -1; i >= inventory.space; i--)
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

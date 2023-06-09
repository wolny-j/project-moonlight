using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<Item> items;
    public delegate void OnItemChanged();
    public event OnItemChanged onItemChangedCallback;
    public int space { get; set; } = 4;

    private GameObject inventoryPanel;
    private bool isOpened = false;

    private bool isHelpOpened = false;
    [SerializeField]private GameObject helpPanel;

    [SerializeField] Animator animator;


    void Awake()
    {
        if(Instance == null)
        {
            Inventory.Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        
        inventoryPanel = GameObject.Find("Inventory");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isOpened)
        {
            animator.Play("InventoryEnter");
            onItemChangedCallback?.Invoke();
            isOpened = true;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && isOpened)
        {
            animator.Play("InventoryClose");
            isOpened = false;
        }

        if (Input.GetKeyDown(KeyCode.I) && !isHelpOpened)
        {
            helpPanel.SetActive(true);
            isHelpOpened = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && isHelpOpened)
        {
            helpPanel.SetActive(false);
            isHelpOpened = false;
        }
    }
    public bool AddItem(Item item)
    {
        if (items.Count < space)
        {
            items.Add(item);
            onItemChangedCallback?.Invoke();
            return true;
        }
        return false;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);

        onItemChangedCallback?.Invoke();

    }

    public void SearchAndRemove(Item deleteItem)
    {
        foreach (Item item in items) 
        {
            if(item.name == deleteItem.name)
            {
                RemoveItem(item);
                break;
            }
        }
    }

    public List<Item> GetItems()
    {
        return items;
    }

    public void UpgradeInventory()
    {
        space++;
        InventoryUI.Instance.UpgradeInventory();
    }

}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInventory : MonoBehaviour
{
    public static ChestInventory Instance;

    public List<Item> items;
    public delegate void OnItemChanged();
    public event OnItemChanged onItemChangedCallback;
    public int space { get; set; } = 2;

    private GameObject inventoryPanel;
    private bool isOpened = false;

    [SerializeField] Animator animator;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        inventoryPanel = GameObject.Find("ChestInventory");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isOpened)
        {
            animator.Play("ChestEnter");
            onItemChangedCallback?.Invoke();
            isOpened = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpened)
        {
            animator.Play("ChestClose");
            isOpened = false;
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
            if (item.name == deleteItem.name)
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
        ChestInventoryUI.Instance.UpgradeInventory();
    }
}

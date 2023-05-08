using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<Item> items;
    public delegate void OnItemChanged();
    public event OnItemChanged onItemChangedCallback;
    private int space = 12;

    private GameObject inventoryPanel;
    private bool isOpened = false;

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
            inventoryPanel.SetActive(true);
            onItemChangedCallback?.Invoke();
            isOpened = true;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && isOpened)
        {
            inventoryPanel.SetActive(false);
            isOpened = false;
        }
    }
        public bool AddItem(Item item)
    {
        if(items.Count < space)
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

}

using System.Collections.Generic;
using UnityEngine;
using static Inventory;

public class LoadInventory : MonoBehaviour
{
    public static LoadInventory Instance;

    [SerializeField] private Item zombieBrain;
    [SerializeField] private Item eye;
    [SerializeField] private Item shell;
    [SerializeField] private Item poppySeed;
    [SerializeField] private Item dandelionSeed;
    [SerializeField] private Item poppy;
    [SerializeField] private Item dandelion;
    [SerializeField] private Item web;
    [SerializeField] private Item healthPotion;
    [SerializeField] private Item stringItem;

    private Dictionary<string, Item> itemMap;

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

        InitializeItemMap();
    }

    private void InitializeItemMap()
    {
        itemMap = new Dictionary<string, Item>
        {
            { "Brain", zombieBrain },
            { "Eye", eye },
            { "Shell", shell },
            { "Poppy Seed", poppySeed },
            { "Dandelion Seed", dandelionSeed },
            { "Poppy", poppy },
            { "Dandelion", dandelion },
            { "Web", web },
            { "Health Potion", healthPotion },
            { "String", stringItem }
        };
    }

    public void Load(PlayerStatsDTO data)
    {
        Inventory.Instance.space = data.inventorySpace;
        InventoryUI.Instance.InitializeInventory();

        foreach (string item in data.items)
        {
            if (itemMap.ContainsKey(item))
            {
                Inventory.Instance.AddItem(itemMap[item]);
            }
        }
    }
}

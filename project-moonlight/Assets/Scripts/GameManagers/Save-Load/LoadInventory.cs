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
    [SerializeField] private Item bambooSeed;
    [SerializeField] private Item poppy;
    [SerializeField] private Item dandelion;
    [SerializeField] private Item bamboo;
    [SerializeField] private Item web;
    [SerializeField] private Item healthPotion;
    [SerializeField] private Item stringItem;
    [SerializeField] private Item gunpowder;
    [SerializeField] private Item goldBar;

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
            { "String", stringItem },
            { "Bamboo", bamboo },
            { "Bamboo Seed", bambooSeed },
            { "Gunpowder", gunpowder},
            { "Gold bar", goldBar}
        };
    }

    public void ChestLoad(ChestDTO data)
    {
        ChestInventory.Instance.space = data.space;
        ChestInventoryUI.Instance.InitializeInventory();

        foreach (string item in data.items)
        {
            if (itemMap.ContainsKey(item))
            {
                ChestInventory.Instance.AddItem(itemMap[item]);
            }
        }
    }

    public void InventoryLoad(PlayerStatsDTO data)
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

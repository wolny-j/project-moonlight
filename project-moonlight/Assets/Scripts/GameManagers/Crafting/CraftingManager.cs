using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance;
    [SerializeField] GameObject craftingPanel;

    [SerializeField] Button healthContainerButton;
    [SerializeField] Button healthPotionButton;
    [SerializeField] Button stringButton;
    [SerializeField] Button inventoryUpgradeButton;
    [SerializeField] Button chestUpgradeButton;
    [SerializeField] Button pickaxeButton;


    private Dictionary<string, int> itemCounters = new Dictionary<string, int>();

    private bool isOpened= false;

    private void Awake()
    {
        if (Instance == null)
        {
            CraftingManager.Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C) && !isOpened)
        {
            isOpened= true;
            CheckInventory();
            UpdateButtons();
            craftingPanel.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.C) && isOpened)
        {
            isOpened= false;
            craftingPanel.SetActive(false);
        }
    }

    
    public void CraftHelthContainer()
    {
        Inventory.Instance.SearchAndRemove(ItemsList.Instance.brain);
        Inventory.Instance.SearchAndRemove(ItemsList.Instance.poppy);
        HealthUIManager.Instance.AddHealthContainer();
        CheckInventory();
        UpdateButtons();
    }
    public void CraftHelthPotion()
    {
        Inventory.Instance.SearchAndRemove(ItemsList.Instance.eye);
        Inventory.Instance.SearchAndRemove(ItemsList.Instance.dandelion);
        Inventory.Instance.AddItem(ItemsList.Instance.healthPotion);
        CheckInventory();
        UpdateButtons();
    }
    public void CraftString()
    {
        Inventory.Instance.SearchAndRemove(ItemsList.Instance.web);
        Inventory.Instance.SearchAndRemove(ItemsList.Instance.web);
        Inventory.Instance.AddItem(ItemsList.Instance.stringItem);
        CheckInventory();
        UpdateButtons();
    }

    public void UpgradeInventory() 
    {
        Inventory.Instance.SearchAndRemove(ItemsList.Instance.shell);
        Inventory.Instance.SearchAndRemove(ItemsList.Instance.stringItem);
        Inventory.Instance.UpgradeInventory();
        CheckInventory();
        UpdateButtons();
    }

    public void UpgradeChest()
    {
        Inventory.Instance.SearchAndRemove(ItemsList.Instance.bamboo);
        Inventory.Instance.SearchAndRemove(ItemsList.Instance.stringItem);
        ChestInventory.Instance.UpgradeInventory();
        CheckInventory();
        UpdateButtons();
    }

    public void CraftPickaxe()
    {
        Inventory.Instance.SearchAndRemove(ItemsList.Instance.bamboo);
        Inventory.Instance.SearchAndRemove(ItemsList.Instance.stringItem);
        Inventory.Instance.SearchAndRemove(ItemsList.Instance.shell);
        PlayerStats.Instance.AddPickaxe();
        CheckInventory();
        UpdateButtons();
    }

    public void CheckInventory()
    {
        var items = Inventory.Instance.GetItems();
        ResetCounters();

        foreach (var item in items)
        {
            if (itemCounters.ContainsKey(item.name))
            {
                itemCounters[item.name]++;
            }
            else
            {
                itemCounters[item.name] = 1;
            }
        }
    }
    private int GetItemCount(string itemName)
    {
        return itemCounters.ContainsKey(itemName) ? itemCounters[itemName] : 0;
    }

    private void ResetCounters()
    {
        foreach (var key in itemCounters.Keys.ToList())
        {
            itemCounters[key] = 0;
        }
    }

    public void UpdateButtons()
    {
        healthContainerButton.interactable = GetItemCount("Brain") > 0 && GetItemCount("Poppy") > 0;
        healthPotionButton.interactable = GetItemCount("Eye") > 0 && GetItemCount("Dandelion") > 0;
        stringButton.interactable = GetItemCount("Web") >= 2;
        inventoryUpgradeButton.interactable = GetItemCount("String") > 0 && GetItemCount("Shell") > 0;
        chestUpgradeButton.interactable = GetItemCount("String") > 0 && GetItemCount("Bamboo") > 0;
        pickaxeButton.interactable = GetItemCount("String") > 0 && GetItemCount("Bamboo") > 0 && GetItemCount("Shell") > 0;
    }

}

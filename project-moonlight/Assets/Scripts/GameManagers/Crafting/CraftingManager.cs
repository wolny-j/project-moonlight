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
    [SerializeField] Button dynamiteButton;

    [SerializeField] GameObject backpackRecipit1;
    [SerializeField] GameObject backpackRecipit2;
    [SerializeField] GameObject healthContainerRecipit1;
    [SerializeField] GameObject healthContainerRecipit2;

    [SerializeField] Animator animator;


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
            UpdateRecipits();
            animator.Play("CraftingEnter");
        }
        else if(Input.GetKeyDown(KeyCode.C) && isOpened)
        {
            isOpened= false;
            animator.Play("CraftingClose");
        }
    }

    
    public void CraftHelthContainer()
    {
        if(PlayerStats.Instance.healthContainers > 4)
        {
            Inventory.Instance.SearchAndRemove(ItemsList.Instance.brain);
            Inventory.Instance.SearchAndRemove(ItemsList.Instance.poppy);
        }
        else
        {
            Inventory.Instance.SearchAndRemove(ItemsList.Instance.brain);
        }
        
        HealthUIManager.Instance.AddHealthContainer();
        CheckInventory();
        UpdateButtons();
        UpdateRecipits();
    }
    public void CraftHelthPotion()
    {
        Inventory.Instance.SearchAndRemove(ItemsList.Instance.eye);
        Inventory.Instance.SearchAndRemove(ItemsList.Instance.dandelion);
        Inventory.Instance.AddItem(ItemsList.Instance.healthPotion);
        CheckInventory();
        UpdateButtons();
        UpdateRecipits();
    }
    public void CraftString()
    {
        Inventory.Instance.SearchAndRemove(ItemsList.Instance.web);
        Inventory.Instance.SearchAndRemove(ItemsList.Instance.web);
        Inventory.Instance.AddItem(ItemsList.Instance.stringItem);
        CheckInventory();
        UpdateButtons();
        UpdateRecipits();
    }

    public void UpgradeInventory() 
    {
        if(Inventory.Instance.space > 3)
        {
            Inventory.Instance.SearchAndRemove(ItemsList.Instance.shell);
            Inventory.Instance.SearchAndRemove(ItemsList.Instance.stringItem);
        }
        else
        {
            Inventory.Instance.SearchAndRemove(ItemsList.Instance.stringItem);
        }
        
        Inventory.Instance.UpgradeInventory();
        CheckInventory();
        UpdateButtons();
        UpdateRecipits();
    }

    public void UpgradeChest()
    {
        Inventory.Instance.SearchAndRemove(ItemsList.Instance.bamboo);
        Inventory.Instance.SearchAndRemove(ItemsList.Instance.stringItem);
        ChestInventory.Instance.UpgradeInventory();
        CheckInventory();
        UpdateButtons();
        UpdateRecipits();
    }

    public void CraftPickaxe()
    {
        Inventory.Instance.SearchAndRemove(ItemsList.Instance.bamboo);
        Inventory.Instance.SearchAndRemove(ItemsList.Instance.stringItem);
        Inventory.Instance.SearchAndRemove(ItemsList.Instance.shell);
        PlayerStats.Instance.AddPickaxe();
        CheckInventory();
        UpdateButtons();
        UpdateRecipits();
    }

    public void CraftDynamite()
    {
        Inventory.Instance.SearchAndRemove(ItemsList.Instance.poppy);
        Inventory.Instance.SearchAndRemove(ItemsList.Instance.gunpowder);
        PlayerStats.Instance.dynamiteCounter += 4;
        UseDynamite.Instance.UpdateCounterUI();
        CheckInventory();
        UpdateButtons();
        UpdateRecipits();
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
        if (PlayerStats.Instance.healthContainers > 4)
            healthContainerButton.interactable = GetItemCount("Brain") > 0 && GetItemCount("Poppy") > 0;
        else
            healthContainerButton.interactable = GetItemCount("Brain") > 0;
        if (Inventory.Instance.space > 3)
            inventoryUpgradeButton.interactable = GetItemCount("String") > 0 && GetItemCount("Shell") > 0;
        else
            inventoryUpgradeButton.interactable = GetItemCount("String") > 0;

        healthPotionButton.interactable = GetItemCount("Eye") > 0 && GetItemCount("Dandelion") > 0;
        stringButton.interactable = GetItemCount("Web") >= 2;
        
        chestUpgradeButton.interactable = GetItemCount("String") > 0 && GetItemCount("Bamboo") > 0;
        dynamiteButton.interactable = GetItemCount("Poppy") > 0 && GetItemCount("Gunpowder") > 0;
        pickaxeButton.interactable = GetItemCount("String") > 0 && GetItemCount("Bamboo") > 0 && GetItemCount("Shell") > 0;

        
    }

    public void UpdateRecipits()
    {
        if(Inventory.Instance.space > 3)
        {
            backpackRecipit1.SetActive(false);
            backpackRecipit2.SetActive(true);
        }

        if(PlayerStats.Instance.healthContainers > 4)
        {
            healthContainerRecipit1.SetActive(false);
            healthContainerRecipit2.SetActive(true);
        }
    }

}

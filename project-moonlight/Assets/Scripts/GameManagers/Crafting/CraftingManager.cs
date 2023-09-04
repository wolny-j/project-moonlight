using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance;
    [SerializeField] GameObject craftingPanel;

    [SerializeField] public Button healthContainerButton;
    [SerializeField] public Button healthPotionButton;
    [SerializeField] public Button stringButton;
    [SerializeField] public Button inventoryUpgradeButton;
    [SerializeField] public Button chestUpgradeButton;
    [SerializeField] public Button pickaxeButton;
    [SerializeField] public Button dynamiteButton;
    [SerializeField] public Button shootSpeedButton;
    [SerializeField] public Button shootSizeButton;

    [SerializeField] GameObject backpackRecipit1;
    [SerializeField] GameObject backpackRecipit2;
    [SerializeField] GameObject healthContainerRecipit1;
    [SerializeField] GameObject healthContainerRecipit2;
    [SerializeField] GameObject chestRecipit1;
    [SerializeField] GameObject chestRecipit2;

    [SerializeField] Animator animator;


    private Dictionary<string, int> itemCounters = new Dictionary<string, int>();

    private bool isOpened= false;

    public bool isGoldBarUsed = false;

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
        if (!isGoldBarUsed)
        {
            if (PlayerStats.Instance.healthContainers > 4)
            {
                Inventory.Instance.SearchAndRemove(ItemsList.Instance.brain);
                Inventory.Instance.SearchAndRemove(ItemsList.Instance.poppy);
            }
            else
            {
                Inventory.Instance.SearchAndRemove(ItemsList.Instance.brain);
            }
        }

        isGoldBarUsed = false;
        HealthUIManager.Instance.AddHealthContainer();
        CheckInventory();
        UpdateButtons();
        UpdateRecipits();
    }
    public void CraftHelthPotion()
    {
        if (!isGoldBarUsed)
        { 
            Inventory.Instance.SearchAndRemove(ItemsList.Instance.eye);
            Inventory.Instance.SearchAndRemove(ItemsList.Instance.dandelion);
        }
        
        Inventory.Instance.AddItem(ItemsList.Instance.healthPotion);
        CheckInventory();
        UpdateButtons();
        UpdateRecipits();
    }
    public void CraftString()
    {
        if (!isGoldBarUsed)
        {
        
            Inventory.Instance.SearchAndRemove(ItemsList.Instance.web);
            Inventory.Instance.SearchAndRemove(ItemsList.Instance.web);
        }

        isGoldBarUsed = false;
        Inventory.Instance.AddItem(ItemsList.Instance.stringItem);
        CheckInventory();
        UpdateButtons();
        UpdateRecipits();
    }

    public void UpgradeInventory() 
    {
        if (!isGoldBarUsed)
        {
            if (Inventory.Instance.space > 3)
            {
                Inventory.Instance.SearchAndRemove(ItemsList.Instance.shell);
                Inventory.Instance.SearchAndRemove(ItemsList.Instance.stringItem);
            }
            else
            {
                Inventory.Instance.SearchAndRemove(ItemsList.Instance.stringItem);
            }
        }

        isGoldBarUsed = false;
        
        Inventory.Instance.UpgradeInventory();
        CheckInventory();
        UpdateButtons();
        UpdateRecipits();
    }

    public void UpgradeChest()
    {
        if (!isGoldBarUsed)
        {
            if (Inventory.Instance.space > 2)
            {
                Inventory.Instance.SearchAndRemove(ItemsList.Instance.bamboo);
                Inventory.Instance.SearchAndRemove(ItemsList.Instance.eye);
            }
            else
                Inventory.Instance.SearchAndRemove(ItemsList.Instance.bamboo);
        }

        isGoldBarUsed = false;
        ChestInventory.Instance.UpgradeInventory();
        CheckInventory();
        UpdateButtons();
        UpdateRecipits();
    }

    public void CraftPickaxe()
    {
        if (!isGoldBarUsed)
        {
            Inventory.Instance.SearchAndRemove(ItemsList.Instance.bamboo);
            Inventory.Instance.SearchAndRemove(ItemsList.Instance.stringItem);
            Inventory.Instance.SearchAndRemove(ItemsList.Instance.shell);
        }

        isGoldBarUsed = false;
        PlayerStats.Instance.AddPickaxe();
        CheckInventory();
        UpdateButtons();
        UpdateRecipits();
    }

    public void CraftDynamite()
    {
        if (!isGoldBarUsed)
        { 
            Inventory.Instance.SearchAndRemove(ItemsList.Instance.poppy);
            Inventory.Instance.SearchAndRemove(ItemsList.Instance.gunpowder);
        }

        isGoldBarUsed = false;
        PlayerStats.Instance.dynamiteCounter += 3;
        UseDynamite.Instance.UpdateCounterUI();
        CheckInventory();
        UpdateButtons();
        UpdateRecipits();
    }

    public void UpgradeShootSpeed()
    {
        if (!isGoldBarUsed)
        { 
            Inventory.Instance.SearchAndRemove(ItemsList.Instance.poppy);
            Inventory.Instance.SearchAndRemove(ItemsList.Instance.dandelion);
        }

        isGoldBarUsed = false;
        PlayerStats.Instance.shootSpeed += 0.5f;
        CheckInventory();
        UpdateButtons();
        UpdateRecipits();
    }

    public void UpgradeShootSize()
    {
        if (!isGoldBarUsed)
        {
            Inventory.Instance.SearchAndRemove(ItemsList.Instance.brain);
            Inventory.Instance.SearchAndRemove(ItemsList.Instance.eye);
        }

        isGoldBarUsed = false;
        PlayerStats.Instance.shootSize += 0.5f;
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
            Debug.Log(GetItemCount("Gold bar"));
        }
    }

    public int GetItemCount(string itemName)
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
        if (!isGoldBarUsed)
        {
            if (PlayerStats.Instance.healthContainers > 4)
                healthContainerButton.interactable = GetItemCount("Brain") > 0 && GetItemCount("Poppy") > 0;
            else
                healthContainerButton.interactable = GetItemCount("Brain") > 0;

            if (Inventory.Instance.space > 3)
                inventoryUpgradeButton.interactable = GetItemCount("String") > 0 && GetItemCount("Shell") > 0;
            else
                inventoryUpgradeButton.interactable = GetItemCount("String") > 0;

            if (ChestInventory.Instance.space > 2)
                chestUpgradeButton.interactable = GetItemCount("Eye") > 0 && GetItemCount("Bamboo") > 0;
            else
                chestUpgradeButton.interactable = GetItemCount("Bamboo") > 0;

            healthPotionButton.interactable = GetItemCount("Eye") > 0 && GetItemCount("Dandelion") > 0;
            stringButton.interactable = GetItemCount("Web") >= 2;


            dynamiteButton.interactable = GetItemCount("Poppy") > 0 && GetItemCount("Gunpowder") > 0;
            pickaxeButton.interactable = GetItemCount("String") > 0 && GetItemCount("Bamboo") > 0 && GetItemCount("Shell") > 0;
            shootSizeButton.interactable = GetItemCount("Eye") > 0 && GetItemCount("Brain") > 0;
            shootSpeedButton.interactable = GetItemCount("Dandelion") > 0 && GetItemCount("Poppy") > 0;
        }

        /*if (GetItemCount("Gold bar") > 0)
        {
            healthContainerButton.interactable = true;
            inventoryUpgradeButton.interactable = true;
            chestUpgradeButton.interactable = true;
            healthPotionButton.interactable = true;
            stringButton.interactable = true;
            dynamiteButton.interactable = true;
            pickaxeButton.interactable = true;
            shootSizeButton.interactable = true;
            shootSpeedButton.interactable = true;
        }*/
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

        if(ChestInventory.Instance.space > 2)
        {
            chestRecipit1.SetActive(false);
            chestRecipit2.SetActive(true);
        }
    }

}

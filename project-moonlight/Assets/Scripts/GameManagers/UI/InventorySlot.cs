using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]Item item;
    [SerializeField]Image icon;
    [SerializeField]Button removeButton;
    [SerializeField]Button useButton;
    [SerializeField]Button addToChestButton;
    [SerializeField]Button removeFromChestButton;


    public void CheckState()
    {
        if (item != null && SceneManager.GetActiveScene().name == "HomeScene")
        {
            if (transform.parent.transform.parent.name == "Inventory")
            {
                addToChestButton.interactable = true;
                removeFromChestButton.gameObject.SetActive(false);
            }
            else if (transform.parent.transform.parent.name == "ChestInventory")
            {
                removeFromChestButton.gameObject.SetActive(true);
                removeFromChestButton.interactable = true;
            }
        }
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        removeButton.interactable = true;
        if(item.isUsable)
        {
            useButton.interactable = true;
        }
        icon.enabled= true;
        icon.sprite = item.sprite;
        UpdateCrafting();
    }

    public void ClearItem()
    {
        item = null;
        icon.sprite = null;
        icon.enabled= false;
        removeButton.interactable = false;
        useButton.interactable = false;
        removeFromChestButton.interactable = false;
        addToChestButton.interactable = false;

    }

    public void RemoveButton()
    {
        if (transform.parent.transform.parent.name == "Inventory")
        {
            Inventory.Instance.RemoveItem(item);
        }
        else if (transform.parent.transform.parent.name == "ChestInventory")
        {
            ChestInventory.Instance.RemoveItem(item);
        }
        
        UpdateCrafting();
    }

    private static void UpdateCrafting()
    {
        if (SceneManager.GetActiveScene().name == "HomeScene")
        {
            CraftingManager.Instance.CheckInventory();
            CraftingManager.Instance.UpdateButtons();
        }
    }

    public void UseButton()
    {
        if(item.tag == Item.Tag.Seed)
        {
            if (FieldSegment.currentHighlightedSquare != null && !FieldSegment.currentHighlightedSquare.isGrowing)
            {
                FieldSegment.currentHighlightedSquare.GetSeed(item);
                //FieldSegment.currentHighlightedSquare = null;
                

                if (transform.parent.parent.name == "Inventory")
                    Inventory.Instance.RemoveItem(item);
                else if (transform.parent.parent.name == "ChestInventory")
                    ChestInventory.Instance.RemoveItem(item);

                if (!item.isUsable && item != null)
                {
                    useButton.interactable = false;
                }
            }
        }
        else if(item.tag == Item.Tag.Potion)
        {
            switch (item.name) 
            {
                case "Health Potion":

                    PlayerStats.Instance.health++;
                    HealthUIManager.Instance.AddHealth();

                    Inventory.Instance.RemoveItem(item);

                    break;
                case "Full Health Potion":

                    while(PlayerStats.Instance.health < PlayerStats.Instance.healthContainers)
                    {
                        PlayerStats.Instance.health++;
                        HealthUIManager.Instance.AddHealth();
                    }
                    

                    Inventory.Instance.RemoveItem(item);

                    break;

            }
            if (!item.isUsable && item != null)
            {
                useButton.interactable = false;
            }
        }
        else if(item.tag == Item.Tag.GoldBar)
        {
            if (SceneManager.GetActiveScene().name == "HomeScene")
            {
                CraftingManager.Instance.isGoldBarUsed = true;
                Debug.Log("Pre");
                CraftingManager.Instance.healthContainerButton.interactable = true;
                CraftingManager.Instance.inventoryUpgradeButton.interactable = true;
                CraftingManager.Instance.chestUpgradeButton.interactable = true;
                CraftingManager.Instance.healthPotionButton.interactable = true;
                CraftingManager.Instance.stringButton.interactable = true;
                CraftingManager.Instance.dynamiteButton.interactable = true;
                CraftingManager.Instance.pickaxeButton.interactable = true;
                CraftingManager.Instance.shootSizeButton.interactable = true;
                CraftingManager.Instance.shootSpeedButton.interactable = true;
                Inventory.Instance.RemoveItem(item);
                
                Debug.Log("Done");
            }
        }
        
    }

    public void AddToChest()
    {
        if (ChestInventory.Instance.items.Count < ChestInventory.Instance.space)
        {
            ChestInventory.Instance.AddItem(item);
            Inventory.Instance.RemoveItem(item);
        }
    }

    public void RemoveFromChest()
    {
        if (Inventory.Instance.items.Count < Inventory.Instance.space)
        {
            Inventory.Instance.AddItem(item);
            ChestInventory.Instance.RemoveItem(item);
        }
    }
}

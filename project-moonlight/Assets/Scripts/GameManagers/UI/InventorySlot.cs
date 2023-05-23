using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]Item item;
    [SerializeField]Image icon;
    [SerializeField]Button removeButton;
    [SerializeField]Button useButton;
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
    }

    public void RemoveButton()
    {
        Inventory.Instance.RemoveItem(item);
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
            if (FieldSegment.currentHighlightedSquare != null)
            {
                FieldSegment.currentHighlightedSquare.GetSeed(item);
                Inventory.Instance.RemoveItem(item);
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
            }
        }
        
    }
}

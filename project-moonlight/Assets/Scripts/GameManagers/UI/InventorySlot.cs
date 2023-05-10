using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }
    public void UseButton()
    {
        if(FieldSegment.currentHighlightedSquare != null)
        {
            FieldSegment.currentHighlightedSquare.GetSeed(item);
            Inventory.Instance.RemoveItem(item);
        }
    }
}

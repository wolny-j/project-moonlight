using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item;
    [SerializeField]Image icon;
    [SerializeField]Button removeButton;
    public void AddItem(Item newItem)
    {
        item = newItem;
        removeButton.interactable = true;
        icon.enabled= true;
        icon.sprite = item.sprite;
    }

    public void ClearItem()
    {
        item = null;
        icon.sprite = null;
        icon.enabled= false;
        removeButton.interactable = false;
    }

    public void RemoveButton()
    {
        Inventory.Instance.RemoveItem(item);
    }
}

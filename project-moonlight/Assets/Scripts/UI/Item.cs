using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name;
    public Sprite sprite;
    public bool isUsable;

}

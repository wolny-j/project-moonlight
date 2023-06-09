using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(fileName ="NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name;
    public Sprite sprite;
    public bool isUsable;
    public Tag tag;
    public enum Tag
    {
        EnemyDrop,
        Seed,
        Potion
    }

    public List<Sprite> growingSprites;

}



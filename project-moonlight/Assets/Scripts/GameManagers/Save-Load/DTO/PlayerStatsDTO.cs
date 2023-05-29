using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[System.Serializable]
public class PlayerStatsDTO
{
    public int health;
    public int healthContainers;
    public float power;
    public float speed;
    public float shootFrequency;
    public float level;
    public int inventorySpace;
    public bool hasPickaxe;
    public int pickaxeDurability;
    public int dynamiteCounter;

    public List<string> items = new();

    public Dictionary<string, int> powerups = new Dictionary<string, int>()
    {
        {"SpeedGem", 0},
        {"PowerGem", 0},
        {"ShootGem", 0}
    };

    

    public PlayerStatsDTO(PlayerStats stats, Inventory inventory)
    {
        health= stats.health;
        healthContainers = stats.healthContainers;

        power= stats.power;
        speed= stats.speed;
        shootFrequency= stats.shootFrequency;
        level = stats.level;
        inventorySpace = inventory.space;

        hasPickaxe = stats.pickaxe1.hasPickaxe;
        pickaxeDurability = stats.pickaxe1.durability;

        foreach (Item item in inventory.items)
        {
            items.Add(item.name);
        }

        powerups = stats.powerups;
        dynamiteCounter = stats.dynamiteCounter;
    }

    public PlayerStatsDTO()
    {
        health = 4;
        healthContainers = 4;
        inventorySpace = 3;

        power = 2f;
        speed = 0.6f;
        shootFrequency = 0.5f;
        level = 1;

        hasPickaxe = false;
        pickaxeDurability = 0;
        dynamiteCounter = 0;
    }
}

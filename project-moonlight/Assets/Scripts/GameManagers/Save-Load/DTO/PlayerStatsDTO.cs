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
    public float luck;
    public int inventorySpace;
    public bool hasPickaxe;
    public int pickaxeDurability;
    public int dynamiteCounter;
    public float shootSpeed;
    public float shootSize;
    public bool bouncingSpellPowerUp;
    public bool toxicTracePowerUp;
    public bool wingsPowerUp;
    public bool shieldPowerUp;
    public bool goldenHeartPowerUp;


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
        luck = stats.luck;
        inventorySpace = inventory.space;

        hasPickaxe = stats.pickaxe1.hasPickaxe;
        pickaxeDurability = stats.pickaxe1.durability;

        foreach (Item item in inventory.items)
        {
            items.Add(item.name);
        }

        powerups = stats.powerups;
        dynamiteCounter = stats.dynamiteCounter;

        shootSize= stats.shootSize;
        shootSpeed = stats.shootSpeed;

        bouncingSpellPowerUp = stats.bouncingSpellPowerUp;
        toxicTracePowerUp = stats.toxicTracePowerUp;
        wingsPowerUp = stats.wingsPowerup;
        shieldPowerUp = stats.shieldPowerup;
        goldenHeartPowerUp = stats.goldenHeartPowerUp;
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
        luck = 0;

        hasPickaxe = false;
        pickaxeDurability = 0;
        dynamiteCounter = 0;

        shootSpeed = 1;
        shootSize = 1;

        bouncingSpellPowerUp = false;
        toxicTracePowerUp = false;
        wingsPowerUp = false;
        shieldPowerUp = false;
        goldenHeartPowerUp = false;
    }
}

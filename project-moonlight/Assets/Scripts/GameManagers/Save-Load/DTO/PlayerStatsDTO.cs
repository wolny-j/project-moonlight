using System;
using System.Collections;
using System.Collections.Generic;
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

        foreach (Item item in inventory.items)
        {
            items.Add(item.name);
        }

        powerups = stats.powerups;
    }

    public PlayerStatsDTO()
    {
        health = 4;
        healthContainers = 4;
        inventorySpace = 4;

        power = 2f;
        speed = 0.6f;
        shootFrequency = 0.5f;
        level = 1;
    }
}

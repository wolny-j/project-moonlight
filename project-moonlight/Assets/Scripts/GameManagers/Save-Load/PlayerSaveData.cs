using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    public int health;
    public float power;
    public float speed;
    public float shootFrequency;

    public List<string> items = new();

    public Dictionary<string, int> powerups = new Dictionary<string, int>()
    {
        {"SpeedGem", 0},
        {"PowerGem", 0},
        {"ShootGem", 0}
    };

    

    public PlayerSaveData(PlayerStats stats, Inventory inventory)
    {
        health= stats.health;

        power= stats.power;
        speed= stats.speed;
        shootFrequency= stats.shootFrequency;

        foreach (Item item in inventory.items)
        {
            items.Add(item.name);
        }

        powerups = stats.powerups;
    }

    public PlayerSaveData()
    {
        health = 8;

        power = 2f;
        speed = 0.6f;
        shootFrequency = 0.5f;
    }
}

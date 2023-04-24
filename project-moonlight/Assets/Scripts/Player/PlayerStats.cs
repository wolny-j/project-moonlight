using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    [SerializeField] Text powerText;
    [SerializeField] Text speedText;
    [SerializeField] Text shootText;

    private float basePower { get; set; } = 2;
    private float baseSpeed { get; set; } = 0.6f;
    private float baseShootFrequency { get; set; } = 0.5f;
    public bool isMapObtained { get; set; } = false;

    private const float SPEED_MULTIPLAYER = 0.2f;
    private const float POWER_MULTIPLAYER = 0.5f;
    private const float SHOOT_FREQUENCY_MULTIPLAYER = 0.05f;

    //PLAYER STATTSTICS
    public int health { get; set; } = 8;
    public float power { get; set; } = 2;
    public float speed { get; set; } = 0.6f;
    public float shootFrequency { get; set; } = 0.4f;
   



    //Dicronary that counts each powerup is collected e.g. ["SpeedGem", 3] means that player collected three speed powerups
    private readonly Dictionary<string, int> powerups = new Dictionary<string, int>()
    {
        {"SpeedGem", 0},
        {"PowerGem", 0},
        {"ShootGem", 0}
    };

    void Start()
    {
        if(Instance != null) 
        {
            Debug.LogWarning("More than one instance if Inventory found.");
            return;
        }

        Instance= this;
        HealthUIManager.Instance.AddHealth(health);
    }



    public void AddPowerup(string key)
    {
        powerups[key] += 1;
        UpdatePowerups();
    }

    public void UpdatePowerups()
    {
        powerups.TryGetValue("SpeedGem", out int speedGems);
        powerups.TryGetValue("PowerGem", out int powerGems);
        powerups.TryGetValue("ShootGem", out int shootGems);

        speed = baseSpeed + (speedGems * SPEED_MULTIPLAYER);
        power = basePower + (powerGems * POWER_MULTIPLAYER);
        shootFrequency = baseShootFrequency - (shootGems * SHOOT_FREQUENCY_MULTIPLAYER);

        speedText.text = speedGems.ToString();
        powerText.text = powerGems.ToString();
        shootText.text = shootGems.ToString();
    }

}

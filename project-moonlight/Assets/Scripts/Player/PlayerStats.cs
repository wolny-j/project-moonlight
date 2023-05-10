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

    [SerializeField] GameObject map;

    private float basePower { get; set; } = 2;
    private float baseSpeed { get; set; } = 0.6f;
    private float baseShootFrequency { get; set; } = 0.5f;

    private const float SPEED_MULTIPLAYER = 0.2f;
    private const float POWER_MULTIPLAYER = 0.5f;
    private const float SHOOT_FREQUENCY_MULTIPLAYER = 0.05f;

    public bool IsCompleted { get; set; } = true;

    //PLAYER STATTSTICS
    public int health { get; set; } = 8;
    public float power { get; set; }
    public float speed { get; set; }
    public float shootFrequency { get; set; }


    //Dicronary that counts each powerup is collected e.g. ["SpeedGem", 3] means that player collected three speed powerups
    public Dictionary<string, int> powerups = new Dictionary<string, int>()
    {
        {"SpeedGem", 0},
        {"PowerGem", 0},
        {"ShootGem", 0}
    };

    private void Awake()
    {
        InitializeStats();
        InitializeUI();
        
    }

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        Load();
        //HealthUIManager.Instance.AddHealth(health);
    }

    private void InitializeUI()
    {
        powerText = GameObject.Find("PowerCounter").GetComponent<Text>();
        shootText = GameObject.Find("FrequencyCounter").GetComponent<Text>();
        speedText = GameObject.Find("SpeedCounter").GetComponent<Text>();
    }

    private void InitializeStats()
    {
        power = basePower;
        speed = baseSpeed;
        shootFrequency = baseShootFrequency;
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

    public void UnlockMap()
    {
        map.SetActive(true);
    }

    public void Load()
    {
        PlayerSaveData data = SaveSystem.LoadPlayer();
        LoadInventory.Instance.Load(data);

        powerups = data.powerups;
        health = data.health;
        speed = data.speed;
        power = data.power;
        shootFrequency = data.shootFrequency;

        UpdatePowerups();
        HealthUIManager.Instance.SubtractHealth(health);

    }
}

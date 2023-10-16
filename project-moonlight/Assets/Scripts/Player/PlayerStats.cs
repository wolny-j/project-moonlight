using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    [Header("UI objects")]
    [SerializeField] Text powerText;
    [SerializeField] Text speedText;
    [SerializeField] Text shootText;

    [SerializeField] GameObject map;
    [SerializeField] GameObject pickaxeUI;
    [SerializeField] GameObject shieldUI;
    [SerializeField] GameObject goldenHeartUI;
    [SerializeField] Slider pickaxeDurabilitySlider;
    [SerializeField] Image pickaxeDurabilityFillRect;

    [Header("Shrine powerups")]
    public bool bouncingSpellPowerUp = false;
    public bool toxicTracePowerUp = false;
    public bool wingsPowerup = false;
    public bool shieldPowerup = false;
    public bool goldenHeartPowerUp = false;

    public bool isShieldActive = false;

    private float basePower { get; set; } = 2;
    private float baseSpeed { get; set; } = 0.6f;
    private float baseShootFrequency { get; set; } = 0.5f;

    private const float SPEED_MULTIPLAYER = 0.2f;
    private const float POWER_MULTIPLAYER = 0.5f;
    private const float SHOOT_FREQUENCY_MULTIPLAYER = 0.05f;

    public bool IsCompleted { get; set; } = true;

    [Header("Player stats")]
    //PLAYER STATTSTICS
    public int health = 4;
    public int healthContainers = 4;
    public int maxHealth { get; set; } = 18;
    public float power;
    public float speed { get; set; }
    public float shootFrequency { get; set; }
    public float level = 1;
    public float luck = 1;

    public int dynamiteCounter = 0;

    public float shootSize = 1;
    public float shootSpeed = 1;

    public struct pickaxe
    {
       public bool hasPickaxe;
       public int durability;
    }
    public pickaxe pickaxe1;


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
        pickaxe1.hasPickaxe = false;
        pickaxe1.durability = 0;
        
    }

    public void CheckDeath()
    {
        if (health <= 0 && !goldenHeartPowerUp)
        {
            PlayerStatsDTO saveData = new();
            SaveSystem.SavePlayer(saveData);
            ChestDTO chestData = new();
            SaveSystem.SaveChest(chestData);

            SaveSystem.DeleteFields();
            SceneManager.LoadScene(0);
        }
        else if(health <= 0 & goldenHeartPowerUp)
        {
            goldenHeartPowerUp = false;
            health++;
            HealthUIManager.Instance.AddHealth();
            SaveSystem.BuildSaveObject(PlayerStats.Instance, Inventory.Instance);
            SceneManager.LoadScene(2);
        }
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

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            if (isShieldActive)
            {
                shieldUI.SetActive(true);
            }
            else
            {
                shieldUI.SetActive(false);
            }

            if (goldenHeartPowerUp)
                goldenHeartUI.SetActive(true);
            else
                goldenHeartUI.SetActive(false);
        }
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
        PlayerStatsDTO data = SaveSystem.LoadPlayer();
        LoadInventory.Instance.InventoryLoad(data);
        if(SceneManager.GetActiveScene().name == "HomeScene")
        {
            ChestDTO chestData = SaveSystem.LoadChest();
            LoadInventory.Instance.ChestLoad(chestData);
        }
        
        level = data.level;
        luck = data.luck;
        powerups = data.powerups;
        health = data.health;
        healthContainers = data.healthContainers;
        speed = data.speed;
        power = data.power;
        shootFrequency = data.shootFrequency;
        pickaxe1.hasPickaxe = data.hasPickaxe;
        pickaxe1.durability = data.pickaxeDurability;
        dynamiteCounter = data.dynamiteCounter;

        shootSpeed= data.shootSpeed;
        shootSize = data.shootSize;

        UseDynamite.Instance.UpdateCounterUI();
        if (pickaxe1.hasPickaxe)
        {
            pickaxeUI.SetActive(true);
            pickaxeDurabilitySlider.value = pickaxe1.durability;
        }

        UpdatePowerups();
        HealthUIManager.Instance.InitializeHearth(healthContainers);

        bouncingSpellPowerUp = data.bouncingSpellPowerUp;
        toxicTracePowerUp = data.toxicTracePowerUp;
        wingsPowerup = data.wingsPowerUp;
        shieldPowerup = data.shieldPowerUp;
        goldenHeartPowerUp = data.goldenHeartPowerUp;

        if (shieldPowerup)
        {
            isShieldActive = true;
        }

    }

    public void AddPickaxe()
    {
        pickaxe1.hasPickaxe = true;
        pickaxe1.durability = 25;
        pickaxeUI.SetActive(true);
        pickaxeDurabilitySlider.value = pickaxe1.durability;
    }

    public void UpdatePickaxe() 
    {
        pickaxe1.durability--;
        pickaxeDurabilitySlider.value = pickaxe1.durability;
        pickaxeDurabilityFillRect.color = Color.green;

        if (pickaxe1.durability == 0)
        {
            pickaxeUI.SetActive(false);
            pickaxe1.hasPickaxe = false;
        }
        else if(pickaxe1.durability < 12 && pickaxe1.durability > 5)
        {
            pickaxeDurabilityFillRect.color = Color.yellow;
        }
        else if(pickaxe1.durability <= 5)
        {
            pickaxeDurabilityFillRect.color = Color.red;
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    private float basePower { get; set; } = 2;
    private float baseSpeed { get; set; } = 0.6f;
    private float baseShootFrequency { get; set; } = 0.4f;

    public int health { get; set; } = 8;
    public float power { get; set; } = 2;
    public float speed { get; set; } = 0.6f;
    public float shootFrequency { get; set; } = 0.4f;
    public bool isMapObtained { get; set; } = false;

    [SerializeField] Text powerText;
    [SerializeField] Text speedText;
    [SerializeField] Text shootText;

    //Dicronary that counts each powerup is collected e.g. ["SpeedGem", 3] means that player collected three speed powerups
    private Dictionary<string, int> powerups= new Dictionary<string, int>();


    void Start()
    {
        if(Instance != null) 
        {
            Debug.LogWarning("More than one instance if Inventory found.");
            return;
        }
        Instance= this;
        InitlializePowerupDict();
        HealthUIManager.Instance.AddHealth(health);
    }

    public void InitlializePowerupDict()
    {
        powerups.Add("SpeedGem", 0);
        powerups.Add("PowerGem", 0);
        powerups.Add("ShootGem", 0);
    }

    public void AddPowerup(string key)
    {
        powerups[key] += 1;
        UpdatePowerups();
    }

    public void UpdatePowerups()
    {
        speed = baseSpeed + ((powerups.GetValueOrDefault("SpeedGem") * 0.2f));
        power = basePower + ((powerups.GetValueOrDefault("PowerGem") * 0.5f));
        shootFrequency = baseShootFrequency - (powerups.GetValueOrDefault("ShootGem") * 0.05f);

        speedText.text = powerups.GetValueOrDefault("SpeedGem").ToString();
        powerText.text = powerups.GetValueOrDefault("PowerGem").ToString();
        shootText.text = powerups.GetValueOrDefault("ShootGem").ToString();
    }

}

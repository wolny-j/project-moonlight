using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public int health { get; set; } = 8;
    public bool isMapObtained { get; set; } = false;

    void Awake()
    {
        if(Instance != null) 
        {
            Debug.LogWarning("More than one instance if Inventory found.");
            return;
        }
        Instance= this;

        HealthUIManager.Instance.AddHealth(health);
    }
}

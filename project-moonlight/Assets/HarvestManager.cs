using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestManager : MonoBehaviour
{
    public static HarvestManager Instance;
    [SerializeField] Item poppy;
    [SerializeField] Item dandelion;
    void Awake()
    {
        if (Instance == null)
        {
            HarvestManager.Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    public bool HarvestPoppy()
    {
        bool result = Inventory.Instance.AddItem(poppy);
        return result;
    }
    public bool HarvestDandelion()
    {
        bool result = Inventory.Instance.AddItem(dandelion);
        return result;
        
    }
    void OnApplicationQuit()
    {
        Debug.Log("Saving");
        PlayerStatsDTO saveData = new();
        SaveSystem.SavePlayer(saveData);
    }
}

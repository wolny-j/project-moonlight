using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestManager : MonoBehaviour
{
    public static HarvestManager Instance;
    [SerializeField] Item poppy;
    [SerializeField] Item dandelion;
    [SerializeField] Item bamboo;
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
    public bool HarvestBamboo()
    {
        bool result = Inventory.Instance.AddItem(bamboo);
        return result;

    }
    public bool HarvestClover()
    {
        PlayerStats.Instance.luck++;
        return true;

    }
    void OnApplicationQuit()
    {
        PlayerStatsDTO saveData = new();
        ChestDTO chestData = new();
        SaveSystem.SaveChest(chestData);
        SaveSystem.SavePlayer(saveData);
    }
}

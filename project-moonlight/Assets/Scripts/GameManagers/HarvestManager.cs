using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestManager : MonoBehaviour
{
    public static HarvestManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        /*if(PlayerStats.Instance.level == 1) 
        {
            FieldManager.Instance.RestetFields();
        }*/
    }
    public bool HarvestPoppy()
    {
        bool result = Inventory.Instance.AddItem(ItemsList.Instance.poppy);
        return result;
    }
    public bool HarvestDandelion()
    {
        bool result = Inventory.Instance.AddItem(ItemsList.Instance.dandelion);
        return result;
        
    }
    public bool HarvestBamboo()
    {
        bool result = Inventory.Instance.AddItem(ItemsList.Instance.bamboo);
        return result;

    }

    public bool HarvestStarfruit()
    {
        bool result = Inventory.Instance.AddItem(ItemsList.Instance.starfruit);
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

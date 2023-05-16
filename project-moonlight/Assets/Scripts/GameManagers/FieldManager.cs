using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    private FieldsListSaveData saveData = new FieldsListSaveData();

    public static FieldManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }


    public void Add(HomeFieldSaveData field)
    {
        if(saveData == null)
            saveData = new FieldsListSaveData();
        else
            saveData.Add(field);
    }

    public FieldsListSaveData GetFields()
    {
        if (saveData == null)
            return new FieldsListSaveData();
        return saveData;
    }

    private void OnApplicationQuit()
    {
        saveData = null;
        SaveSystem.SaveHarvestField();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    private FieldsListDTO saveData = new FieldsListDTO();

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


    public void Add(HomeFieldDTO field)
    {
        if(saveData == null)
            saveData = new FieldsListDTO();
        else
            saveData.Add(field);
    }

    public FieldsListDTO GetFields()
    {
        if (saveData == null)
            return new FieldsListDTO();
        return saveData;
    }

    private void OnApplicationQuit()
    {
        saveData = null;
        SaveSystem.SaveHarvestField();
    }
}

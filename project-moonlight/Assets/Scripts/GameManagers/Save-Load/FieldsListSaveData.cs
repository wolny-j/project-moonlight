using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FieldsListSaveData
{
    public List<HomeFieldSaveData> fields = new List<HomeFieldSaveData>();

    public FieldsListSaveData()
    {

    }

    public void Add(HomeFieldSaveData field)
    {
        fields.Add(field);
    }

    public List<HomeFieldSaveData> GetFields()
    {
        return fields;
    }

}

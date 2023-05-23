using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FieldsListDTO
{
    public List<HomeFieldDTO> fields = new List<HomeFieldDTO>();

    public FieldsListDTO()
    {

    }

    public void Add(HomeFieldDTO field)
    {
        fields.Add(field);
    }

    public List<HomeFieldDTO> GetFields()
    {
        return fields;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HomeFieldDTO
{
    public int growingIndex;
    public bool isGrowing;
    public string name;
    public int fieldIndex;

    public HomeFieldDTO(int _growingIndex, bool _isGrowing, string _name, int _fieldIndex)
    {
        growingIndex = _growingIndex;
        isGrowing = _isGrowing;
        name = _name;
        fieldIndex = _fieldIndex;
    }

    public HomeFieldDTO(string _name, int _fieldIndex) 
    {
        growingIndex = 0;
        isGrowing= false;
        name = _name;
        fieldIndex = _fieldIndex;
    }
}

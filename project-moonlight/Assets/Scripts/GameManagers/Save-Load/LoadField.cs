using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class LoadField : MonoBehaviour
{
    [SerializeField] List<GameObject> fields = new List<GameObject>();
    [SerializeField] Item poppySeed;
    [SerializeField] Item dandelionSeed;
    [SerializeField] Item bambooSeed;
    [SerializeField] Item cloverSeed;
    [SerializeField] Item weed;
    [SerializeField] Item starfruidSeed;
    public static LoadField Instance;

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
    private void Start()
    {
        var data = SaveSystem.LoadField();
        Load(data);
    }

    public void Load(FieldsListDTO data)
    {
        if (data != null)
        {
            foreach (HomeFieldDTO field in data.GetFields())
            {
                if (field != null)
                {
                    FieldSegment fieldTemp;
                    switch (field.name)
                    {
                        case "Poppy Seed":
                            fieldTemp = fields[field.fieldIndex].GetComponent<FieldSegment>();
                            fieldTemp.GetSeed(poppySeed);
                            for (int i = 0; i <= field.growingIndex; i++)
                                fieldTemp.Grow();
                            break;
                        case "Dandelion Seed":
                            fieldTemp = fields[field.fieldIndex].GetComponent<FieldSegment>();
                            fieldTemp.GetSeed(dandelionSeed);
                            for (int i = 0; i <= field.growingIndex; i++)
                                fieldTemp.Grow();
                            break;
                        case "Bamboo Seed":
                            fieldTemp = fields[field.fieldIndex].GetComponent<FieldSegment>();
                            fieldTemp.GetSeed(bambooSeed);
                            for (int i = 0; i <= field.growingIndex; i++)
                                fieldTemp.Grow();
                            break;
                        case "Clover Seed":
                            fieldTemp = fields[field.fieldIndex].GetComponent<FieldSegment>();
                            fieldTemp.GetSeed(cloverSeed);
                            for (int i = 0; i <= field.growingIndex; i++)
                                fieldTemp.Grow();
                            break;
                        case "Weed":
                            fieldTemp = fields[field.fieldIndex].GetComponent<FieldSegment>();
                            fieldTemp.GetSeed(weed);
                            for (int i = 0; i <= field.growingIndex; i++)
                                fieldTemp.Grow();
                            break;
                        case "Starfruit Seed":
                            fieldTemp = fields[field.fieldIndex].GetComponent<FieldSegment>();
                            fieldTemp.GetSeed(starfruidSeed);
                            for (int i = 0; i <= field.growingIndex; i++)
                                fieldTemp.Grow();
                            break;
                        case null:
                            break;
                    }
                }
            }
        }
    }

}

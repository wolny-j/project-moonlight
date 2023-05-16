using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class LoadField : MonoBehaviour
{
    [SerializeField] List<GameObject> fields = new List<GameObject>();
    [SerializeField] Item poppySeed;
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

    public void Load(FieldsListSaveData data)
    {
        if (data != null)
        {
            foreach (HomeFieldSaveData field in data.GetFields())
            {
                if (field != null)
                {
                    switch (field.name)
                    {
                        case "Poppy Seed":
                            FieldSegment fieldTemp = fields[field.fieldIndex].GetComponent<FieldSegment>();
                            fieldTemp.GetSeed(poppySeed);
                            Debug.Log(field.growingIndex);
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

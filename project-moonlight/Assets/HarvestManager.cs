using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestManager : MonoBehaviour
{
    public static HarvestManager Instance;
    [SerializeField] Item poppy;
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
    public void HarvestPoppy()
    {
        Inventory.Instance.AddItem(poppy);
    }
}

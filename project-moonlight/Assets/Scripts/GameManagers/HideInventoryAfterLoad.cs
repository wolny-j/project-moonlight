using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInventoryAfterLoad : MonoBehaviour
{
    [SerializeField] GameObject inventory;

    private void Start()
    {
        inventory.SetActive(false);
    }
}

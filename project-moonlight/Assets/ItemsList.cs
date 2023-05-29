using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsList : MonoBehaviour
{
    public static ItemsList Instance;

    [SerializeField] public Item brain;
    [SerializeField] public Item poppy;
    [SerializeField] public Item eye;
    [SerializeField] public Item dandelion;
    [SerializeField] public Item healthPotion;
    [SerializeField] public Item stringItem;
    [SerializeField] public Item shell;
    [SerializeField] public Item web;
    [SerializeField] public Item bamboo;
    [SerializeField] public Item gunpowder;
    void Awake()
    {
        if (Instance == null)
        {
            ItemsList.Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}

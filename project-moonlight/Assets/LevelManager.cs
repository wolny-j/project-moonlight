using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool isMapSpawned { get; set; } = false;
    public int mapDropChance { get; set; } = 92;
    [SerializeField] public GameObject map;
    [SerializeField] public GameObject heart;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

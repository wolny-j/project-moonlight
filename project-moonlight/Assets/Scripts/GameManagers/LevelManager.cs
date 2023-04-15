using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public bool isMapSpawned { get; set; } = false;
    public int mapDropChance { get; set; } = 92;
    public int brainDropChance { get; set; } = 80;
    public int eyeDropChance { get; set; } = 80;
    [SerializeField] public GameObject map;
    [SerializeField] public GameObject heart;
    [SerializeField] public GameObject brain;
    [SerializeField] public GameObject eye;
    void Awake()
    {
        Instance = this;
    }

}

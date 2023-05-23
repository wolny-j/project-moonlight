using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public bool isMapSpawned { get; set; } = false;
    public int mapDropChance { get; set; } = 92;
    public int brainDropChance { get; set; } = 85;
    public int eyeDropChance { get; set; } = 90;
    public int shellDropChance { get; set; } = 92;
    public int heartDropChance { get; set; } = 92;
    public int webDropChance { get; set; } = 78;

    [SerializeField] public GameObject map;
    [SerializeField] public GameObject heart;
    [SerializeField] public GameObject brain;
    [SerializeField] public GameObject eye;
    [SerializeField] public GameObject shell;
    [SerializeField] public GameObject web;

    [SerializeField] public GameObject eyeEnemy;
    [SerializeField] public GameObject zombieEnemy;
    [SerializeField] public GameObject snailEnemy;
    [SerializeField] public GameObject shooterEnemy;
    [SerializeField] public GameObject coreEnemy;
    [SerializeField] public GameObject spiderEnemy;
    void Awake()
    {
        Instance = this;
    }

    void OnApplicationQuit()
    {
        PlayerStatsDTO saveData = new();
        SaveSystem.SavePlayer(saveData);
    }

}

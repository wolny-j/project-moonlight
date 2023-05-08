using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    private PlayerStats playerStats;
    private bool isEnemySpawned = false;
    private List<GameObject> enemyList = new List<GameObject>();

    public bool isStartintgSegment { get; set; } = false;
    public bool isCompleted { get; set; } = false;

    private bool isUpdated = false;
    //REFACTOR to one instance in the whole game attached to SpawnManager and pass transform as parameter
    private void Awake()
    {
        playerStats = GameObject.Find("PlayerStatsManager").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (NoEnemiesLeft() && isEnemySpawned == true && !isUpdated)
        {
            isCompleted = true;
            playerStats.IsCompleted = true;
            isUpdated = true;
        }
    }


    private bool NoEnemiesLeft()
    {
        foreach (GameObject enemy in enemyList)
        {
            if (enemy != null)
            {
                return false;
            }
        }

        // If the loop completes, then no enemies were found, so return true
        return true;
    }

    public void SpawnEnemies(bool isInitial)
    {
        //If enemy is already spawned break the function
        if (isEnemySpawned && isInitial)
        {
            return;
        }
        int enemyCount;
        if (isInitial)
        {
             enemyCount = UnityEngine.Random.Range(2, 5);
        }
        else
        {
            enemyCount = UnityEngine.Random.Range(1, 3);
        }    
       
        for (int i = 0; i < enemyCount; i++)
        {
            int enemyType = UnityEngine.Random.Range(1, 7);
            

            //Spawn enemy based on random given type
            switch (enemyType)
            {
                case 1:
                    SpawnEnemiesOfType(LevelManager.Instance.eyeEnemy, isInitial);
                    break;
                case 2:
                    if (isInitial)
                        SpawnEnemiesOfType(LevelManager.Instance.zombieEnemy, isInitial);
                    else
                        SpawnEnemiesOfType(LevelManager.Instance.eyeEnemy, isInitial);
                    break;
                case 3:
                    SpawnEnemiesOfType(LevelManager.Instance.snailEnemy, isInitial);
                    break;
                case 4:
                    SpawnEnemiesOfType(LevelManager.Instance.shooterEnemy, isInitial);
                    break;
                case 5:
                    if(isInitial)
                        SpawnEnemiesOfType(LevelManager.Instance.coreEnemy, isInitial);
                    else
                        SpawnEnemiesOfType(LevelManager.Instance.eyeEnemy, isInitial);
                    break;
                default:
                    SpawnEnemiesOfType(LevelManager.Instance.eyeEnemy, isInitial);
                    break;
            }
        }
        if(isInitial)
            isEnemySpawned = true;
    }

    //Loop for enemyCount and spawn each of them in a random position. Next add it to the list (list is used to check if all enemies are dead in NoEnemiesLeft function).
    private void SpawnEnemiesOfType(GameObject enemyPrefab, bool isInitial)
    {
        Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f), 0f);
        GameObject enemy = Instantiate(enemyPrefab, transform);
        enemy.transform.localPosition = spawnPosition;
        enemyList.Add(enemy);
        if (!isInitial)
        {
            enemy.GetComponent<EnemyDropItem>().IsDropingItem = false;
        }
    }

}

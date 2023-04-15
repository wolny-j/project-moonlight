using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject eyeEnemy;
    [SerializeField] private GameObject zombieEnemy;
    [SerializeField] private GameObject snailEnemy;

    private bool isEnemySpawned = false;
    private List<GameObject> enemyList = new List<GameObject>();

    public bool isStartintgSegment { get; set; } = false;
    public bool isCompleted { get; set; } = false;
    // Update is called once per frame
    void Update()
    {
        if (NoEnemiesLeft() && isEnemySpawned == true)
        {
            isCompleted = true;
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

    public void SpawnEnemies()
    {
        //If enemy is already spawned break the function
        if (isEnemySpawned)
        {
            return;
        }

        int enemyType = UnityEngine.Random.Range(1, 4);
        int enemyCount;

        //Spawn enemy based on random given type
        switch (enemyType)
        {
            case 1:
                enemyCount = UnityEngine.Random.Range(1, 6);
                SpawnEnemiesOfType(eyeEnemy, enemyCount);
                break;
            case 2:
                enemyCount = UnityEngine.Random.Range(1, 3);
                SpawnEnemiesOfType(zombieEnemy, enemyCount);
                break;
            case 3:
                enemyCount = UnityEngine.Random.Range(1, 3);
                SpawnEnemiesOfType(snailEnemy, enemyCount);
                break;
            default:
                enemyCount = 1;
                SpawnEnemiesOfType(eyeEnemy, enemyCount);
                break;
        }

        isEnemySpawned = true;
    }

    //Loop for enemyCount and spawn each of them in a random position. Next add it to the list (list is used to check if all enemies are dead in NoEnemiesLeft function).
    private void SpawnEnemiesOfType(GameObject enemyPrefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f), 0f);
            GameObject enemy = Instantiate(enemyPrefab, transform);
            enemy.transform.localPosition = spawnPosition;
            enemyList.Add(enemy);
        }
    }

}

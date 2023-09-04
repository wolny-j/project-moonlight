using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyOfType : MonoBehaviour
{
    public static SpawnEnemyOfType Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance= this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //Loop for enemyCount and spawn each of them in a random position. Next add it to the list (list is used to check if all enemies are dead in NoEnemiesLeft function).
    public void Spawn(GameObject enemyPrefab, bool isInitial, Transform localTransform, ref List<GameObject> enemyList)
    {
        Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f), 0f);
        GameObject enemy = Instantiate(enemyPrefab, localTransform);
        enemy.transform.localPosition = spawnPosition;
        enemyList.Add(enemy);
        if (!isInitial)
        {
            enemy.GetComponent<EnemyDropItem>().IsDropingItem = false;
        }
    }

    public void SpawnSpiderNextStage(GameObject enemyPrefab, bool isInitial, Transform localTransform, ref List<GameObject> enemyList, Transform spiderTransform)
    {
        Vector3 spawnPosition = spiderTransform.localPosition;
        GameObject enemy = Instantiate(enemyPrefab, localTransform);
        enemy.transform.localPosition = spawnPosition;
        enemyList.Add(enemy);
        if (!isInitial)
        {
            enemy.GetComponent<EnemyDropItem>().IsDropingItem = false;
        }
    }

    public void SpwanJumpingSlime(GameObject enemyPrefab, Transform localTransform)
    {
        Vector3 spawnPosition = new Vector3(0.3f, 0, 1f);
        GameObject enemy = Instantiate(enemyPrefab, localTransform);
        enemy.transform.localPosition = spawnPosition;

        spawnPosition = new Vector3(-0.3f, 0, 1f);
        GameObject enemy2 = Instantiate(enemyPrefab, localTransform);
        enemy2.transform.localPosition = spawnPosition;
    }


}

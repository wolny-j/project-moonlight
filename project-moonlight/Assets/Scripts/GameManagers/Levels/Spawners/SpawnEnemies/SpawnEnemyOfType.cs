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
}

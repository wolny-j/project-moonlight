using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject basicEnemy;
    private bool isCompleted = false;
    private bool isEnemySpawned = false;
    private List<GameObject> enemyList = new List<GameObject>();

    public bool isStartintgSegment { get; set; } = false;
 
    // Update is called once per frame
    void Update()
    { 
        if (NoEnemiesLeft() && isEnemySpawned == true)  
        {
            isCompleted= true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnEnemies();
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

    private void SpawnEnemies()
    {
        if (!isStartintgSegment && !isCompleted)
        {
            System.Random rand = new System.Random();
            int enemyCount;
            int enemyType = rand.Next(1, 3);

            switch(enemyType)
            {
                default:
                    enemyCount = 1;
                    break;
                case 1:
                    enemyCount = rand.Next(5);
                    break;
                case 2:
                    enemyCount = rand.Next(1, 3);
                    break;
            }
           
            for (int i = 0; i < enemyCount; i++)
            {
                GameObject enemy = Instantiate(basicEnemy, transform);
                enemy.transform.localPosition = new(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0);
                enemyList.Add(enemy);
                if (enemyType == 2)
                {
                    BasicEnemy basicEnemy = enemy.GetComponent<BasicEnemy>();
                    basicEnemy.isAiming = true;
                    basicEnemy.speed = 0.15f;
                }
            }

            isEnemySpawned = true;
        }
    }
}

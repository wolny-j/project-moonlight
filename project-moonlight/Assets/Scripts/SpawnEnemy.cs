using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject basicEnemy;
    private bool isCompleted = false;
    private bool isStarted = false;
    private List<GameObject> enemyList = new List<GameObject>();

    public bool isStartintgSegment { get; set; } = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { 
        if (NoEnemiesLeft() && isStarted == true)  
        {
            isCompleted= true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isStartintgSegment)
            {
                if (!isCompleted)
                {
                    System.Random rand = new System.Random();
                    int enemyCount;
                    int enemyType = rand.Next(1, 3);

                    if(enemyType == 1)
                    {
                         enemyCount = rand.Next(5);
                    }
                    else if (enemyType == 2)
                    {
                        enemyCount = rand.Next(2);
                    }
                    else
                    {
                        enemyCount = 0;
                    }
                    for (int i = 0; i < enemyCount; i++)
                    {
                        GameObject enemy = Instantiate(basicEnemy, transform);
                        enemy.transform.localPosition = new(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0);
                        enemyList.Add(enemy);
                        if(enemyType == 2)
                        {
                            enemy.GetComponent<BasicEnemy>().isAiming = true;
                            enemy.GetComponent<BasicEnemy>().speed = 0.15f;
                        }
                    }
                    isStarted = true;
                }
            }
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
}

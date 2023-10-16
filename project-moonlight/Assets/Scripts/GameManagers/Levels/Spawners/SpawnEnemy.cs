using Assets.Scripts.GameManagers.Levels.Spawners;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    private PlayerStats playerStats;
    private bool isEnemySpawned = false;
    [SerializeField] List<GameObject> enemyList = new List<GameObject>();

    public bool isStartintgSegment { get; set; } = false;
    public bool isCompleted { get; set; } = false;

    private bool isUpdated = false;

    private IEnemySpawnStrategy enemySpawnStrategy;
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
            enemyCount = UnityEngine.Random.Range(1, 2);
        }    
       
        for (int i = 0; i < enemyCount; i++)
        {
            if (PlayerStats.Instance.level == 1)
            {
                enemySpawnStrategy = new Level1EnemySpawnStrategy();
                enemySpawnStrategy.SpawnEnemy(isInitial, transform, ref enemyList);
            }
            else if (PlayerStats.Instance.level == 2)
            {
                enemySpawnStrategy = new Level2To3EnemySpawnStrategy();
                enemySpawnStrategy.SpawnEnemy(isInitial, transform, ref enemyList);
            }
            else if (PlayerStats.Instance.level == 3)
            {
                enemySpawnStrategy = new Level2To3EnemySpawnStrategy();
                enemySpawnStrategy.SpawnEnemy(isInitial, transform, ref enemyList);

                int chance = Random.Range(1, 10);
                if (chance > 3 && i == 0 && isInitial)
                    SpawnSlimes();
            }
            else if(PlayerStats.Instance.level <= 5)
            {
                enemySpawnStrategy = new Level4To5EnemySpawnStrategy();
                enemySpawnStrategy.SpawnEnemy(isInitial, transform, ref enemyList);
                int chance = Random.Range(1, 10);
                if (chance > 6 && i == 0 && isInitial)
                    SpawnSlimes();
            }
            else if(PlayerStats.Instance.level <= 7)
            {
                enemySpawnStrategy = new Level6To7EnemySpawnStrategy();
                enemySpawnStrategy.SpawnEnemy(isInitial, transform, ref enemyList);
                int chance = Random.Range(1, 10);
                if (chance > 6 && i == 0)
                    SpawnSlimes();
            }
            else
            {
                enemySpawnStrategy = new Level8To9EnemySpawnStrategy();
                enemySpawnStrategy.SpawnEnemy(isInitial, transform, ref enemyList);
                int chance = Random.Range(1, 10);
                if (chance > 6 && i == 0 && isInitial)
                    SpawnSlimes();
            }
            //Spawn enemy based on random given type
            
        }
        if(isInitial)
            isEnemySpawned = true;
    }

    public void SpawnNextBossStage(GameObject boss, Transform spiderPosition)
    {
        SpawnEnemyOfType.Instance.SpawnSpiderNextStage(boss, false, transform, ref enemyList, spiderPosition);
    }

    public void SpawnSpiderBoss()
    {
        SpawnEnemyOfType.Instance.Spawn(LevelManager.Instance.spiderBoss, false, transform, ref enemyList);
        isEnemySpawned = true;
    }

    public void SpawnWaveShootherBoss()
    {
        SpawnEnemyOfType.Instance.Spawn(LevelManager.Instance.waveShooterBoss, false, transform, ref enemyList);
        isEnemySpawned = true;
    }
    public void SpawnWizzardBoss()
    {
        SpawnEnemyOfType.Instance.Spawn(LevelManager.Instance.wizzardBoss, false, transform, ref enemyList);
        isEnemySpawned = true;
    }

    public void SpawnSlimes()
    {
        SpawnEnemyOfType.Instance.SpwanJumpingSlime(LevelManager.Instance.jumpingSlimeEnemy, transform);
    }

}

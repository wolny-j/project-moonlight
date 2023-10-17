using Assets.Scripts.GameManagers.Levels.Spawners;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Level10to12EnemySPawnStrategy : IEnemySpawnStrategy
{

    public void SpawnEnemy(bool isInitial, Transform localTransform, ref List<GameObject> enemyList)
    {
        int enemyType = UnityEngine.Random.Range(1, 10);

        switch (enemyType)
        {
            case 1:
                SpawnEnemyOfType.Instance.Spawn(LevelManager.Instance.jumingSpiderEnemy, isInitial, localTransform, ref enemyList);
                break;
            case 2:
                if (isInitial)
                    SpawnEnemyOfType.Instance.Spawn(LevelManager.Instance.zombieEnemy, isInitial, localTransform, ref enemyList);
                else
                    SpawnEnemyOfType.Instance.Spawn(LevelManager.Instance.spiderEnemy, isInitial, localTransform, ref enemyList);
                break;
            case 3:
                SpawnEnemyOfType.Instance.Spawn(LevelManager.Instance.snailEnemy, isInitial, localTransform, ref enemyList);
                break;
            case 4:
                SpawnEnemyOfType.Instance.Spawn(LevelManager.Instance.shooterEnemy, isInitial, localTransform, ref enemyList);
                break;
            case 5:
                if (isInitial)
                    SpawnEnemyOfType.Instance.Spawn(LevelManager.Instance.coreEnemy, isInitial, localTransform, ref enemyList);
                else
                    SpawnEnemyOfType.Instance.Spawn(LevelManager.Instance.spiderEnemy, isInitial, localTransform, ref enemyList);
                break;
            case 6:
                SpawnEnemyOfType.Instance.Spawn(LevelManager.Instance.ninjaEnemy, isInitial, localTransform, ref enemyList);
                break;
            case 7:
                SpawnEnemyOfType.Instance.Spawn(LevelManager.Instance.devilEnemy, isInitial, localTransform, ref enemyList);
                break;
            case 8:
                SpawnEnemyOfType.Instance.Spawn(LevelManager.Instance.explosiveEnemy, isInitial, localTransform, ref enemyList);
                break;
            default:
                SpawnEnemyOfType.Instance.Spawn(LevelManager.Instance.jumingSpiderEnemy, isInitial, localTransform, ref enemyList);
                break;
        }
    }
}

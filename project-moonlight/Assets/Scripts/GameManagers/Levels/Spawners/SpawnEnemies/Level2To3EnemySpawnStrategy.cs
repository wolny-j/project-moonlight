using Assets.Scripts.GameManagers.Levels.Spawners;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2To3EnemySpawnStrategy : IEnemySpawnStrategy
{
    public void SpawnEnemy(bool isInitial, Transform localTransform, ref List<GameObject> enemyList)
    {
        int enemyType = UnityEngine.Random.Range(1, 4);

        switch (enemyType)
        {
            case 1:
                SpawnEnemyOfType.Instance.Spawn(LevelManager.Instance.spiderEnemy, isInitial, localTransform, ref enemyList);
                break;
            case 2:
                SpawnEnemyOfType.Instance.Spawn(LevelManager.Instance.spiderEnemy, isInitial, localTransform, ref enemyList);
                break;
            case 3:
                SpawnEnemyOfType.Instance.Spawn(LevelManager.Instance.snailEnemy, isInitial, localTransform, ref enemyList);
                break;
            default:
                SpawnEnemyOfType.Instance.Spawn(LevelManager.Instance.spiderEnemy, isInitial, localTransform, ref enemyList);
                break;
        }
    }
}

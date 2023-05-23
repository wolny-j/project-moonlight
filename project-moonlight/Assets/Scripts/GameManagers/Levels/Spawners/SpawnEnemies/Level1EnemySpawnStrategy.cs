using Assets.Scripts.GameManagers.Levels.Spawners;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1EnemySpawnStrategy : IEnemySpawnStrategy
{
    public void SpawnEnemy(bool isInitial, Transform localTransform, ref List<GameObject> enemyList)
    {
       SpawnEnemyOfType.Instance.Spawn(LevelManager.Instance.eyeEnemy, isInitial, localTransform, ref enemyList);
    }
}

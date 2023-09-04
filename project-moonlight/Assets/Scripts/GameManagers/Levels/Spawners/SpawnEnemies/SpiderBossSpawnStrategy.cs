using Assets.Scripts.GameManagers.Levels.Spawners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SpiderBossSpawnStrategy : IEnemySpawnStrategy
{
    public void SpawnEnemy(bool isInitial, Transform localTransform, ref List<GameObject> enemyList)
    {
        SpawnEnemyOfType.Instance.Spawn(LevelManager.Instance.spiderEnemy, isInitial, localTransform, ref enemyList);
    }
}

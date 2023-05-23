using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GameManagers.Levels.Spawners
{
    internal interface IEnemySpawnStrategy
    {
        void SpawnEnemy(bool isInitial, Transform localTransform, ref List<GameObject> enemyList);
    }
}

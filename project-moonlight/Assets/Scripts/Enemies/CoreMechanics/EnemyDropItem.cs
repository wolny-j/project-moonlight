using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    LevelManager levelManager;

    private void Start()
    {
        levelManager = LevelManager.Instance;
    }

    public bool DropHeartOnDeath()
    {
        System.Random random = new System.Random();
        int chance = random.Next(100);
        if (chance >= levelManager.heartDropChance)
        {
            Instantiate(levelManager.heart, transform.position, Quaternion.identity);
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool DropItemOnDeath(GameObject item, int dropChance)
    {
        System.Random random = new System.Random();
        int chance = random.Next(100);
        if (chance >= dropChance)
        {
            Instantiate(item, transform.position, Quaternion.identity);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool DropMapOnDeath()
    {
        if (!levelManager.isMapSpawned)
        {

            System.Random random = new System.Random();
            int chance = random.Next(100);
            if (chance >= levelManager.mapDropChance)
            {

                Instantiate(levelManager.map, transform.position, Quaternion.identity);
                levelManager.isMapSpawned = true;
                return true;
            }
            else
            {
                levelManager.mapDropChance -= 2;
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}

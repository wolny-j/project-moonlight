using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    private LevelManager levelManager;
    public bool IsDropingItem { get; set; } = true;
    private void Start()
    {
        levelManager = LevelManager.Instance;
    }

    public void CheckDeath(float health, GameObject item, int chance)
    {
        if (health <= 0)
        {
            DropHeartOnDeath();
            DropMapOnDeath();
            DropItemOnDeath(item, chance);


            Destroy(gameObject);
        }
    }

    public bool CheckDeathWithExplosion(float health, GameObject item, int chance)
    {
        if (health <= 0)
        {
            DropHeartOnDeath();
            DropMapOnDeath();
            DropItemOnDeath(item, chance);


            return true;
        }
        return false;
    }


    public bool DropHeartOnDeath()
    {
        if (IsDropingItem)
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
        else
        {
            return false;
        }
    }
    public bool DropItemOnDeath(GameObject item, int dropChance)
    {
        if (IsDropingItem)
        {
            System.Random random = new System.Random();
            int chance = random.Next(100);
            if (chance + PlayerStats.Instance.luck >= dropChance)
            {
                Instantiate(item, transform.position, Quaternion.identity);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool DropMapOnDeath()
    {
        if (IsDropingItem)
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
        else
        {
            return false;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    [SerializeField] GameObject chest;
    [SerializeField] GameObject shrine;

    Vector3 position1 = new Vector3(-0.336f, 0.36f, 0f);
    Vector3 position2 = new Vector3(-0.336f, -0.43f, 0f);
    Vector3 position3 = new Vector3(-0.336f, 0.36f, 0f);
    Vector3 position4 = new Vector3(0.34f, -0.43f, 0f);

    //REFACTOR to one instance in the whole game attached to SpawnManager and pass transform as parameter
    public void SpawnChest()
    {
        int chance = UnityEngine.Random.Range(0, 100);

        if(chance + PlayerStats.Instance.luck > 56)
        {
            chance = UnityEngine.Random.Range(0, 100);
            if(chance > 90)
            {
                GameObject shrineObj = Instantiate(shrine, transform);
                shrineObj.transform.localPosition = position2 + new Vector3(0, 0.2f, 0);
            }
            else
            {
                int position = UnityEngine.Random.Range(1, 5);
                Vector3 spawnPoint = new Vector3();
                GameObject chestObj = Instantiate(chest, transform);
                switch (position)
                {
                    case 1:
                        spawnPoint = position1;
                        break;
                    case 2:
                        spawnPoint = position2;
                        break;
                    case 3:
                        spawnPoint = position3;
                        break;
                    case 4:
                        spawnPoint = position4;
                        break;
                }
                chestObj.transform.localPosition = spawnPoint;
            }
            
        }
    }
}

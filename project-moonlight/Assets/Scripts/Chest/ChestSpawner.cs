using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    [SerializeField] GameObject chest;

    Vector3 position1 = new Vector3(-0.336f, 0.316f, 0f);
    Vector3 position2 = new Vector3(-0.336f, -0.34f, 0f);
    Vector3 position3 = new Vector3(-0.336f, 0.316f, 0f);
    Vector3 position4 = new Vector3(0.34f, -0.34f, 0f);

    //REFACTOR to one instance in the whole game attached to SpawnManager and pass transform as parameter
    public void SpawnChest()
    {
        int chance = UnityEngine.Random.Range(0, 100);

        if(chance > 1)
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
                    spawnPoint= position4;
                    break;
            }
            chestObj.transform.localPosition = spawnPoint;
        }
    }
}

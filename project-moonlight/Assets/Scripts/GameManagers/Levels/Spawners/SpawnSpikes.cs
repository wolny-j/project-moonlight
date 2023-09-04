using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpikes : MonoBehaviour
{
    [SerializeField] GameObject spikesPrefab;
    [SerializeField] GameObject rockPrefab;

    //REFACTOR to one instance in the whole game attached to SpawnManager and pass transform as parameter
    //Spawn spikes randomly on the segment
    public void GenerateSpikes(Transform segmentTransform)
    {
        const float initialX = -0.33f;
        const float initialY = 0.3f;

        const int spikeChanceThreshold = 93;

        const int spikeSpawnRows = 7;
        const int spikeSpawnColumns = 14;

        const float spikeSpawnColumnOffset = 0.05f;
        const float spikeSpawnRowOffset = -0.1f;

        int spikesTypeChance = 4;

        if (PlayerStats.Instance.level <= 2)
        {
            spikesTypeChance = 11;
        }

        for (int i = 0; i < spikeSpawnColumns; i++)
        {
            //Move spikes spawn x position by 'spikesSpawnColumnOffset' each iteration
            float x = initialX + i * spikeSpawnColumnOffset;

            for (int j = 0; j < spikeSpawnRows; j++)
            {
                //Move spikes spawn x position by 'spikesSpawnColumnOffset' each iteration
                float y = initialY + j * spikeSpawnRowOffset;
                if (j == 3 && (i == 6 || i == 7))
                    continue;
                int chance = UnityEngine.Random.Range(0, 100);

                //Spawn spikes prefab if chance is equal or higher than spikeChanceTreshold
                if (chance >= spikeChanceThreshold)
                {
                    Vector3 spawnPoint = new Vector3(x, y, 1);
                    int typeChance = Random.Range(0, 10);
                    if (typeChance >= spikesTypeChance)
                    {
                        
                        GameObject spikes = Instantiate(spikesPrefab, segmentTransform);
                        spikes.transform.localPosition = new Vector3(spawnPoint.x, spawnPoint.y - 0.02f, 1);
                    }
                    else
                    {
                        GameObject rock = Instantiate(rockPrefab, segmentTransform);
                        rock.transform.localPosition = spawnPoint;
                    }
                }
            }
        }
    }
}

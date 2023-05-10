using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpikes : MonoBehaviour
{
    [SerializeField] GameObject spikesPrefab;

    //REFACTOR to one instance in the whole game attached to SpawnManager and pass transform as parameter
    //Spawn spikes randomly on the segment
    public void GenerateSpikes()
    {
        const float initialX = -0.35f;
        const float initialY = 0.3f;

        const int spikeChanceThreshold = 95;

        const int spikeSpawnRows = 7;
        const int spikeSpawnColumns = 15;

        const float spikeSpawnColumnOffset = 0.05f;
        const float spikeSpawnRowOffset = -0.1f;

        for (int i = 0; i < spikeSpawnColumns; i++)
        {
            //Move spikes spawn x position by 'spikesSpawnColumnOffset' each iteration
            float x = initialX + i * spikeSpawnColumnOffset;

            for (int j = 0; j < spikeSpawnRows; j++)
            {
                //Move spikes spawn x position by 'spikesSpawnColumnOffset' each iteration
                float y = initialY + j * spikeSpawnRowOffset;

                int chance = UnityEngine.Random.Range(0, 100);

                //Spawn spikes prefab if chance is equal or higher than spikeChanceTreshold
                if (chance >= spikeChanceThreshold)
                {
                    Vector3 spawnPoint = new Vector3(x, y, 1);
                    GameObject spikes = Instantiate(spikesPrefab, transform);
                    spikes.transform.localPosition = spawnPoint;
                }
            }
        }
    }

}
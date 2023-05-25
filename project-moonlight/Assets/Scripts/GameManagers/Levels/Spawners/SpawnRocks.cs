using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRocks : MonoBehaviour
{ 
    [SerializeField] GameObject spikesPrefab;

    //REFACTOR to one instance in the whole game attached to SpawnManager and pass transform as parameter
    //Spawn spikes randomly on the segment
    public void GenerateRocks(Transform segmentTransform)
    {
        if (PlayerStats.Instance.level <= 3) return;

        const float initialX = -0.35f;
        const float initialY = 0.3f;

        const int rocksChanceThreshold = 97;

        const int rocksSpawnRows = 7;
        const int rocksSpawnColumns = 15;

        const float rocksSpawnColumnOffset = 0.05f;
        const float rocksSpawnRowOffset = -0.1f;

        for (int i = 0; i < rocksSpawnColumns; i++)
        {
            //Move spikes spawn x position by 'spikesSpawnColumnOffset' each iteration
            float x = initialX + i * rocksSpawnColumnOffset;

            for (int j = 0; j < rocksSpawnRows; j++)
            {
                //Move spikes spawn x position by 'spikesSpawnColumnOffset' each iteration
                float y = initialY + j * rocksSpawnRowOffset;

                int chance = UnityEngine.Random.Range(0, 100);

                //Spawn spikes prefab if chance is equal or higher than spikeChanceTreshold
                if (chance >= rocksChanceThreshold)
                {
                    Vector3 spawnPoint = new Vector3(x, y, 1);
                    GameObject spikes = Instantiate(spikesPrefab, segmentTransform);
                    spikes.transform.localPosition = spawnPoint;
                }
            }
        }
    }

}

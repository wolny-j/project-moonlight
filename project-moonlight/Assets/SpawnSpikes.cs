using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpikes : MonoBehaviour
{
    private bool isCompleted = false;
    private bool isStartingSegment;

    [SerializeField] GameObject spikesPrefab;
    void Start()
    {
        isStartingSegment = gameObject.GetComponent<SpawnEnemy>().isStartintgSegment;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GenereteSpikes();
        }
    }

    private void GenereteSpikes()
    {
        float x = -0.35f;
        float y = 0.3f;
        if (!isCompleted && !isStartingSegment)
        {
            for(int i = 0; i < 15; i++)
            {
                for(int j = 0; j < 7; j++)
                {
                    System.Random rand = new System.Random();
                    int chance = rand.Next(100);
                    if(chance > 95)
                    {
                        Vector3 spawnpoint = new Vector3(x, y, 1);

                        GameObject spikes = Instantiate(spikesPrefab, transform);
                        spikes.transform.localPosition = spawnpoint;
                    }
                    y -= 0.1f;
                }
                x += 0.05f;
                y = 0.3f;
            }
        }
    }
}

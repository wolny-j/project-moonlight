using System.Diagnostics.Tracing;
using UnityEngine;

public class SegmentCameraChange : MonoBehaviour
{
    [SerializeField] private float cameraOffsetX = 0.2f;

    private Camera mainCamera;

    private SpawnEnemy enemySpawner;
    private SpawnSpikes spikesSpawner;
    private ChestSpawner chestSpawner;
    private SpawnEnding endingSpawner;

    private PlayerStats playerStats;
    private Segment segment;

    private void Awake()
    {
        mainCamera = Camera.main;

        //Attach components from segemnt gameobject to execute SpawnEnemies and Spawn Spikes.
        enemySpawner = GetComponent<SpawnEnemy>();
        spikesSpawner = GetComponent<SpawnSpikes>();
        chestSpawner = GetComponent<ChestSpawner>();
        endingSpawner = GetComponent<SpawnEnding>();

        playerStats= GameObject.Find("PlayerStatsManager").GetComponent<PlayerStats>();
        segment = GetComponent<Segment>();
    }

    //REFACTOR CAMERA CHANGES AND SEGMENT CHANGE BLOCK

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!playerStats.IsCompleted)
            {
                GetComponent<BoxCollider2D>().isTrigger = false;
            }
            else
            {
                //If player entered segment for the first time and it is not starting segment execute code SpawnEnemies and GenerateSpikes then change camera
                if (segment.isFirstEnter && !segment.isStartintgSegment)
                {
                    playerStats.IsCompleted = false;
                    enemySpawner.SpawnEnemies(true);
                    chestSpawner.SpawnChest();
                    spikesSpawner.GenerateSpikes();
                    segment.isFirstEnter = false;

                    if (segment.isEndingSegment)
                    {
                        endingSpawner.Spawn();
                    }

                }
                Vector3 cameraSpawnPosition = transform.position + new Vector3(cameraOffsetX, 0f, -10f);
                mainCamera.transform.position = cameraSpawnPosition;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerStats.IsCompleted && !segment.isFirstEnter)
            {
                GetComponent<BoxCollider2D>().isTrigger = true;
                Vector3 cameraSpawnPosition = transform.position + new Vector3(cameraOffsetX, 0f, -10f);
                mainCamera.transform.position = cameraSpawnPosition;
            }
            else if(playerStats.IsCompleted && segment.isFirstEnter)
            {
                if (!segment.isStartintgSegment)
                {
                    playerStats.IsCompleted = false;
                    enemySpawner.SpawnEnemies(true);
                    chestSpawner.SpawnChest();
                    spikesSpawner.GenerateSpikes();
                    segment.isFirstEnter = false;
                }
                GetComponent<BoxCollider2D>().isTrigger = true;
                Vector3 cameraSpawnPosition = transform.position + new Vector3(cameraOffsetX, 0f, -10f);
                mainCamera.transform.position = cameraSpawnPosition;
            }
        }
    }
}

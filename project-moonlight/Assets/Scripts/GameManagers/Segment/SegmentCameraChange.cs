using System.Diagnostics.Tracing;
using UnityEngine;

public class SegmentCameraChange : MonoBehaviour
{
    [SerializeField] private float cameraOffsetX = 0.2f;

    private Camera mainCamera;

    private SpawnEnemy enemySpawner;
    private SpawnSpikes spikesSpawner;
    private SpawnRocks rocksSpawner;
    private ChestSpawner chestSpawner;
    private SpawnEnding endingSpawner;

    private PlayerStats playerStats;
    private Segment segment;

    [SerializeField] GameObject verticalWall;
    [SerializeField] GameObject horizontalWall;


    private void Awake()
    {
        mainCamera = Camera.main;

        //Attach components from segemnt gameobject to execute SpawnEnemies and Spawn Spikes.
        enemySpawner = GetComponent<SpawnEnemy>();
        spikesSpawner = GameObject.Find("GameManager").GetComponent<SpawnSpikes>();
        rocksSpawner = GameObject.Find("GameManager").GetComponent<SpawnRocks>();
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

                    SpawnEnemies();
                    chestSpawner.SpawnChest();
                    spikesSpawner.GenerateSpikes(transform);
                    //rocksSpawner.GenerateRocks(transform);
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
                    SpawnEnemies();
                    chestSpawner.SpawnChest();
                    spikesSpawner.GenerateSpikes(transform);
                    //rocksSpawner.GenerateRocks(transform);
                    segment.isFirstEnter = false;
                }
                GetComponent<BoxCollider2D>().isTrigger = true;
                Vector3 cameraSpawnPosition = transform.position + new Vector3(cameraOffsetX, 0f, -10f);
                mainCamera.transform.position = cameraSpawnPosition;
            }
        }
    }

    private void SpawnEnemies()
    {
        if (segment.isEndingSegment && playerStats.level == 4)
        {
            enemySpawner.SpawnSpiderBoss();
        }
        else if( segment.isEndingSegment && playerStats.level == 8)
        {
            enemySpawner.SpawnWaveShootherBoss();
        }
        else if (segment.isEndingSegment && playerStats.level == 12)
        {
            enemySpawner.SpawnWizzardBoss();
        }
        else
        {
            enemySpawner.SpawnEnemies(true);
        }
    }

    public void InstantiateEnemyWall()
    {
        int type = Random.Range(0, 2);
        if (type == 0)
        {
            //Vertical wall
            Vector3 spawnPoint = new Vector3(0, Random.Range(-0.1f, 0.5f), 1);
            GameObject wall = Instantiate(verticalWall, transform);
            wall.transform.localPosition = spawnPoint;
        }
        else
        {
            //Horizontal wall
            Vector3 spawnPoint = new Vector3(Random.Range(-0.5f, 0.1f), -0.5f, 1);
            GameObject wall = Instantiate(horizontalWall, transform);
            wall.transform.localPosition = spawnPoint;
        }
    }

}

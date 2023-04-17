using System.Diagnostics.Tracing;
using UnityEngine;

public class SegmentCameraChange : MonoBehaviour
{
    [SerializeField] private float cameraOffsetX = 0.2f;

    private Camera mainCamera;

    private SpawnEnemy enemySpawner;
    private SpawnSpikes spikesSpawner;

    public bool isStartintgSegment { get; set; } = false;
    public bool isFirstEnter { get; set; } = true;

    private void Awake()
    {
        mainCamera = Camera.main;

        //Attach components from segemnt gameobject to execute SpawnEnemies and Spawn Spikes.
        enemySpawner = GetComponent<SpawnEnemy>();
        spikesSpawner = GetComponent<SpawnSpikes>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //If player entered segment for the first time and it is not starting segment execute code SpawnEnemies and GenerateSpikes then change camera
            if (isFirstEnter && !isStartintgSegment)
            {
                enemySpawner.SpawnEnemies(true);
                spikesSpawner.GenerateSpikes();
                isFirstEnter = false;
            }
            Vector3 cameraSpawnPosition = transform.position + new Vector3(cameraOffsetX, 0f, -10f);
            mainCamera.transform.position = cameraSpawnPosition;            
        }
    }
}

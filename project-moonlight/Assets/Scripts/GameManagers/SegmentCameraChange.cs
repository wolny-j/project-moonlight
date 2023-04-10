using System.Diagnostics.Tracing;
using UnityEngine;

public class SegmentCameraChange : MonoBehaviour
{
    [SerializeField] private float cameraOffsetX = 0.2f;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 cameraSpawnPosition = transform.position + new Vector3(cameraOffsetX, 0f, -10f);
            mainCamera.transform.position = cameraSpawnPosition;            
        }
    }
}

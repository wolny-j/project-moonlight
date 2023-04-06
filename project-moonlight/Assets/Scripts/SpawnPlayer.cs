using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject camOB;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject grid;
    [SerializeField] private GameObject map;
    public GameObject player { get; set; }
    public bool spawned { get; set; } = false;
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            player = Instantiate(playerPrefab, camOB.transform.position, Quaternion.identity);
            player.transform.parent = grid.transform;
            player.transform.localPosition = new Vector3(camOB.transform.localPosition.x, camOB.transform.localPosition.y, 1);
            spawned = true;
        }
    }

}

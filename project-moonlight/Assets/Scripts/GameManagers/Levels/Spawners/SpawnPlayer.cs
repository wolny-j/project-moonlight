using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject camOB;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject grid;
    [SerializeField] private GameObject inventoryPanel;
    public GameObject player { get; set; }
    public bool spawned { get; set; } = false;

    [SerializeField] bool isNormalLevel = true;
    // Start is called before the first frame update

    void Start()
    {
        if(isNormalLevel)
        {
            inventoryPanel.SetActive(false);
            player = Instantiate(playerPrefab, camOB.transform.position, Quaternion.identity);
            player.transform.parent = grid.transform;
            player.transform.localPosition = new Vector3(camOB.transform.localPosition.x, camOB.transform.localPosition.y, 1);
            spawned = true;
        }
        else
        {
            player = Instantiate(playerPrefab, new Vector3(0, 0, 1), Quaternion.identity);
        }
        
    }

}

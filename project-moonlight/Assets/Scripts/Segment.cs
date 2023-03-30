using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    private GameObject cam;

    private void Awake()
    {
        cam = GameObject.Find("Main Camera");

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Transform cameraSpawn = transform;
            cam.transform.position = new Vector3(cameraSpawn.position.x + 0.2f, cameraSpawn.position.y, cam.transform.position.z);
        }
    }



}

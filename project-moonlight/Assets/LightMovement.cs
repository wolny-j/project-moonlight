using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMovement : MonoBehaviour
{
    private float speed = 0.2f;
    private Vector3 destination;


    private void Start()
    {
       destination = SetNewDestination();
    }

    void Update()
    {

        if (transform.localPosition == destination)
        {
            destination = SetNewDestination();
        }

        
        MoveToDestination(speed, destination);
    }

    public Vector3 SetNewDestination()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 10);
    }

    public void MoveToDestination(float speed, Vector3 destination)
    {
        float step = speed * Time.deltaTime;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, step);
    }
}

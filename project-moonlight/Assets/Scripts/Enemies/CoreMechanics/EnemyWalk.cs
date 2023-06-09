using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    public Vector3 SetNewDestination()
    {
        return new Vector3(Random.Range(-0.48f, 0.48f), Random.Range(-0.47f, 0.47f), 0);
    }

    public void MoveToDestination(float speed, Vector3 destination)
    {
        float step = speed * Time.deltaTime;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, step);
    }
}

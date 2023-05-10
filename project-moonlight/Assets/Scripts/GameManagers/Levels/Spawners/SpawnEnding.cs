using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnding : MonoBehaviour
{
    [SerializeField] GameObject ending;

    public void Spawn()
    {
        GameObject endingObject = Instantiate(ending, transform);
        endingObject.transform.localPosition = new Vector3(0, 0, 1);
    }
}

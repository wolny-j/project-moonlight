using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private float timer;
    private const float TIMELIFE = 5;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > TIMELIFE)
        {
            Destroy(gameObject);
        }
    }
}

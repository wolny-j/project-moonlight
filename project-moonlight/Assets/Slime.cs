using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private float timer = 0;
    private const float lifetime = 6f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= lifetime)
        {
            Destroy(gameObject);    
        }
    }
}

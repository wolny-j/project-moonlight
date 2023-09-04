using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingVerticalWall : MonoBehaviour
{
    private float speed = 0.8f;
    private float counter = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (counter > 10)
        {
            Destroy(gameObject);
        }
    }
}

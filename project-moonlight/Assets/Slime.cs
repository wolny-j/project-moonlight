using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private float timer = 0;
    private const float lifetime = 9f;



    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime * 0.6f)
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.7f);
        }
        if(timer >= lifetime)
        {
            Destroy(gameObject);    
        }
    }
}

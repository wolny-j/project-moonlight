using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class JumpingSlime : MonoBehaviour
{
    private Vector3 direction = Vector3.down;
    private bool isJumping = true;
    private float speed = 2.5f;
    private float counter = 0;
    private float frequency = 0;


    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite splashedSprite;

    // Start is called before the first frame update
    void Start()
    {
        frequency = Random.Range(1f, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if(counter > 1)
        {
            speed *= 1.3f;
            counter= 0;
        }
        if (isJumping)
            transform.Translate(speed * Time.deltaTime * direction);
        else
            transform.Translate(Vector2.zero);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Wall"))
        {
            speed = 2.5f;
            StartCoroutine(Jump());
        }
        if (collision.gameObject.CompareTag("BasicSpell"))
        { 
            Destroy(collision.gameObject); 
        }
        else if(collision.gameObject.CompareTag("Rock"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }




    IEnumerator Jump()
    {
        isJumping = false;
        GetComponent<SpriteRenderer>().sprite = splashedSprite;

        direction = (direction == Vector3.up) ? Vector3.down : Vector3.up;
        yield return new WaitForSeconds(frequency);
        GetComponent<SpriteRenderer>().sprite = normalSprite;

        isJumping = true;
    }
}

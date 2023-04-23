using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpell : MonoBehaviour
{
    public Direction direction;

    private float speed = 1.5f;
    private float timeToLive = 3;
    private float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeToLive)
        {
            Destroy(gameObject);
        }

        switch (direction)
        {
            case Direction.Up:
                transform.Translate(Vector3.up * speed * Time.deltaTime);
                break;
            case Direction.Down:
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                break;
            case Direction.Left:
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                break;
            case Direction.Right:
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                break;
            default:
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy the spell object if it collides with a wall
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}

public enum Direction
{
    Left,
    Right,
    Up, 
    Down
}

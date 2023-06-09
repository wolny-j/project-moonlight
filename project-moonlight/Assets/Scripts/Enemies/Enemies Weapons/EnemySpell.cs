using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemySpell : MonoBehaviour
{
    public Direction direction;

    public bool isBoomerang = false;

    private float speed = 1.5f;
    private float timeToLive = 3;
    private float timer = 0;
    private bool isReturning = false;

   

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

        if(isBoomerang && timer > 1 && !isReturning)
        {
            timeToLive = 5;
            /*switch (direction)
            {
                case Direction.Up:
                    direction = Direction.Down;
                    break;
                case Direction.Down:
                    direction = Direction.Up;
                    break;
                case Direction.Left:
                    direction = Direction.Right;
                    break;
                case Direction.Right:
                    direction = Direction.Left;
                    break;
                default:
                    break;
            }*/
            speed = speed * 1.5f;
            isReturning = true;
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

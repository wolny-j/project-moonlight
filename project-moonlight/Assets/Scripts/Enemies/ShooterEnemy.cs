using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    public float speed = 0.4f;
    private float health = 10f;
    private Vector3 destination;
    public bool isAiming = false;

    private float timer = 0;

    private LevelManager levelManager;
    SpriteRenderer spriteRenderer;
    private EnemyDropItem dropItem;
    private EnemyWalk enemyWalk;

    [SerializeField] Sprite eyeSprite;
    [SerializeField] Sprite eyeSpriteInverted;
    [SerializeField] GameObject enemySpell;


    // Start is called before the first frame update
    void Start()
    {
        dropItem = GetComponent<EnemyDropItem>();
        levelManager = LevelManager.Instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyWalk= GetComponent<EnemyWalk>();
        destination = enemyWalk.SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        CheckDeath();
        Shoot();

        if (transform.localPosition == destination)
        {
            destination = enemyWalk.SetNewDestination();
        }

        UpdateSprite();
        enemyWalk.MoveToDestination(speed, destination);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BasicSpell"))
        {
            health -= PlayerStats.Instance.power;
            Destroy(collision.gameObject);
        }
    }
    
    private void UpdateSprite()
    {
        if (destination.x > transform.localPosition.x)
        {
            spriteRenderer.sprite = eyeSpriteInverted;
        }
        else
        {
            spriteRenderer.sprite = eyeSprite;
        }
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            bool dropped = false;
            if (!dropped)
            {
                dropped = dropItem.DropHeartOnDeath();
            }
            if (!dropped)
            {
                dropped = dropItem.DropMapOnDeath();
            }
            if (!dropped)
            {
                dropped = dropItem.DropItemOnDeath(levelManager.eye, levelManager.eyeDropChance);
            }

            Destroy(gameObject);
        }
    }

    private void Shoot()
    {
        if(timer > 2)
        {
            Vector2 spawnPosition = transform.position;
            InstantiateEnemySpell(spawnPosition, Direction.Left);
            InstantiateEnemySpell(spawnPosition, Direction.Right);
            InstantiateEnemySpell(spawnPosition, Direction.Up);
            InstantiateEnemySpell(spawnPosition, Direction.Down);
            timer = 0;
        }
    }

    private void InstantiateEnemySpell(Vector2 spawnPosition, Direction direction)
    {
        EnemySpell spell = Instantiate(enemySpell, spawnPosition, Quaternion.identity).GetComponent<EnemySpell>();
        spell.direction = direction;
    }
}

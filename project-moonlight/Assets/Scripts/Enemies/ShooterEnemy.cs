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

    [SerializeField] Sprite eyeSprite;
    [SerializeField] Sprite eyeSpriteInverted;
    [SerializeField] GameObject enemySpell;


    // Start is called before the first frame update
    void Start()
    {
        levelManager = LevelManager.Instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        CheckDeath();
        Shoot();

        if (transform.localPosition == destination)
        {
            SetNewDestination();
        }

        MoveToDestination();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BasicSpell"))
        {
            health -= PlayerStats.Instance.power;
            Destroy(collision.gameObject);
        }
    }

    private void SetNewDestination()
    {
        destination = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0);
    }

    private void MoveToDestination()
    {
        UpdateSprite();

        float step = speed * Time.deltaTime;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, step);
    }

    private bool DropHeartOnDeath()
    {
        System.Random random = new System.Random();
        int chance = random.Next(100);
        if (chance >= levelManager.heartDropChance)
        {
            Instantiate(levelManager.heart, transform.position, Quaternion.identity);
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool DropItemOnDeath(GameObject item, int dropChance)
    {
        System.Random random = new System.Random();
        int chance = random.Next(100);
        if (chance >= dropChance)
        {
            Instantiate(item, transform.position, Quaternion.identity);
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool DropMapOnDeath()
    {
        if (!levelManager.isMapSpawned)
        {

            System.Random random = new System.Random();
            int chance = random.Next(100);
            if (chance >= levelManager.mapDropChance)
            {

                Instantiate(levelManager.map, transform.position, Quaternion.identity);
                levelManager.isMapSpawned = true;
                return true;
            }
            else
            {
                levelManager.mapDropChance -= 2;
                return false;
            }
        }
        else
        {
            return false;
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
                dropped = DropHeartOnDeath();
            }
            if (!dropped)
            {
                dropped = DropMapOnDeath();
            }
            if (!dropped)
            {
                dropped = DropItemOnDeath(levelManager.eye, levelManager.eyeDropChance);
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

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    public float speed = 0.4f;
    private float health = 10f;
    private Vector3 destination;
    public bool isAiming = false;
    public bool isBoomerangEnemy = false;

    private float timer = 0;

    private EnemyDropItem dropItem;
    private EnemyWalk enemyWalk;
    private ISpriteUpdate spriteUpdate;
    private LevelManager levelManager;
    private float shootFrequency;

    [SerializeField] Sprite eyeSprite;
    [SerializeField] Sprite eyeSpriteInverted;
    [SerializeField] GameObject enemySpell;
    [SerializeField] AudioSource takeDamage;

    // Start is called before the first frame update
    void Start()
    {
        dropItem = GetComponent<EnemyDropItem>();
        enemyWalk= GetComponent<EnemyWalk>();
        spriteUpdate = GetComponent<EnemyUpdateSprite>();
        levelManager = LevelManager.Instance;
        destination = enemyWalk.SetNewDestination();
        if(isBoomerangEnemy)
        {
            shootFrequency = 1;
        }
        else
        {
            shootFrequency = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        dropItem.CheckDeath(health, levelManager.eye, levelManager.eyeDropChance);
        Shoot();

        if (transform.localPosition == destination)
        {
            destination = enemyWalk.SetNewDestination();
        }

        spriteUpdate.UpdateSprite(destination);
        enemyWalk.MoveToDestination(speed, destination);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BasicSpell"))
        {
            health -= PlayerStats.Instance.power;
            spriteUpdate.BlinkAnimation();
            takeDamage.Play();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Explosion"))
        {
            spriteUpdate.BlinkAnimation();
            takeDamage.Play();
            health -= PlayerStats.Instance.power * 3;
        }
    }


    private void Shoot()
    {
        if(timer > shootFrequency)
        {
            Vector2 spawnPosition = transform.position;
            if (isBoomerangEnemy)
            {
                ShootBoomerang(spawnPosition);
            }
            else
            {
                ShootSpell(spawnPosition);
            }


            timer = 0;
            
        }
    }

    private void ShootSpell(Vector2 spawnPosition)
    {
        InstantiateEnemySpell(spawnPosition, Direction.Left);
        InstantiateEnemySpell(spawnPosition, Direction.Right);
        InstantiateEnemySpell(spawnPosition, Direction.Up);
        InstantiateEnemySpell(spawnPosition, Direction.Down);
    }

    private void ShootBoomerang(Vector2 spawnPosition)
    {
        int rand = Random.Range(1, 5);
        switch (rand)
        {
            case 1:
                InstantiateEnemySpell(spawnPosition, Direction.Left);
                break;
            case 2:
                InstantiateEnemySpell(spawnPosition, Direction.Right);
                break;
            case 3:
                InstantiateEnemySpell(spawnPosition, Direction.Up);
                break;
            case 4:
                InstantiateEnemySpell(spawnPosition, Direction.Down);
                break;
        }
    }

    private void InstantiateEnemySpell(Vector2 spawnPosition, Direction direction)
    {
        EnemySpell spell = Instantiate(enemySpell, spawnPosition, Quaternion.identity).GetComponent<EnemySpell>();
        spell.direction = direction;
        if (isBoomerangEnemy)
        {
            spell.isBoomerang = true;
        }
    }
}

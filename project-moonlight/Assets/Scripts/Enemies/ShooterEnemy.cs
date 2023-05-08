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

    private EnemyDropItem dropItem;
    private EnemyWalk enemyWalk;
    private ISpriteUpdate spriteUpdate;
    private LevelManager levelManager;

    [SerializeField] Sprite eyeSprite;
    [SerializeField] Sprite eyeSpriteInverted;
    [SerializeField] GameObject enemySpell;


    // Start is called before the first frame update
    void Start()
    {
        dropItem = GetComponent<EnemyDropItem>();
        enemyWalk= GetComponent<EnemyWalk>();
        spriteUpdate = GetComponent<EnemyUpdateSprite>();
        levelManager = LevelManager.Instance;
        destination = enemyWalk.SetNewDestination();
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
            Destroy(collision.gameObject);
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

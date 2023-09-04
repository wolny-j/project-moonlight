using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BossEnemyWaveShooter : MonoBehaviour
{
    public float speed = 0.4f;
    private float health = BossHealthBar.SHOOTER_HEALTH;
    private Vector3 destination;

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

    private bool hit = false;
    private float hitCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        BossHealthBar.Instance.LoadShooterBar();
        dropItem = GetComponent<EnemyDropItem>();
        enemyWalk = GetComponent<EnemyWalk>();
        spriteUpdate = GetComponent<EnemyUpdateSprite>();
        levelManager = LevelManager.Instance;
        destination = enemyWalk.SetNewDestination();
        shootFrequency = 2.5f;
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHit();
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

            if (!hit)
            {
                health -= PlayerStats.Instance.power;
                BossHealthBar.Instance.UpdateHealthBar(PlayerStats.Instance.power);
                spriteUpdate.BlinkAnimation();
                takeDamage.Play();
                Destroy(collision.gameObject);
                hit = true;
            }

        }
        if (collision.gameObject.CompareTag("PlayerSlime"))
        {
            if (!hit)
            {
                spriteUpdate.BlinkAnimation();
                takeDamage.Play();
                health -= PlayerStats.Instance.power / 4;
                BossHealthBar.Instance.UpdateHealthBar(PlayerStats.Instance.power);
                hit = true;
            }
        }
        if (collision.gameObject.CompareTag("Explosion"))
        {
            if (health < PlayerStats.Instance.power * 4)
            {
                BossHealthBar.Instance.UpdateHealthBar(health);
            }
            else
            {
                BossHealthBar.Instance.UpdateHealthBar(PlayerStats.Instance.power);
            }
            spriteUpdate.BlinkAnimation();
            takeDamage.Play();
            health -= PlayerStats.Instance.power * 4;
            
        }
        if (collision.gameObject.CompareTag("Rock"))
        {
            destination = enemyWalk.SetNewDestination();
            enemyWalk.MoveToDestination(speed, destination);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            destination = enemyWalk.SetNewDestination();
            enemyWalk.MoveToDestination(speed, destination);
        }
    }


    private void Shoot()
    {
        if (timer > shootFrequency)
        {
            StartCoroutine(ShotWave());


            timer = 0;

        }
    }

    private void ShootSpell(Vector2 spawnPosition)
    {
        InstantiateEnemySpell(spawnPosition, Direction.Left);
        InstantiateEnemySpell(spawnPosition, Direction.Right);
        InstantiateEnemySpell(spawnPosition, Direction.Up);
        InstantiateEnemySpell(spawnPosition, Direction.Down);
        InstantiateEnemySpell(spawnPosition, Direction.TopLeft);
        InstantiateEnemySpell(spawnPosition, Direction.TopRight);
        InstantiateEnemySpell(spawnPosition, Direction.BottomLeft);
        InstantiateEnemySpell(spawnPosition, Direction.BottomRight);
    }

    private void InstantiateEnemySpell(Vector2 spawnPosition, Direction direction)
    {
        EnemySpell spell = Instantiate(enemySpell, spawnPosition, Quaternion.identity).GetComponent<EnemySpell>();
        spell.direction = direction;
        spell.SetSpeed(1.7f);
    }

    IEnumerator ShotWave()
    {
        Vector2 spawnPosition;
        for(int x = 0; x< 5; x++)
        {
            spawnPosition = transform.position;
            ShootSpell(spawnPosition);
            yield return new WaitForSeconds(0.25f);
        }

    }

    private void UpdateHit()
    {
        if (hit)
            hitCounter += Time.deltaTime;
        if (hitCounter > 0.1f)
        {
            hit = false;
            hitCounter = 0;
        }
    }
}

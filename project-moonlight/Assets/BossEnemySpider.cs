using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BossEnemySpider : MonoBehaviour
{
    [SerializeField] private float speed = 0.4f;
    private float health = 20f;
    private Vector3 destination;
    public bool isAiming = false;
    private SpawnEnemy spawnEnemy;
    private EnemyDropItem dropItem;
    private EnemyWalk enemyWalk;
    private ISpriteUpdate spriteUpdate;
    private LevelManager levelManager;

    [SerializeField] Sprite eyeSprite;
    [SerializeField] Sprite eyeSpriteInverted;
    [SerializeField] AudioSource takeDamage;

    [SerializeField] BossStage bossStage = BossStage.First;

    [SerializeField] GameObject spiderBoss2;
    [SerializeField] GameObject spiderBoss3;
    [SerializeField] GameObject spiderBoss4;

    private bool hit = false;
    private float hitCounter = 0;
    enum BossStage
    {
        First,
        Second,
        Third,
        Fourth,
    }

    // Start is called before the first frame update
    void Start()
    {
        if(bossStage == BossStage.First)
            BossHealthBar.Instance.LoadSpiderBar();
        spawnEnemy = transform.parent?.GetComponent<SpawnEnemy>();
        dropItem = GetComponent<EnemyDropItem>();
        enemyWalk = GetComponent<EnemyWalk>();
        spriteUpdate = GetComponent<EnemyUpdateSprite>();
        levelManager = LevelManager.Instance;
        destination = enemyWalk.SetNewDestination();

        switch (bossStage)
        {
            case BossStage.First:
                health = 20;
                break;
            case BossStage.Second:
                health = 10;
                break;
            case BossStage.Third:
                health = 5;
                break;
            case BossStage.Fourth:
                health = 1;
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHit();
        dropItem.CheckDeath(health, levelManager.web, levelManager.webDropChance);

        if (transform.localPosition == destination)
        {
            destination = enemyWalk.SetNewDestination();
        }

        OnDeath();
        spriteUpdate.UpdateSprite(destination);
        enemyWalk.MoveToDestination(speed, destination);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
        if (collision.gameObject.CompareTag("PlayerSlime"))
        {
            if (!hit)
            {
                spriteUpdate.BlinkAnimation();
                takeDamage.Play();
                health -= PlayerStats.Instance.power / 4;
                hit = true;
            }
        }
        if (collision.gameObject.CompareTag("BasicSpell"))
        {
            if (!hit)
            {
                if (health < PlayerStats.Instance.power)
                {
                    BossHealthBar.Instance.UpdateHealthBar(health);
                }
                else
                {
                    BossHealthBar.Instance.UpdateHealthBar(PlayerStats.Instance.power);
                }
                health -= PlayerStats.Instance.power;
                spriteUpdate.BlinkAnimation();
                takeDamage.Play();
                hit = true;
                Destroy(collision.gameObject);
            }
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

    private void OnDeath()
    {
        if(health <= 0)
        {
            switch(bossStage)
            {
                case BossStage.First:
                    for(int i = 0; i < 2; i++)
                    {
                        SpawnNextBossStage(spiderBoss2);
                    }
                    break;
                case BossStage.Second:
                    for (int i = 0; i < 2; i++)
                    {
                        SpawnNextBossStage(spiderBoss3);
                    }
                    break;
                case BossStage.Third:
                    for (int i = 0; i <2; i++)
                    {
                        SpawnNextBossStage(spiderBoss4);
                    }
                    break;
                case BossStage.Fourth:
                   // spawnEnemy.isCompleted = true;
                    Destroy(gameObject);
                    break;

            }
            Destroy(gameObject);
        }
    }


    private void SpawnNextBossStage(GameObject boss)
    {
        spawnEnemy.SpawnNextBossStage(boss, transform);
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


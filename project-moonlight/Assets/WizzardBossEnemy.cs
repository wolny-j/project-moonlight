using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class WizzardBossEnemy : MonoBehaviour
{
    public float speed = 0.15f;
    private float health = BossHealthBar.WIZZARD_HEALTH;
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
        BossHealthBar.Instance.LoadWizzardBar();
        dropItem = GetComponent<EnemyDropItem>();
        enemyWalk = GetComponent<EnemyWalk>();
        spriteUpdate = GetComponent<EnemyUpdateSprite>();
        levelManager = LevelManager.Instance;
        destination = enemyWalk.SetNewDestination();
        shootFrequency = 3.2f;

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
            gameObject.GetComponentInParent<SegmentCameraChange>().InstantiateEnemyWall();
            timer = 0;

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

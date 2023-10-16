using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilEnemy : MonoBehaviour
{
    public float speed = 0.1f;
    private float health = 20f;
    private Vector3 destination;

    private float timer = 0;

    private EnemyDropItem dropItem;
    private EnemyWalk enemyWalk;
    private ISpriteUpdate spriteUpdate;
    private LevelManager levelManager;
    private float shootFrequency;

    [SerializeField] GameObject fire;
    [SerializeField] AudioSource takeDamage;

    private bool hit = false;
    private float hitCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        dropItem = GetComponent<EnemyDropItem>();
        enemyWalk = GetComponent<EnemyWalk>();
        spriteUpdate = GetComponent<EnemyUpdateSprite>();
        levelManager = LevelManager.Instance;
        destination = enemyWalk.SetNewDestination();
        shootFrequency = 3;
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

                spriteUpdate.BlinkAnimation();
                takeDamage.Play();
                Destroy(collision.gameObject);
                hit = true;
            }
        }
        if (collision.gameObject.CompareTag("Explosion"))
        {
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
            timer = 0;
            Vector2 spawnPosition = GameObject.Find("Player(Clone)").transform.position;
            StartCoroutine(ShootAnimation(spawnPosition));
        }
    }

    IEnumerator ShootAnimation(Vector2 spawn)
    {
        float temp = speed;
        speed = 0;
        yield return new WaitForSeconds(0.4f);
        

        Instantiate(fire, spawn, Quaternion.identity);

        
        speed = temp;
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

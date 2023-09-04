using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    private float health = 20;
    private float counter = 0;
    private const float SPAWN_FREQUENCY = 6f;

    private SpawnEnemy spawnEnemy;

    private ISpriteUpdate spriteUpdate;
    [SerializeField] AudioSource takeDamage;

    private bool hit = false;
    private float hitCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        spriteUpdate = GetComponent<EnemyUpdateSprite>();
        spawnEnemy = transform.parent?.GetComponent<SpawnEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHit();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        counter += Time.deltaTime;

        if(counter > SPAWN_FREQUENCY)
        {
            spawnEnemy.SpawnEnemies(false);
            counter = 0;
        }
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
        if (collision.gameObject.CompareTag("Explosion"))
        {
            takeDamage.Play();
            spriteUpdate.BlinkAnimation();
            health -= PlayerStats.Instance.power * 4;
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

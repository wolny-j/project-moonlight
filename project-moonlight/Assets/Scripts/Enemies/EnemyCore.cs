using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    private float health = 20;
    private float counter = 0;
    private const float SPAWN_FREQUENCY = 4f;

    private SpawnEnemy spawnEnemy;
    // Start is called before the first frame update
    void Start()
    { 
        spawnEnemy = transform.parent?.GetComponent<SpawnEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
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
        if (collision.CompareTag("BasicSpell"))
        {
            health -= PlayerStats.Instance.power;
            Destroy(collision.gameObject);
        }
    }
}

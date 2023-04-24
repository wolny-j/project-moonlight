using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailEnemy : MonoBehaviour
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

    [SerializeField] Sprite snailSprite;
    [SerializeField] Sprite snailSpriteInverted;

    [SerializeField] GameObject slimePrefab;
    [SerializeField] float frequency = 0.3f;



    // Start is called before the first frame update
    void Start()
    {
        dropItem= GetComponent<EnemyDropItem>();
        levelManager = LevelManager.Instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyWalk= GetComponent<EnemyWalk>();
        destination = enemyWalk.SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        DropSlime(frequency);

        CheckDeath();

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

    private void DropSlime(float frequency)
    {
        if(timer >= frequency)
        {
            Instantiate(slimePrefab, transform.position, Quaternion.identity);
            timer = 0;
        }
    }

  

    private void UpdateSprite()
    {
        if (destination.x > transform.localPosition.x)
        {
            spriteRenderer.sprite = snailSprite;

        }
        else
        {
            spriteRenderer.sprite = snailSpriteInverted;
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
                dropped = dropItem.DropItemOnDeath(levelManager.shell, levelManager.shellDropChance);
            }

            Destroy(gameObject);
        }
    }
}

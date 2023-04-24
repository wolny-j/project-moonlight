using System.Collections;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public float speed = 0.4f;
    private float health = 10f;
    private Vector3 destination;
    public bool isAiming = false;


    private LevelManager levelManager;
    private EnemyDropItem dropItem;
    private EnemyWalk enemyWalk;
    private SpriteRenderer spriteRenderer;

    [SerializeField] Sprite eyeSprite;
    [SerializeField] Sprite eyeSpriteInverted;


    // Start is called before the first frame update
    void Start()
    {
        levelManager = LevelManager.Instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
        dropItem = GetComponent<EnemyDropItem>();
        enemyWalk = GetComponent<EnemyWalk>();
        destination = enemyWalk.SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
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
                dropped = dropItem.DropHeartOnDeath();
            }
            if (!dropped)
            {
                dropped = dropItem.DropMapOnDeath();
            }
            if (!dropped)
            {
                dropped = dropItem.DropItemOnDeath(levelManager.eye, levelManager.eyeDropChance);
            }

            Destroy(gameObject);
        }
    }
}

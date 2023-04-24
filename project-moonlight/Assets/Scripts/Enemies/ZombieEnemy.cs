using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEnemy : MonoBehaviour
{
    public float speed = 0.15f;
    private float health = 10f;
    private Vector3 destination;
    private Transform player;
    private GameObject target;
    
    public bool isAiming = true;
    private bool aim = false;
    
    private const float RUSH_MULTIPLAYER = 3.5f;
    private const float RUSH_DISTANCE = 2f;

    private LevelManager levelManager;
    SpriteRenderer spriteRenderer;
    private EnemyDropItem dropItem;
    private EnemyWalk enemyWalk;

    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite normalSpriteInverted;
    [SerializeField] Sprite RushSprite;
    [SerializeField] Sprite rushSpriteInverted;


    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = LevelManager.Instance;
        dropItem= GetComponent<EnemyDropItem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player(Clone)").transform;
        enemyWalk = GetComponent<EnemyWalk>();
        destination = enemyWalk.SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        CheckDeath();

        if (transform.localPosition == destination)
        {
            destination = enemyWalk.SetNewDestination();
        }

        float distance = Vector2.Distance(player.position, transform.position);

        if (isAiming && distance < RUSH_DISTANCE && !aim && timer > 2f)
        {
            Aim();
        }

        if (aim && transform.localPosition == target.transform.localPosition)
        {
            StartCoroutine(StunEnemy(target));
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
    private void Aim()
    {
        aim = true;
        target = new GameObject();
        target.transform.position = new Vector3(player.position.x, player.position.y, 0);
        target.transform.SetParent(transform.parent);
        destination = target.transform.localPosition;
        speed *= RUSH_MULTIPLAYER;
    }

    IEnumerator StunEnemy(GameObject target)
    {
        float temp = speed;
        speed = 0.01f;
        isAiming = false;
        aim = false;
        yield return new WaitForSeconds(2);
        speed = temp / RUSH_MULTIPLAYER;
        enemyWalk.SetNewDestination();
        yield return new WaitForSeconds(1);
        isAiming = true;
        Destroy(target);
    }

    
    private void UpdateSprite()
    {
        if (destination.x > transform.localPosition.x)
        {

            if (aim)
            {
                spriteRenderer.sprite = RushSprite;
            }
            else
            {
                spriteRenderer.sprite = normalSprite;
            }
        }
        else
        {

            if (aim)
            {
                spriteRenderer.sprite = rushSpriteInverted;
            }
            else
            {
                spriteRenderer.sprite = normalSpriteInverted;
            }
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
                dropped = dropItem.DropItemOnDeath(levelManager.brain, levelManager.brainDropChance);
            }

            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEnemy : MonoBehaviour
{
    public float speed = 0.15f;
    private float health = 10f;
    private Vector3 destination;
    private LevelManager levelManager;
    private Transform player;
    private GameObject target;
    
    public bool isAiming = true;
     public bool aim { get; set; } = false;
    
    private const float RUSH_MULTIPLAYER = 3.5f;
    private const float RUSH_DISTANCE = 2f;


    private EnemyDropItem dropItem;
    private EnemyWalk enemyWalk;
    private ISpriteUpdate spriteUpdate;

    [SerializeField] AudioSource takeDamage;
    [SerializeField] AudioSource aimSound;

    private bool hit = false;
    private float hitCounter = 0;

    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        dropItem= GetComponent<EnemyDropItem>();
        player = GameObject.Find("Player(Clone)").transform;
        enemyWalk = GetComponent<EnemyWalk>();
        destination = enemyWalk.SetNewDestination();
        levelManager = LevelManager.Instance;
        spriteUpdate = GetComponent<ZombieUpdateSprite>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHit();
        timer += Time.deltaTime;

        dropItem.CheckDeath(health, levelManager.brain, levelManager.brainDropChance);

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
            spriteUpdate.BlinkAnimation();
            takeDamage.Play();
            health -= PlayerStats.Instance.power * 4;
        }
    }
    private void Aim()
    {
        aim = true;
        aimSound.Play();
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

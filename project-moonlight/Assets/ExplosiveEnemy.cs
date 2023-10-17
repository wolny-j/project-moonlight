using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ExplosiveEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 0.3f;
    private float health = 16f;
    private Vector3 destination;
    public bool isAiming = false;
    private Transform player;
    private EnemyDropItem dropItem;
    private EnemyWalk enemyWalk;
    private ISpriteUpdate spriteUpdate;
    private LevelManager levelManager;


    [SerializeField] AudioSource takeDamage;
    [SerializeField] GameObject explosion;

    private bool hit = false;
    private float hitCounter = 0;

    enum Type
    {
        Eye,
        Spider
    }

    [SerializeField] Type enemyType;
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player(Clone)").transform;
        dropItem = GetComponent<EnemyDropItem>();
        enemyWalk = GetComponent<EnemyWalk>();
        spriteUpdate = GetComponent<EnemyUpdateSprite>();
        levelManager = LevelManager.Instance;
        destination = enemyWalk.SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHit();

        if(dropItem.CheckDeathWithExplosion(health, levelManager.eye, levelManager.eyeDropChance))
        {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        Aim();

        spriteUpdate.UpdateSprite(destination);
        enemyWalk.MoveToDestination(speed, destination);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void Aim()
    {
        target = new GameObject();
        target.transform.position = new Vector3(player.position.x, player.position.y, 0);
        target.transform.SetParent(transform.parent);
        destination = target.transform.localPosition;

    }
}

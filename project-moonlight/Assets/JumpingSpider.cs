using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class JumpingSpider : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private float health = 20f;
    private Vector3 destination;


    private EnemyDropItem dropItem;
    private EnemyWalk enemyWalk;
    private ISpriteUpdate spriteUpdate;
    private LevelManager levelManager;

    [SerializeField] Sprite eyeSprite;
    [SerializeField] Sprite eyeSpriteInverted;
    [SerializeField] AudioSource takeDamage;

    private bool hit = false;
    private float hitCounter = 0;

    float stunCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
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
        dropItem.CheckDeath(health, levelManager.web, levelManager.webDropChance);

        if (transform.localPosition == destination)
        {
            stunCounter += Time.deltaTime;
            if (stunCounter < 3)
            {
                speed = 0;
            }
            else
            {
                speed = 1f;
                destination = enemyWalk.SetNewDestination();
                stunCounter = 0;
            }
            
        }

        enemyWalk.MoveToDestination(speed, destination);
        spriteUpdate.UpdateSprite(destination);
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
        if (collision.gameObject.CompareTag("Wall"))
        {

            destination = enemyWalk.SetNewDestination();
            enemyWalk.MoveToDestination(speed, destination);
        }
    }

}

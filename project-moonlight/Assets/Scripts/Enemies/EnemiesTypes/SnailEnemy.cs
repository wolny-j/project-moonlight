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

    private EnemyDropItem dropItem;
    private EnemyWalk enemyWalk;
    private ISpriteUpdate spriteUpdate;
    private LevelManager levelManager;
    [SerializeField] GameObject slimePrefab;
    [SerializeField] float frequency = 0.3f;
    [SerializeField] AudioSource takeDamage;


    // Start is called before the first frame update
    void Start()
    {
        dropItem= GetComponent<EnemyDropItem>();
        enemyWalk= GetComponent<EnemyWalk>();
        spriteUpdate = GetComponent<EnemyUpdateSprite>();
        levelManager = LevelManager.Instance;
        destination = enemyWalk.SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        DropSlime(frequency);

        dropItem.CheckDeath(health, levelManager.shell, levelManager.shellDropChance);

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
            health -= PlayerStats.Instance.power;
            spriteUpdate.BlinkAnimation();
            takeDamage.Play();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Explosion"))
        {
            spriteUpdate.BlinkAnimation();
            takeDamage.Play();
            health -= PlayerStats.Instance.power * 3;
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

}

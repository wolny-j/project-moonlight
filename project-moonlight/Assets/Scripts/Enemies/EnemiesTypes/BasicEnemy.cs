using System.Collections;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public float speed = 0.4f;
    private float health = 10f;
    private Vector3 destination;
    public bool isAiming = false;


    private EnemyDropItem dropItem;
    private EnemyWalk enemyWalk;
    private ISpriteUpdate spriteUpdate;
    private LevelManager levelManager;

    [SerializeField] Sprite eyeSprite;
    [SerializeField] Sprite eyeSpriteInverted;


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
        dropItem.CheckDeath(health, levelManager.eye, levelManager.eyeDropChance);

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
            Destroy(collision.gameObject);
        }
    }


  
}

using System.Collections;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField]private float speed = 0.4f;
    private float health = 10f;
    private Vector3 destination;
    public bool isAiming = false;


    private EnemyDropItem dropItem;
    private EnemyWalk enemyWalk;
    private ISpriteUpdate spriteUpdate;
    private LevelManager levelManager;

    [SerializeField] Sprite eyeSprite;
    [SerializeField] Sprite eyeSpriteInverted;
    [SerializeField] AudioSource takeDamage;

    enum Type
    {
        Eye,
        Spider
    }

    [SerializeField] Type enemyType;

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
        if(enemyType == Type.Eye)
            dropItem.CheckDeath(health, levelManager.eye, levelManager.eyeDropChance);
        else if(enemyType == Type.Spider)
            dropItem.CheckDeath(health, levelManager.web, levelManager.webDropChance);

        if (transform.localPosition == destination)
        {
            destination = enemyWalk.SetNewDestination();
        }

        spriteUpdate.UpdateSprite(destination);
        enemyWalk.MoveToDestination(speed, destination);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Explosion"))
        {
            spriteUpdate.BlinkAnimation();
            takeDamage.Play();
            health -= PlayerStats.Instance.power * 4;
        }
        if (collision.gameObject.CompareTag("BasicSpell"))
        {
            health -= PlayerStats.Instance.power;
            spriteUpdate.BlinkAnimation();
            takeDamage.Play();
            Destroy(collision.gameObject);
        }
    }



}

using System.Collections;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public float speed = 0.4f;
    private float health = 10f;
    private Vector3 destination;
    private Transform player;
    private GameObject target;
    public bool isAiming = false;
    private bool aim = false;
    private const float RUSH_MULTIPLAYER = 3.5f;
    private const float RUSH_DISTANCE = 2f;
    private const float HEARTH_CHANCE = 92f;


    private LevelManager levelManager;
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite normalSpriteInverted;
    [SerializeField] Sprite RushSprite;
    [SerializeField] Sprite rushSpriteInverted;


    private float timer = 0; 

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player(Clone)").transform;
        SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if (health <= 0)
        {
            bool dropped = false;
            if (!dropped)
            {
                dropped = DropHeartOnDeath();
            }
            if (!dropped)
            {
                dropped = DropMapOnDeath();
            }
            
            Destroy(gameObject);
        }

        if (transform.localPosition == destination)
        {
            SetNewDestination();
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

        MoveToDestination();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BasicSpell"))
        {
            health -= 2f;
            Destroy(collision.gameObject);
        }
    }

    private void SetNewDestination()
    {
        destination = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0);
    }

    private void MoveToDestination()
    {
        UpdateSprite();

        float step = speed * Time.deltaTime;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, step);
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
        speed = 0.01f   ;
        isAiming = false;
        aim = false;
        yield return new WaitForSeconds(2);
        speed = temp / RUSH_MULTIPLAYER;
        SetNewDestination();
        yield return new WaitForSeconds(1);
        isAiming = true;
        Destroy(target);
    }

    private bool DropHeartOnDeath()
    {
        System.Random random = new System.Random();
        int chance = random.Next(100);
        if(chance >= HEARTH_CHANCE)
        {
            Instantiate(levelManager.heart, transform.position, Quaternion.identity);
            return true;
        }
        else
        {
            return false;
        }    
    }

    private bool DropMapOnDeath()
    {
        if (!levelManager.isMapSpawned)
        {
            
            System.Random random = new System.Random();
            int chance = random.Next(100);
            if (chance >= levelManager.mapDropChance)
            {
                
                Instantiate(levelManager.map, transform.position, Quaternion.identity);
                levelManager.isMapSpawned = true;
                return true;
            }
            else
            {
                levelManager.mapDropChance -= 2;
                return false;
            }
        }
        else
        {
            return false;
        }
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
}

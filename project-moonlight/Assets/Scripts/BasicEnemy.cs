using System.Collections;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public float speed = 0.4f;
    private float health = 20f;
    private Vector3 destination;
    private Transform player;
    private GameObject target;
    public bool isAiming = false;
    private bool aim = false;
    private const float RUSH_MULTIPLAYER = 3.5f;
    private const float RUSH_DISTANCE = 2f;

    [SerializeField] GameObject hearthPrefab;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player(Clone)").transform;
        SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (health <= 0)
        {
            DropItemOnDeath();
            Destroy(gameObject);
        }

        if (transform.localPosition == destination)
        {
            SetNewDestination();
        }

        float distance = Vector2.Distance(player.position, transform.position);

        if (isAiming && distance < RUSH_DISTANCE && !aim)
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
        speed = 0;
        isAiming = false;
        aim = false;
        yield return new WaitForSeconds(2);
        speed = temp / RUSH_MULTIPLAYER;
        SetNewDestination();
        yield return new WaitForSeconds(1);
        isAiming = true;
        Destroy(target);
    }

    private void DropItemOnDeath()
    {
        System.Random random = new System.Random();
        int chance = random.Next(100);
        Debug.Log(chance);
        if(chance >= 92)
        {
            Instantiate(hearthPrefab, transform.position, Quaternion.identity);
        }
    }
}

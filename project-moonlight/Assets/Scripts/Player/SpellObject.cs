using UnityEngine;

public class SpellObject : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float timeToLive = 3f;

    private float MAX_SPEED = 1.5f;
    private const float SLOWDOWN_FACTOR = 0.99f;
    private const float SPEEEDUP_FACTOR = 1.99f;

    private Vector3 INITIAL_POSITION = new(0, 0, 1);

    private Rigidbody2D spellRB;
    private float timer = 0f;

    Vector2 direction;

    private void Awake()
    {
        // Initialize the spell object
        InitializeSpell();
        MAX_SPEED *= PlayerStats.Instance.shootSpeed;
        transform.localScale *= PlayerStats.Instance.shootSize;

        if (PlayerStats.Instance.bouncingSpellPowerUp)
            timeToLive *= 2;
    }

    private void Update()
    {
        // Check the timer and speed of the spell object
        CheckTimer();
        CheckSpeed();

    }

    private void InitializeSpell()
    {
        // Set the initial position and get the Rigidbody2D component
        transform.localPosition = INITIAL_POSITION;
        spellRB = GetComponent<Rigidbody2D>();

        // Calculate the direction towards the mouse position and set the velocity
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - transform.position).normalized;
        spellRB.velocity = direction * moveSpeed;
    }

    private void CheckTimer()
    {
        // Increase the timer and destroy the spell object if the timer is up
        timer += Time.deltaTime;
        if(timer > 0.2f)
        {
            Collider2D[] colliders = GetComponents<Collider2D>();
            if (colliders.Length >= 2)
            {
                colliders[0].enabled = true;
                colliders[1].enabled = true;
            }

        }
        if (timer >= timeToLive)
        {
            Destroy(gameObject);
        }
    }

    private void CheckSpeed()
    {
        // Slow down the spell object if it is too fast, speed it up otherwise
        if (spellRB.velocity.sqrMagnitude > MAX_SPEED)
        {
            spellRB.velocity *= SLOWDOWN_FACTOR;
        }
        else
        {
            spellRB.velocity *= SPEEEDUP_FACTOR;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy the spell object if it collides with a wall

        if (other.CompareTag("Wall"))
        {
           if(!PlayerStats.Instance.bouncingSpellPowerUp)
                Destroy(gameObject);
            
        }
        if (other.CompareTag("Rock"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerStats.Instance.bouncingSpellPowerUp)
        { 
            ContactPoint2D contact = collision.contacts[0];
            Vector2 normal = contact.normal;
            Vector2 newDirection = Vector2.Reflect(direction, normal);
            direction = newDirection;
            spellRB.velocity = direction *moveSpeed;
        }
    }

}

using UnityEngine;

public class SpellObject : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float timeToLive = 3f;

    private const float MAX_SPEED = 1.5f;
    private const float SLOWDOWN_FACTOR = 0.99f;
    private const float SPEEEDUP_FACTOR = 1.99f;

    private Vector3 INITIAL_POSITION = new(0, 0, 1);

    private Rigidbody2D spellRB;
    private float timer = 0f;

    private void Awake()
    {
        // Initialize the spell object
        InitializeSpell();
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
        Vector2 direction = (mousePosition - transform.position).normalized;
        spellRB.velocity = direction * moveSpeed;
    }

    private void CheckTimer()
    {
        // Increase the timer and destroy the spell object if the timer is up
        timer += Time.deltaTime;
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
            Destroy(gameObject);
        }
    }
}

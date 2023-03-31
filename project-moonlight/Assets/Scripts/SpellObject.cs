using UnityEngine;

public class SpellObject : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float timeToLive = 3f;

    private const float MAX_SPEED = 4f;
    private const float SLOWDOWN_FACTOR = 0.99f;
    private const float SPEEEDUP_FACTOR = 1.99f;

    private Vector3 INITIAL_POSITION = new Vector3(0, 0, 1);

    private Rigidbody2D spellRB;
    private float timer = 0f;

    private void Awake()
    {
        InitializeSpell();
    }

    private void Update()
    {
        CheckTimer();

        CheckSpeed();
    }

    private void InitializeSpell()
    {
        transform.localPosition = INITIAL_POSITION;
        spellRB = GetComponent<Rigidbody2D>();

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        spellRB.velocity = direction * moveSpeed;
    }

    private void CheckTimer()
    {
        timer += Time.deltaTime;
        if (timer >= timeToLive)
        {
            Destroy(gameObject);
        }
    }

    private void CheckSpeed()
    {
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
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}

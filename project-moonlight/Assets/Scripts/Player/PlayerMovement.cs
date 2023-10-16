using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRB;

    [SerializeField]private PlayerStats playerStats;
    [SerializeField] public GameObject harvestIcon;
    private Vector2 movement;

    private Vector2 prevPosition;
    private SpriteRenderer spriteRenderer;

    [SerializeField] Sprite sprite;
    [SerializeField] Sprite spriteInverted;

    bool startIdle = false;
    bool isMoving = true;
    [SerializeField] Sprite idle1;
    [SerializeField] Sprite idle2;
    [SerializeField] Sprite invertedIdle1;
    [SerializeField] Sprite invertedIdle2;

    [SerializeField] float frequency = 0.5f;
    private float timer = 0;
    [SerializeField] GameObject slimePrefab;
    [SerializeField] GameObject shadow;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerStats= PlayerStats.Instance;
    }
    private void Update()
    {
        if (playerStats.wingsPowerup)
            shadow.SetActive(true);

        if(PlayerStats.Instance.toxicTracePowerUp)
        {
            timer += Time.deltaTime;
            DropSlime(frequency);
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            SaveSystem.BuildSaveObject(PlayerStats.Instance, Inventory.Instance);
            SceneManager.LoadScene(2);
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(movement.x != 0 || movement.y != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Rock") && playerStats.wingsPowerup)
        {
           /* MineRocks.Instance.rockTouched = true;
            MineRocks.Instance.rock = collision.gameObject;*/
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        playerRB.MovePosition(playerRB.position + movement * playerStats.speed * Time.fixedDeltaTime);
        if (prevPosition.x > transform.position.x)
        {
            spriteRenderer.sprite = spriteInverted;


        }
        else if (prevPosition.x < transform.position.x)
        {

            spriteRenderer.sprite = sprite;

        }
        else if(prevPosition.y < transform.position.y || prevPosition.y > transform.position.y)
        {

        }
        else if (prevPosition.x == transform.position.x)
        {
            if (spriteRenderer.sprite == sprite && !startIdle)
            {
                StartCoroutine(IdleAnim());
            }
            else if (spriteRenderer.sprite == spriteInverted && !startIdle)
            {
                StartCoroutine(InvertedIdleAnim());
            }
        }

        prevPosition = transform.position;
    }

    IEnumerator IdleAnim()
    {
        startIdle = true;
        if (!isMoving)
            spriteRenderer.sprite = idle1;
        yield return new WaitForSeconds(0.5f);
        if (!isMoving)
            spriteRenderer.sprite = idle2;
        yield return new WaitForSeconds(0.5f);
        if (!isMoving)
            spriteRenderer.sprite = sprite;
        startIdle = false;
    }

    IEnumerator InvertedIdleAnim()
    {
        startIdle = true;
        if(!isMoving)
            spriteRenderer.sprite = invertedIdle1;
        yield return new WaitForSeconds(0.5f);
        if (!isMoving)
            spriteRenderer.sprite = invertedIdle2;
        yield return new WaitForSeconds(0.5f);
        if (!isMoving)
            spriteRenderer.sprite = spriteInverted;
        startIdle = false;
    }

    private void DropSlime(float frequency)
    {
        if (timer >= frequency)
        {
            Instantiate(slimePrefab, transform.position, Quaternion.identity);
            timer = 0;
        }
    }


}

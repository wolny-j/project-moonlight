using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRB;

    [SerializeField]private PlayerStats playerStats;
    private Vector2 movement;

    private void Start()
    {
        playerStats= PlayerStats.Instance;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            SceneManager.LoadScene(1);
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerRB.MovePosition(playerRB.position + movement * playerStats.speed * Time.fixedDeltaTime);
    }
}

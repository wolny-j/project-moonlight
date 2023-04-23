using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerRB.MovePosition(playerRB.position + movement * playerStats.speed * Time.fixedDeltaTime);
    }
}

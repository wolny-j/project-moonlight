using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class OpenChest : MonoBehaviour
{
    private Animator animator;
    private bool isOpened = false;

    [SerializeField] GameObject speedGem;
    [SerializeField] GameObject powerGem;
    [SerializeField] GameObject shootingGem;

    void Start()
    {
       animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !isOpened)
        {
            animator.SetTrigger("openChest");
            SpawnRandomLoot();
            isOpened = true;
        }
    }

    private void SpawnRandomLoot()
    {
        int rand = Random.Range(1, 4);
        GameObject item = null;
        switch (rand) 
        { 
            case 1:
                item = Instantiate(speedGem, transform);
                break;
            case 2:
                item = Instantiate(powerGem, transform);
                break;
            case 3:
                item = Instantiate(shootingGem, transform);
                break;
        }
        item.transform.localPosition = Vector2.zero;
    }
}

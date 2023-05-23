using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OpenChest : MonoBehaviour
{
    private Animator animator;
    private bool isOpened = false;

    [SerializeField] GameObject speedGem;
    [SerializeField] GameObject powerGem;
    [SerializeField] GameObject shootingGem;
    [SerializeField] GameObject poppySeends;
    [SerializeField] GameObject dandelionSeends;

    void Start()
    {
       animator = GetComponent<Animator>();
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
        int rand = Random.Range(1, 9);
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
            case 4:
                item = Instantiate(poppySeends, transform);
                break;
            case 5:
                item = Instantiate(dandelionSeends, transform);
                break;
            case 6:
                item = Instantiate(speedGem, transform);
                break;
            case 7:
                item = Instantiate(powerGem, transform);
                break;
            case 8:
                item = Instantiate(shootingGem, transform);
                break;
        }
        StartCoroutine(AnimateLoot(item));
        item.transform.localPosition = Vector2.zero;
    }

    IEnumerator AnimateLoot(GameObject item)
    {  
        yield return new WaitForSeconds(1.1f);
            item.GetComponent<Collider2D>().isTrigger = true;
        

    }
}

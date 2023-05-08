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
        int rand = Random.Range(1, 5);
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

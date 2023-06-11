using System.Collections;
using UnityEngine;


public class OpenChest : MonoBehaviour
{
    private Animator animator;
    private bool isOpened = false;

    [SerializeField] GameObject speedGem;
    [SerializeField] GameObject powerGem;
    [SerializeField] GameObject shootingGem;
    [SerializeField] GameObject poppySeed;
    [SerializeField] GameObject dandelionSeed;
    [SerializeField] GameObject bambooSeed;

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
        int rand = Random.Range(1, 10);
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
                item = Instantiate(poppySeed, transform);
                break;
            case 5:
                item = Instantiate(dandelionSeed, transform);
                break;
            case 6:
                item = Instantiate(bambooSeed, transform);
                break;
            case 7:
                item = Instantiate(dandelionSeed, transform);
                break;
            case 8:
                item = Instantiate(poppySeed, transform);
                break;
            case 9:
                item = Instantiate(bambooSeed, transform);
                break;
        }
        StartCoroutine(AnimateLoot(item));
        item.transform.localPosition = Vector2.zero;
    }

    IEnumerator AnimateLoot(GameObject item)
    {  
        yield return new WaitForSeconds(0.3f);
            item.GetComponent<Collider2D>().isTrigger = true;
        

    }
}

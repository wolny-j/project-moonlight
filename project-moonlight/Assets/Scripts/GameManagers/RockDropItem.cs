using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDropItem : MonoBehaviour
{
    [SerializeField] GameObject gunpowderPrefab;
    [SerializeField] GameObject speedGemPrefab;
    [SerializeField] GameObject powerGemPrefab;
    [SerializeField] GameObject shootFrequencyGemPrefab;
    [SerializeField] GameObject goldenBar;

    [SerializeField] Sprite destroy1;
    [SerializeField] Sprite destroy2;

    [SerializeField] GameObject collider;

    private bool exploded = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.CompareTag("Explosion") || collision.CompareTag("EnemyExplosion")) && !exploded)
        {
            exploded= true;
            DestroyRock();
        }
    }
    public void DestroyRock()
    {
        if (!gameObject.scene.isLoaded)
        {
            return;
        }
        collider.SetActive(false);
        int dropChance = Random.Range(1, 100);
        if(dropChance > 70 && dropChance < 77 )
        {
            StartCoroutine(SpawnGem(goldenBar));
        }
        else if (dropChance >= 77 && dropChance < 93)
        {
            SpawnItem(gunpowderPrefab);
        }
        else if (dropChance >= 93)
        {
            SpawnRandomGem();
        }
        else
        {
            StartCoroutine(DestroyRockAnim());
        }
    }

    private void SpawnItem(GameObject prefab)
    {
        StartCoroutine(SpawnGunPowder(prefab));
    }

    private void SpawnRandomGem()
    {
        int gemIndex = Random.Range(1, 4);
        GameObject gemPrefab;

        switch (gemIndex)
        {
            case 1:
                if (PlayerStats.Instance.powerups["SpeedGem"] > 6)
                    gemPrefab = powerGemPrefab;
                else
                    gemPrefab = shootFrequencyGemPrefab;
                break;
            case 2:
                gemPrefab = powerGemPrefab;
                break;
            case 3:
                gemPrefab = shootFrequencyGemPrefab;
                break;
            default:
                gemPrefab = powerGemPrefab;
                break;
        }

        
        StartCoroutine(SpawnGem(gemPrefab));
        
    }

    IEnumerator SpawnGem(GameObject gemPrefab)
    {
        
        gameObject.GetComponent<SpriteRenderer>().sprite = destroy1;
        yield return new WaitForSeconds(0.5f);

        gameObject.GetComponent<SpriteRenderer>().sprite = destroy2;
        GameObject gem = Instantiate(gemPrefab, transform);

        yield return new WaitForSeconds(0.5f);

        gem.transform.localPosition = Vector2.zero;

        gem.GetComponent<Collider2D>().isTrigger = true;

        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;


    }

    IEnumerator SpawnGunPowder(GameObject prefab)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = destroy1;
        yield return new WaitForSeconds(0.5f);

        gameObject.GetComponent<SpriteRenderer>().sprite = destroy2;

       

        yield return new WaitForSeconds(0.5f);

        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(0.1f);
        GameObject item = Instantiate(prefab, transform);
        item.transform.SetParent(null);
    }
    IEnumerator DestroyRockAnim()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = destroy1;
        yield return new WaitForSeconds(0.5f);

        gameObject.GetComponent<SpriteRenderer>().sprite = destroy2;



        yield return new WaitForSeconds(0.5f);

        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;

    }
}

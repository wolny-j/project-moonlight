using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDropItem : MonoBehaviour
{
    [SerializeField] GameObject gunpowderPrefab;
    [SerializeField] GameObject speedGemPrefab;
    [SerializeField] GameObject powerGemPrefab;
    [SerializeField] GameObject shootFrequencyGemPrefab;

    [SerializeField] Sprite destroy1;
    [SerializeField] Sprite destroy2;
    public void DestroyRock()
    {
        if (!gameObject.scene.isLoaded)
        {
            return;
        }

        int dropChance = Random.Range(1, 100);

        if (dropChance >= 80 && dropChance < 95)
        {
            SpawnItem(gunpowderPrefab);
        }
        else if (dropChance >= 95)
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
                gemPrefab = speedGemPrefab;
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

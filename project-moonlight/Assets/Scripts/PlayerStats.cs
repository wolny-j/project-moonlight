using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private GameObject map;

    HealthUIManager healthUIManager;
    private bool immortality = false;

    public int health { get; set; } = 8;
    public bool isMapObtained { get; set; } = false;

    void Awake()
    {
        map = GameObject.Find("Map");
        map.SetActive(false);

        healthUIManager = GameObject.Find("GameManager").GetComponent<HealthUIManager>();
        healthUIManager.AddHealth(health);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && !immortality)
        {
            health--;
            healthUIManager.SubtractHealth(health);
            StartCoroutine(ShortImmortality());
        }
        if (collision.gameObject.CompareTag("Hearth") && health < 8)
        {
            health++;
            healthUIManager.AddHealth(health);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Map"))
        {
            map.SetActive(true);
            isMapObtained= true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Spikes"))
        {
            health--;
            healthUIManager.SubtractHealth(health);
            StartCoroutine(ShortImmortality());
        }
    }

    IEnumerator ShortImmortality()
    {
        immortality = true;
        yield return new WaitForSeconds(1.5f);
        immortality= false;
    }
    
}

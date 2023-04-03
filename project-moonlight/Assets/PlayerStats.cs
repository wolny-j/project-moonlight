using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health { get; set; } = 8;
    HealthUIManager healthUIManager;
    private bool immortality = false;
    void Start()
    {
        healthUIManager = GameObject.Find("GameManager").GetComponent<HealthUIManager>();
        healthUIManager.AddHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && !immortality)
        {
            health--;
            healthUIManager.SubstractHealth(health);
            StartCoroutine(ShortImmortality());
        }
        if (collision.gameObject.CompareTag("Hearth") && health < 8)
        {
            health++;
            healthUIManager.AddHealth(health);
            Destroy(collision.gameObject);
        }
    }

    IEnumerator ShortImmortality()
    {
        immortality = true;
        yield return new WaitForSeconds(1.5f);
        immortality= false;
    }
    
}

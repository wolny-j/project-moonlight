using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingDamage : MonoBehaviour
{
    private bool immortality = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckDamage(collision, "Enemy", PlayerStats.Instance.health);
        CheckDamage(collision, "Spikes", PlayerStats.Instance.health);
        CheckDamage(collision, "Slime", PlayerStats.Instance.health);
    }

    void CheckDamage(Collider2D collision, string tag, int health)
    {

        if (collision.gameObject.CompareTag(tag) && !immortality)
        {
            health--;
            PlayerStats.Instance.health = health;
            HealthUIManager.Instance.SubtractHealth(health);
            StartCoroutine(ShortImmortality());
        }
    }

    IEnumerator ShortImmortality()
    {
        immortality = true;
        yield return new WaitForSeconds(1.5f);
        immortality = false;
    }
}

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
        CheckDamage(collision, "EnemySpell", PlayerStats.Instance.health);
    }

    void CheckDamage(Collider2D collision, string tag, int health)
    {

        if (collision.gameObject.CompareTag(tag) && !immortality)
        {
            health--;
            PlayerStats.Instance.health = health;
            HealthUIManager.Instance.SubtractHealth(health);

            StartCoroutine(BlinkImmortalityAnimation());
            if(tag == "EnemySpell")
            {
                Destroy(collision.gameObject);
            }
        }
    }


    IEnumerator BlinkImmortalityAnimation()
    {
        immortality = true;
        var spriteRenderer = GetComponent<SpriteRenderer>();
        var invisibleColor = new Color32(0, 0, 0, 0);
        var currentColor = spriteRenderer.color;

        ChangeColor(spriteRenderer, invisibleColor);

        for(int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(.1f );
            ChangeColor(spriteRenderer, currentColor);

            yield return new WaitForSeconds(.1f);
            ChangeColor(spriteRenderer, invisibleColor);
        }
        
        yield return new WaitForSeconds(.2f);
        ChangeColor(spriteRenderer, currentColor);
        yield return new WaitForSeconds(.3f);
        immortality = false;
    }

    private void ChangeColor(SpriteRenderer spriteRenderer, Color32 color)
    {
        spriteRenderer.color = color;
    }
}

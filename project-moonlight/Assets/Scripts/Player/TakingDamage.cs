using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingDamage : MonoBehaviour
{
    private bool immortality = false;

    [SerializeField] AudioSource takeDamageSource;

    [SerializeField] GameObject shieldUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckDamage(collision, "Enemy", PlayerStats.Instance.health);
        CheckDamage(collision, "Spikes", PlayerStats.Instance.health);
        CheckDamage(collision, "Slime", PlayerStats.Instance.health);
        CheckDamage(collision, "EnemySpell", PlayerStats.Instance.health);
        CheckDamage(collision, "MagicWall", PlayerStats.Instance.health);
    }

    void CheckDamage(Collider2D collision, string tag, int health)
    {
        if ((tag == "Slime" || tag == "Spikes") && PlayerStats.Instance.wingsPowerup)
            return;

        if (collision.gameObject.CompareTag(tag) && !immortality)
        {
            if(PlayerStats.Instance.isShieldActive)
            {
                PlayerStats.Instance.isShieldActive = false;
            }
            else
            {
                health--;
                PlayerStats.Instance.health = health;
                HealthUIManager.Instance.SubtractHealth(health);
            }
            

            takeDamageSource.Play();
            StartCoroutine(BlinkImmortalityAnimation());
            if(tag == "EnemySpell")
            {
                Destroy(collision.gameObject);
            }

        }

        PlayerStats.Instance.CheckDeath();
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

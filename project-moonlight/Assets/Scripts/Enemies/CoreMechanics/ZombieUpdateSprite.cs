using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class ZombieUpdateSprite : MonoBehaviour, ISpriteUpdate
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] Sprite sprite;
    [SerializeField] Sprite spriteInverted;

    [SerializeField] Sprite RushSprite;
    [SerializeField] Sprite rushSpriteInverted;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateSprite(Vector3 destination)
    {
        bool aim = GetComponent<ZombieEnemy>().aim;
        if (destination.x > transform.localPosition.x)
        {
            
            if (aim)
            {
                spriteRenderer.sprite = RushSprite;
            }
            else
            {
                spriteRenderer.sprite = sprite;
            }
        }
        else
        {

            if (aim)
            {
                spriteRenderer.sprite = rushSpriteInverted;
            }
            else
            {
                spriteRenderer.sprite = spriteInverted;
            }
        }
    }

    public void BlinkAnimation()
    {
        StartCoroutine(BlinkAnimationCorutine());
    }

    IEnumerator BlinkAnimationCorutine()
    {
        var invisibleColor = new Color32(255, 58, 0, 180);
        var currentColor = spriteRenderer.color;

        ChangeColor(spriteRenderer, invisibleColor);


        yield return new WaitForSeconds(.1f);
        ChangeColor(spriteRenderer, currentColor);
    }

    private void ChangeColor(SpriteRenderer spriteRenderer, Color32 color)
    {
        spriteRenderer.color = color;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUpdateSprite : MonoBehaviour, ISpriteUpdate
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] Sprite sprite;
    [SerializeField] Sprite spriteInverted;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateSprite(Vector3 destination)
    {
        if (destination.x > transform.localPosition.x)
        {
            spriteRenderer.sprite = spriteInverted;

        }
        else
        {
            spriteRenderer.sprite = sprite;
        }
    }

    public void BlinkAnimation ()
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

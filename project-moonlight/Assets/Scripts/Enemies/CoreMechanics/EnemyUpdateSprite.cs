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
}

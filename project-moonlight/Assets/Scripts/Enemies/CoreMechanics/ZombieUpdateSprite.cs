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
}

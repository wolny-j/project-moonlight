using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSegment : MonoBehaviour
{
    // Fields
    private SpriteRenderer spriteRenderer;
    private Item seed;
    private int growingIndex = 0;
    private bool isGrowing = false;
    private bool isFirstStage = false;

    public static FieldSegment currentHighlightedSquare;
    public static FieldSegment nextHighlightedSquare;

    private static readonly Color32 normalColor = new(87, 2, 2, 255);
    private static readonly Color32 highlightedColor = new(145, 43, 13, 255);

    // Properties
    public bool IsGrowing => isGrowing;

    // Methods
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && currentHighlightedSquare == this)
        {
            if (!isFirstStage)
            {
                isFirstStage = true;
                spriteRenderer.transform.localScale *= 2f;
            }
            spriteRenderer.sprite = seed.growingSprites[growingIndex++];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (currentHighlightedSquare == null && !isGrowing)
        {
            HighlightField.Highlight(spriteRenderer);
            currentHighlightedSquare = this;
        }
        else
        {
            nextHighlightedSquare = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (currentHighlightedSquare == this && !isGrowing)
        {
            HighlightField.Dim(spriteRenderer);
            if (nextHighlightedSquare != null)
            {
                currentHighlightedSquare = nextHighlightedSquare;
                HighlightField.Highlight(currentHighlightedSquare.spriteRenderer);
                nextHighlightedSquare = null;
            }
            else
            {
                currentHighlightedSquare = null;
                nextHighlightedSquare = null;
            }
        }
        else if (currentHighlightedSquare == this && isGrowing)
        {
            currentHighlightedSquare = null;
            nextHighlightedSquare = null;
            HighlightField.White(spriteRenderer);
        }
    }

    public void GetSeed(Item item)
    {
        seed = item;
        spriteRenderer.sprite = item.sprite;
        HighlightField.White(spriteRenderer);
        currentHighlightedSquare.isGrowing = true;
    }
}

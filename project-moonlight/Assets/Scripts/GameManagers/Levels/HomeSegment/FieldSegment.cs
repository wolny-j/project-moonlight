using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSegment : MonoBehaviour
{
    // Fields
    private SpriteRenderer spriteRenderer;
    public Item seed = null;
    public HomeFieldDTO data;
    [SerializeField] int fieldIndex;
    [SerializeField] Sprite normalFieldSprite;

    private int growingIndex = 0;
    private bool isGrowing = false;

    public static FieldSegment currentHighlightedSquare;
    public static FieldSegment nextHighlightedSquare;



    // Methods
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && currentHighlightedSquare.growingIndex >= 3 && currentHighlightedSquare == this)
        {
            bool result = false;
            switch (seed.name)
            {
                case "Poppy Seed":
                    result = HarvestManager.Instance.HarvestPoppy();
                    if (result)
                    {
                        ResetField();
                    }
                    break;
                case "Dandelion Seed":
                    result = HarvestManager.Instance.HarvestDandelion();
                    if (result)
                    {
                        ResetField();
                    }
                    break;
                case "Bamboo Seed":
                    result = HarvestManager.Instance.HarvestBamboo();
                    if (result)
                    {
                        ResetField();
                    }
                    break;
            }
        }
    }

    private void ResetField()
    {
        seed = null;
        data = null;
        FieldManager.Instance.Add(data);
        SaveSystem.SaveHarvestField();
        spriteRenderer.sprite = normalFieldSprite;
        growingIndex = 0;
        isGrowing = false;
        HighlightField.Dim(spriteRenderer);
        spriteRenderer.transform.localScale /= 2f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (currentHighlightedSquare == null)
        {
            if(!isGrowing)
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
                if(!isGrowing)
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
        isGrowing = true;
        //currentHighlightedSquare.isGrowing = true;
    }
    public void Grow()
    {
        if (growingIndex == 0)
        {
            spriteRenderer.sprite = seed.growingSprites[growingIndex++];
            spriteRenderer.transform.localScale *= 2f;
        }
        else if (growingIndex < 3)
            spriteRenderer.sprite = seed.growingSprites[growingIndex++];
        else
            growingIndex++;

        if(growingIndex == 5)
            ResetField();


    }
    private void OnDisable()
    {
        if(seed != null)
        {
            data = new(growingIndex, isGrowing, seed.name, fieldIndex);
            FieldManager.Instance.Add(data);
            SaveSystem.SaveHarvestField();
        }
        
    }

}

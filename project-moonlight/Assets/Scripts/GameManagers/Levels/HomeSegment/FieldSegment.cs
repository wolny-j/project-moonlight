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

    private void Start()
    {
        int chance = UnityEngine.Random.Range(0, 100);

        if (!isGrowing && chance > 97)
        {
            Debug.Log("Grow");
            GetSeed(ItemsList.Instance.cloverSeed);
            Grow();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && currentHighlightedSquare.growingIndex >= 3 && currentHighlightedSquare == this)
        {

            bool result;
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
                case "Clover Seed":
                    result = HarvestManager.Instance.HarvestClover();
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
            currentHighlightedSquare = this;
            HighlightField.Highlight(currentHighlightedSquare.spriteRenderer);
        }
        else
        { 
            nextHighlightedSquare = this;
        }
        if (currentHighlightedSquare.growingIndex == 3 && currentHighlightedSquare == this)
        {
            GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().harvestIcon.SetActive(true);
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
            
                HighlightField.White(currentHighlightedSquare.spriteRenderer);
            if (nextHighlightedSquare != null)
            {
                currentHighlightedSquare = nextHighlightedSquare;
                nextHighlightedSquare = null;
                HighlightField.Highlight(currentHighlightedSquare.spriteRenderer);
            }
            else
            {
                currentHighlightedSquare = null;
                nextHighlightedSquare = null;
            }
        }
        GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().harvestIcon.SetActive(false);
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

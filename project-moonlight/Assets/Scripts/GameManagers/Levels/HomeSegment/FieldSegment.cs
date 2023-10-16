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
    public bool isGrowing = false;

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

        if (!isGrowing && (chance > 91 && chance <= 95))
        {
            GetSeed(ItemsList.Instance.cloverSeed);
            Grow();
        }
        if(!isGrowing && chance  > 95)
        {
            GetSeed(ItemsList.Instance.weed);
            Grow();
        }
    }

    private void Update()
    {
        if(currentHighlightedSquare != null)
        {
            if (currentHighlightedSquare.growingIndex >= 3 && currentHighlightedSquare == this)
            {
                GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().harvestIcon.SetActive(true);
            }
            else if(PlayerStats.Instance.pickaxe1.hasPickaxe && currentHighlightedSquare == this && seed.name == "Weed")
            {
                GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().harvestIcon.SetActive(true);
            }

        }
        
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
                    InventoryUI.Instance.UpdtaeClover();
                    break;
                case "Starfruit Seed":
                    result = HarvestManager.Instance.HarvestStarfruit();
                    if (result)
                    {
                        ResetField();
                    }
                    break;


            }
        }
        if(Input.GetKeyDown(KeyCode.G) && PlayerStats.Instance.pickaxe1.hasPickaxe && currentHighlightedSquare == this)
        {
            switch (seed.name)
            {
                case "Weed":
                    PlayerStats.Instance.UpdatePickaxe();
                    ResetField();
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
        else
        {
            HighlightField.Dim(spriteRenderer);
            currentHighlightedSquare = null;
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


        if (seed.name != "Weed")
        {
            if (growingIndex == 5)
                ResetField();
        }

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

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] Sprite cButton;
    [SerializeField] Sprite handIcon;
    [SerializeField] Sprite gemIcon;
    [SerializeField] Sprite chestArrowIcon;

    [SerializeField] Text title;
    [SerializeField] Text description;
    [SerializeField] Image icon;
    [SerializeField] GameObject panel;
    [SerializeField] Button button;

    private int counter = 0;
    struct TutorialPanel
    {
        public string title;
        public string description;
        public Sprite icon;
    }
    TutorialPanel crafting = new TutorialPanel();
    TutorialPanel harvesting = new TutorialPanel();
    TutorialPanel gems = new TutorialPanel();
    TutorialPanel chest = new TutorialPanel();

    List<string> craftableItems = new List<string>() { "Eye", "Brain", "Shell", "String", "Web", "Dandelion", "Bamboo", "Gunpowder", "Poppy" };
    List<string> seedItems = new List<string>() { "Poppy Seed", "Dandelion Seed", "Bamboo Seed"};

    List<string> panelsList = new List<string>();
    private void Start()
    {

        crafting.title = "Crafting";
        crafting.description = "To open the crafting menu, press the C key. Within the crafting panel, you have the ability to craft items and upgrades for your character.";
        crafting.icon = cButton;


        harvesting.title = "Harvesting";
        harvesting.description = "If you come across a seed, you have the option to plant it by utilizing the hand icon while you are standing on the empty field. " +
            "After a few rounds, it will mature and be ready for harvesting, which can be done by pressing the G button.";
        harvesting.icon = handIcon;


        chest.title = "Chest";
        chest.description = "You can store your items in chest. They will be save here when you left your home. To open chest and your inventory press TAB button. To move them between your inventory and the chest press yellow arrow button.";
        chest.icon = chestArrowIcon;

        title.text = crafting.title;
        description.text = crafting.description;
        icon.sprite = crafting.icon;

        /*if (CheckCraftingTutorial())
        {
            panelsList.Add("Crafting");
            panelsList.Add("Chest");
        }
        if (CheckHarvestingTutorial())
        {
            panelsList.Add("Harvesting");
        }

        if (panelsList.Count > 0)
        {
            panel.SetActive(true);


            switch (panelsList[0])
            {
                case "Crafting":

                    title.text = crafting.title;
                    description.text = crafting.description;
                    icon.sprite = crafting.icon;
                    break;
                case "Harvesting":
                    title.text = harvesting.title;
                    description.text = harvesting.description;
                    icon.sprite = harvesting.icon;
                    break;
                case "Chest":
                    title.text = chest.title;
                    description.text = chest.description;
                    icon.sprite = chest.icon;
                    break;
            }
            panelsList.Remove(panelsList[0]);
        }
        else
        {
            panel.SetActive(false);
        }*/
    }

    public void NextButton()
    {
        counter++;

        switch (counter)
        {
            case 1:
                title.text = harvesting.title;
                description.text = harvesting.description;
                icon.sprite = harvesting.icon;
                break;
            case 2:
                title.text = chest.title;
                description.text = chest.description;
                icon.sprite = chest.icon;
                break;
            case 3:
                panel.SetActive(false);
                break;
        }
    }

    /* public void NextButton()
     {
         if (panelsList.Count > 0)
         {
             foreach (var panel in panelsList)
             {
                 switch (panel)
                 {
                     case "Crafting":

                         title.text = crafting.title;
                         description.text = crafting.description;
                         icon.sprite = crafting.icon;
                         break;
                     case "Harvesting":
                         title.text = harvesting.title;
                         description.text = harvesting.description;
                         icon.sprite = harvesting.icon;
                         break;
                     case "Chest":
                         title.text = chest.title;
                         description.text = chest.description;
                         icon.sprite = chest.icon;
                         break;
                 }
                 panelsList.Remove(panelsList[0]);
             }
         }
         else
         {
             panel.SetActive(false);
         }
     }*/

    /*private bool CheckCraftingTutorial()
    {
        foreach(var item in craftableItems)
        {
            int result = CraftingManager.Instance.GetItemCount(item);
            if(result > 0)
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckHarvestingTutorial()
    {
        foreach(var item in seedItems)
        {
            int result = CraftingManager.Instance.GetItemCount(item);
            if (result > 0)
            {
                return true;
            }
        }
        return false;
    }*/

}

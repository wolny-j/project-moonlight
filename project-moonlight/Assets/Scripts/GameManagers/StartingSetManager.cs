using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StartingSetManager : MonoBehaviour
{
    [SerializeField] List<GameObject> panels;
    [SerializeField] GameObject gemsSetPanel;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject tutorialPanel;

    private int gemsCounter = 1;

    private void Start()
    {
        RandomizeSets();
        RandomizeSets();
        for (int i = 0; i < 3; i++)
        {
            panels[i].SetActive(true);
        }
        if (PlayerStats.Instance.level != 1)
        {
            panel.SetActive(false);
        }

    }

    private void RandomizeSets()
    {
        System.Random rng = new System.Random();

        int n = panels.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            GameObject temp = panels[k];
            panels[k] = panels[n];
            panels[n] = temp;
        }
    }

    public void AdventureSet()
    {
        Inventory.Instance.AddItem(ItemsList.Instance.stringItem);
        Inventory.Instance.AddItem(ItemsList.Instance.shell);
        CloseAllSets(true);
    }

    public void HarvesterSet()
    {
        PlayerStats.Instance.luck += 1;
        Inventory.Instance.AddItem(ItemsList.Instance.dandelionSeed);
        Inventory.Instance.AddItem(ItemsList.Instance.poppySeed);
        Inventory.Instance.AddItem(ItemsList.Instance.bambooSeed);
        InventoryUI.Instance.UpdtaeClover();
        CloseAllSets(true);
    }
    public void SurvivorSet()
    {
        Inventory.Instance.AddItem(ItemsList.Instance.brain);
        Inventory.Instance.AddItem(ItemsList.Instance.eye);
        Inventory.Instance.AddItem(ItemsList.Instance.poppy);
        CloseAllSets(true);
    }
    public void LuckySet()
    {
        PlayerStats.Instance.luck += 4;
        gemsCounter = 2;
        InventoryUI.Instance.UpdtaeClover();
        CloseAllSets(true);
    }
    public void MinerSet()
    {
        PlayerStats.Instance.AddPickaxe();
        CloseAllSets(true);
    }
    public void DestroyerSet()
    {
        PlayerStats.Instance.dynamiteCounter += 4;
        UseDynamite.Instance.UpdateCounterUI();
        gemsCounter = 2;
        CloseAllSets(true);
    }

    public void JewelerSet()
    {
        gemsCounter = 4;
        CloseAllSets(true);
    }

    private void CloseAllSets(bool activateGemsPanel)
    {
        foreach(GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        gemsSetPanel.SetActive(activateGemsPanel);
    }


    public void AddRedGem()
    {
        PlayerStats.Instance.AddPowerup("PowerGem");
        if (gemsCounter == 1)
            ClosePanels();
        else
            gemsCounter--;
    }
    public void AddBlueGem()
    {
        PlayerStats.Instance.AddPowerup("SpeedGem");
        if (gemsCounter == 1)
            ClosePanels();
        else
            gemsCounter--;
    }
    public void AddPinkGem()
    {
        PlayerStats.Instance.AddPowerup("ShootGem");
        if (gemsCounter == 1)
            ClosePanels();
        else
            gemsCounter--;
    }

    private void ClosePanels()
    {
        gemsSetPanel.SetActive(false);
        panel.SetActive(false);
        tutorialPanel.SetActive(true);
    }
}

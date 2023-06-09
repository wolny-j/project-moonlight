using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StartingSetManager : MonoBehaviour
{
    [SerializeField] List<GameObject> panels;
    [SerializeField] GameObject gemsSetPanel;
    [SerializeField] GameObject panel;

    private void Start()
    {
        if (PlayerStats.Instance.level != 1)
        {
            panel.SetActive(false);
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
        PlayerStats.Instance.luck = 3;
        Inventory.Instance.AddItem(ItemsList.Instance.dandelion);
        Inventory.Instance.AddItem(ItemsList.Instance.poppy);
        Inventory.Instance.AddItem(ItemsList.Instance.bamboo);
        CloseAllSets(true);
    }
    public void SurvivorSet()
    {
        Inventory.Instance.AddItem(ItemsList.Instance.brain);
        Inventory.Instance.AddItem(ItemsList.Instance.eye);
        Inventory.Instance.AddItem(ItemsList.Instance.poppy);
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
        ClosePanels();
    }
    public void AddBlueGem()
    {
        PlayerStats.Instance.AddPowerup("SpeedGem");
        ClosePanels();
    }
    public void AddPinkGem()
    {
        PlayerStats.Instance.AddPowerup("ShootGem");
        ClosePanels();
    }

    private void ClosePanels()
    {
        gemsSetPanel.SetActive(false);
        panel.SetActive(false);
    }
}

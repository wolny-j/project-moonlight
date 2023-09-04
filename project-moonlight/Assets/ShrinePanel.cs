using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShrinePanel : MonoBehaviour
{
    private bool used = false;
    [SerializeField] Text title;
    [SerializeField] Text buttonText;
    [SerializeField] GameObject resultPanel;
    public void Sacrifice()
    {
        if (used)
        {
            HealthUIManager.Instance.SubstractHealthContainer();
            resultPanel.SetActive(true);
            title.text = "Don't be too greedy!";
        }
        else
        {

            HealthUIManager.Instance.SubstractHealthContainer();
            int random = UnityEngine.Random.Range(1, 5);
            resultPanel.SetActive(true);
            switch (random)
            {
                case 1:
                    PlayerStats.Instance.bouncingSpellPowerUp = true;
                    title.text = "Bouncing spell unlocked!";
                    break;
                case 2:
                    PlayerStats.Instance.toxicTracePowerUp = true;
                    title.text = "Toxic trace unlocked!";
                    break;
                case 3:
                    PlayerStats.Instance.AddPowerup("SpeedGem");
                    PlayerStats.Instance.AddPowerup("PowerGem");
                    PlayerStats.Instance.AddPowerup("ShootGem");
                    title.text = "3 gems unlocked!";
                    break;
                case 4:
                    HealthUIManager.Instance.AddHealthContainer();
                    HealthUIManager.Instance.AddHealthContainer();
                    HealthUIManager.Instance.AddHealthContainer();
                    title.text = "2 health containers unlocked!";
                    break;
            }
            used = true;
            buttonText.text = "Only one sacrifice per level";
        }
    }

    public void CloseResultPanel()
    {
        resultPanel.SetActive(false);
    }
}

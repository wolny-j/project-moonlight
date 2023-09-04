using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIManager : MonoBehaviour
{
    [Tooltip("List of heart GameObjects representing the player's health")]
    List<GameObject> hearts;

    public static HealthUIManager Instance;

    private void Awake()
    {
        hearts = new List<GameObject>();
        if (Instance == null)
        {
            Instance = this;
            for (int i = 0; i < 18; i++)
            {
                hearts.Add(GameObject.Find($"Hearth ({i})"));
            }
            
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        
    }

   

    public void InitializeHearth(int healthContainers)
    {
        for (int i = PlayerStats.Instance.maxHealth - 1; i >= 0; i--)
        {
            if (i >= 0 && i < hearts.Count)
            {
                if (i >= PlayerStats.Instance.health && i >= healthContainers)
                    hearts[i].SetActive(false);
                else if (i >= PlayerStats.Instance.health && i < healthContainers)
                {
                    hearts[i].SetActive(true);
                    hearts[i].GetComponent<Image>().color = Color.black;
                }
            }
            else
            {
                Debug.LogWarning("Index out of range: " + i);
            }
        }
    }

    public void SubtractHealth(int health)
    {
        //hearts[PlayerStats.Instance.health -1].GetComponent<Image>().color = Color.black;
        for (int i = PlayerStats.Instance.healthContainers; i > health - 1; i--)
        {
            if (i >= 0 && i < hearts.Count)
            {
                hearts[i].GetComponent<Image>().color = Color.black;
            }
            else
            {
                Debug.LogWarning("Index out of range: " + i);
            }
        }
    }



    public void AddHealth()
    {

        hearts[PlayerStats.Instance.health - 1].GetComponent<Image>().color = Color.white;



        /*for (int i = 0; i < health; i++)
        {
            if (i >= 0 && i < health)
            {
                hearts[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
            else
            {
                Debug.LogWarning("Index out of range: " + i);
            }
        }*/
    }

    public void AddHealthContainer()
    {
        PlayerStats.Instance.healthContainers++;
        hearts[PlayerStats.Instance.healthContainers - 1].SetActive(true);
        hearts[PlayerStats.Instance.healthContainers - 1].GetComponent<Image>().color = Color.black;
    }

    public void SubstractHealthContainer()
    {
        PlayerStats.Instance.healthContainers--;
        PlayerStats.Instance.health--;
        hearts[PlayerStats.Instance.healthContainers].SetActive(false);
    }
}

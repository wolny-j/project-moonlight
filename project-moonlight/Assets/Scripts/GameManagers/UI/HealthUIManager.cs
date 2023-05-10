using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            for (int i = 0; i < 8; i++)
            {
                hearts.Add(GameObject.Find($"Hearth ({i})"));
            }
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        
    }

    public void SubtractHealth(int health)
    {
        for (int i = 7; i > health - 1; i--)
        {
            if (i >= 0 && i < hearts.Count)
            {
                hearts[i].SetActive(false);
            }
            else
            {
                Debug.LogWarning("Index out of range: " + i);
            }
        }
    }

    public void AddHealth(int health)
    {
        for (int i = 0; i < health; i++)
        {
            if (i >= 0 && i < hearts.Count)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                Debug.LogWarning("Index out of range: " + i);
            }
        }
    }
}

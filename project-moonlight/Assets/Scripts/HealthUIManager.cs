using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIManager : MonoBehaviour
{
    [Tooltip("List of heart GameObjects representing the player's health")]
    [SerializeField] List<GameObject> hearts;

    public static HealthUIManager Instance;

    private void Awake()
    {
        Instance = this;
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

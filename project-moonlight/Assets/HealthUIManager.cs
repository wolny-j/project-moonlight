using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIManager : MonoBehaviour
{
    [SerializeField] List<GameObject> hearths;
    void Start()
    {

    }



    public void SubstractHealth(int health)
    {
        for (int i = 7; i > health - 1; i--)
        {
            hearths[i].SetActive(false);
        }
    }

    public void AddHealth(int health)
    {
        for (int i = 0; i < health; i++)
        {
            hearths[i].SetActive(true);
        }
    }
}

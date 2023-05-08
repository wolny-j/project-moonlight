using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    private static GameManager instance;
    private void Awake()
    {
        // If the instance is not set, set it to this script
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        // If the instance is already set to another script, destroy this script
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}

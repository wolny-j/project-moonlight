using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitHome : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SaveSystem.BuildSaveObject(PlayerStats.Instance, Inventory.Instance);
            SaveSystem.BuildSaveChest(ChestInventory.Instance);
            SceneManager.LoadScene(0);
        }
    }
}

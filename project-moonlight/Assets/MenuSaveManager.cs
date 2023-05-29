using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSaveManager : MonoBehaviour
{
    private void Start()
    {
        var playerData = new PlayerStatsDTO();
        var chestData = new ChestDTO();

        //FieldManager.Instance.RestetFields();
        
        SaveSystem.SavePlayer(playerData);
        SaveSystem.SaveChest(chestData);
        //SaveSystem.SaveHarvestField();

    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}

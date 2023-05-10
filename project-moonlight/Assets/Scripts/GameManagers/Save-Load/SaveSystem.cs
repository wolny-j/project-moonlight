using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string savePath = Application.persistentDataPath + "/temp.save";

    public static void BuildSaveObject(PlayerStats stats, Inventory inventory)
    {
        var data = new PlayerSaveData(stats, inventory);
        SavePlayer(data);
    }

    public static void SavePlayer(PlayerSaveData data)
    {
        var formatter = new BinaryFormatter();
        using var fileStream = File.Create(savePath);
        formatter.Serialize(fileStream, data);
        Debug.Log($"Saved in {savePath}");
    }

    public static PlayerSaveData LoadPlayer()
    {
        if (File.Exists(savePath))
        {
            var formatter = new BinaryFormatter();
            using var fileStream = File.Open(savePath, FileMode.Open);
            var data = formatter.Deserialize(fileStream) as PlayerSaveData;
            return data;
        }
        else
        {
            Debug.Log("File doesn't exist");
            return null;
        }
    }
}

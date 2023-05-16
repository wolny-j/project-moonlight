using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string savePlayerPath = Application.persistentDataPath + "/player_temp.save";
    private static readonly string saveFieldPath = Application.persistentDataPath + "/field_temp.save";

    public static void BuildSaveObject(PlayerStats stats, Inventory inventory)
    {
        var data = new PlayerSaveData(stats, inventory);
        SavePlayer(data);
    }

    public static void SavePlayer(PlayerSaveData data)
    {
        var formatter = new BinaryFormatter();
        using var fileStream = File.Create(savePlayerPath);
        formatter.Serialize(fileStream, data);
        Debug.Log($"Saved in {savePlayerPath}");
    }

    public static void SaveHarvestField()
    {
        var formatter = new BinaryFormatter();
        using var fileStream = File.Create(saveFieldPath);
        formatter.Serialize(fileStream, FieldManager.Instance.GetFields());
        Debug.Log($"Saved in {saveFieldPath}");
    }

    public static PlayerSaveData LoadPlayer()
    {
        if (File.Exists(savePlayerPath))
        {
            var formatter = new BinaryFormatter();
            using var fileStream = File.Open(savePlayerPath, FileMode.Open);
            var data = formatter.Deserialize(fileStream) as PlayerSaveData;
            return data;
        }
        else
        {
            Debug.Log("File doesn't exist");
            return null;
        }
    }

    public static FieldsListSaveData LoadField()
    {
        if (File.Exists(savePlayerPath))
        {
            var formatter = new BinaryFormatter();
            using var fileStream = File.Open(saveFieldPath, FileMode.Open);

            try
            {
                var data = formatter.Deserialize(fileStream) as FieldsListSaveData;
                return data;
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }
        else
        {
            Debug.Log("File doesn't exist");
            return null;
        }
    }

    public static void DeleteFromPath(string path)
    {
        try
        {
            File.Delete(path);
            Debug.Log($"File {path} deleted");
        }
        catch (IOException e)
        {
            Debug.Log($"Wyst¹pi³ b³¹d podczas usuwania pliku: {e.Message}");
        }
    }
}

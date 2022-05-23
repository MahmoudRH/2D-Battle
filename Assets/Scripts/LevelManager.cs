using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class LevelManager
{
    private static string filePath = Application.persistentDataPath + "/playerData.sv";
    public static void SaveGame(PlayerData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath,FileMode.Create);
        formatter.Serialize(stream,data);
        stream.Close();
    }

    public static PlayerData LoadGame()
    {
        Debug.Log("Loading Game...");
        PlayerData playerData;
        if (File.Exists(filePath))
        {
            Debug.Log("Loading Game, File Exists: "+filePath);
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Open);
            playerData = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
        }
        else
        {
            playerData = new PlayerData(1); 
        }

        return playerData;
    }
}

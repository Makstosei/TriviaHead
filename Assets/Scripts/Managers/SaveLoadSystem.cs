using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoadSystem
{
    

    public static void SavePlayer(PlayerData playerData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.Makstosei";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData playerdataManager = new PlayerData(playerData);

        formatter.Serialize(stream, playerdataManager);
        stream.Close();
    }

    public static PlayerData LoadData()
    {

        string path = Application.persistentDataPath + "/player.Makstosei";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            formatter.Deserialize(stream);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save File Not Found in "+path);
            return null;
        }


    }



}

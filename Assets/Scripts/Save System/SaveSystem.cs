using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static BinaryFormatter _formatter = new BinaryFormatter();
    private static FileStream _stream;

    public static void Save(SaveData data)
    {
        _stream = new FileStream(GetPath(), FileMode.Create);

        _formatter.Serialize(_stream, data);
        _stream.Close();
    }

    public static SaveData Load(SaveData data)
    {
        if (File.Exists(GetPath()))
        {
            _stream = new FileStream(GetPath(), FileMode.Open);

            data = _formatter.Deserialize(_stream) as SaveData;
            _stream.Close();
        }
        else
            Debug.LogWarning("Failed to load factory data, file not found.");

        return data;
    }

    private static string GetPath()
    {
        return Application.persistentDataPath + "/data.sav";
    }



    // public static void Save(SaveData data, string id)
    // {
    //     PlayerPrefs.SetString(id, JsonUtility.ToJson(data));
    // }

    // public static SaveData Load(SaveData data, string id)
    // {
    //     if (PlayerPrefs.HasKey(id))
    //     {
    //         data = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(id));
    //     }
    //     else
    //         Debug.LogWarning("Failed to load factory data, key not found.");

    //     return data;
    // }
}
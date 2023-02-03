using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

// public class SaveSystem
// {
//     public void Save (SaveData data)
//     {
//         BinaryFormatter formatter = new BinaryFormatter();
//         FileStream fileStream = new FileStream(GetPath(), FileMode.Create);
//         formatter.Serialize(fileStream, data);
//         fileStream.Close();
//     }

//     public SaveData Load(SaveData data)
//     {
//         if(!File.Exists(GetPath()))
//         {
//             Save(data);
//             return data;
//         }

//         BinaryFormatter formatter = new BinaryFormatter();
//         FileStream fileStream = new FileStream(GetPath(), FileMode.Open);
//         data = formatter.Deserialize(fileStream) as SaveData;
//         fileStream.Close();

//         return data;
//     }

//     private string GetPath()
//     {
//         return Application.persistentDataPath + "/data.qnd";
//     }
// }

public static class SaveSystem
{
    public static void SaveFactoryData(FactoryData data, string id)
    {
        PlayerPrefs.SetString(id, JsonUtility.ToJson(data));
    }

    public static FactoryData LoadFactoryData(FactoryData data, string id)
    {
        if (PlayerPrefs.HasKey(id))
        {
            data = JsonUtility.FromJson<FactoryData>(PlayerPrefs.GetString(id));
        }
        else
            Debug.LogWarning("Failed to load factory data.");

        return data;
    }

    public static void SaveCurrencyTier1(double amount, string id)
    {
        PlayerPrefs.SetString(id, JsonUtility.ToJson(amount));
    }

    public static double LoadCurrencyTier1(double amount, string id)
    {
        if (PlayerPrefs.HasKey(id))
        {
            amount = JsonUtility.FromJson<double>(PlayerPrefs.GetString(id));
        }
        else
            Debug.LogWarning("Failed to load factory data.");

        return amount;
    }

    public static void SaveTimerTick(DateTime time, string id)
    {
        PlayerPrefs.SetString(id, time.ToString());
    }

    public static DateTime LoadTimerTick(DateTime time, string id)
    {
        if (PlayerPrefs.HasKey(id))
        {
            string dateString = (PlayerPrefs.GetString(id));
            if (!DateTime.TryParse(dateString, out time))
            {
                Debug.LogWarning("Failed to parse datetime string when loadind TimerTicks");
                return time;
            }
        }
        else
            Debug.LogWarning("Failed to load factory data.");

        return time;
    }
}
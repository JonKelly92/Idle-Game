using UnityEngine;

public static class SaveSystem
{
    public static void Save(SaveData data, string id)
    {
        PlayerPrefs.SetString(id, JsonUtility.ToJson(data));
    }

    public static SaveData Load(SaveData data, string id)
    {
        if (PlayerPrefs.HasKey(id))
        {
            data = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(id));
        }
        else
            Debug.LogWarning("Failed to load factory data, key not found.");

        return data;
    }
}
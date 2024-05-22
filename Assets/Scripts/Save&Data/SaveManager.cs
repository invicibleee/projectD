using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    public static void Save<T>(string key, T saveData)
    {
        string JSONDataString = JsonUtility.ToJson(saveData, true);
        PlayerPrefs.SetString(key, JSONDataString);
    }

    public static T Load<T>(string key) where T : new()
    {
        if (PlayerPrefs.HasKey(key))
        {
            string loadedString = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<T>(loadedString);
        }
        else
            return new T();
    }
}

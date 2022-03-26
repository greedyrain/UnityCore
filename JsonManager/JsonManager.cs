using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public enum JsonType { JsonUtility, LitJson }

public class JsonManager
{
    private static JsonManager instance = new JsonManager();
    public static JsonManager Instance
    {
        get { return instance; }
    }
    private JsonManager()
    {

    }

    public void SaveData(object data, string fileName, JsonType type = JsonType.LitJson)
    {
        string path = Application.persistentDataPath + $"/{fileName}.json";
        string dataStr = "";
        switch (type)
        {
            case JsonType.JsonUtility:
                dataStr = JsonUtility.ToJson(data);
                break;
            case JsonType.LitJson:
                dataStr = JsonMapper.ToJson(data);
                break;
        }
        File.WriteAllText(path, dataStr);
    }

    public T LoadData<T>(string fileName, JsonType type = JsonType.LitJson) where T : new()
    {
        string path = Application.streamingAssetsPath + $"/{fileName}.json";
        if (!File.Exists(path))
            path = Application.persistentDataPath + $"/{fileName}.json";
        if (!File.Exists(path))
            return new T();
        string dataStr = File.ReadAllText(path);
        T data = default(T);
        switch (type)
        {
            case JsonType.JsonUtility:
                data = JsonUtility.FromJson<T>(dataStr);
                break;
            case JsonType.LitJson:
                data = JsonMapper.ToObject<T>(dataStr);
                break;
        }
        return data;
    }
}

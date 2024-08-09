using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class SaveLoadHelper : MonoBehaviour
{
    public static void SaveData<T>(T data) where T : SaveData
    {
        if (!Directory.Exists(data.GetDriectory()))
        {
            Directory.CreateDirectory(data.GetDriectory());
        }

        var json = JsonUtility.ToJson(data);
        File.WriteAllText(data.GetFullPath(), json);

        Debug.Log("Data is Saved : " + data.GetFullPath());
    }

    public static T LoadData<T>(string fileName, string dir) where T : SaveData
    {
        //var fullPath = dir;
        var fullPath = Application.persistentDataPath + "/" + dir + "/" + fileName + ".json";

        if (!File.Exists(fullPath))
        {
            Debug.LogError("There is no file : " + fullPath);
            return null;
        }

        var json = File.ReadAllText(fullPath);
        var data = JsonUtility.FromJson<T>(json);

        Debug.Log("Data is Loaded: " + fullPath);

        return data;
    }

    public static T LoadDataFromRes<T>(string fileName) where T : SaveData
    {
        var path = "Json/" + fileName;
        Debug.Log("Attempting to load resource from path: " + path);

        var asset = Resources.Load<TextAsset>(path);

        if (asset == null)
        {
            Debug.LogError("There is no Resource File: " + path);
            return null;
        }

        var json = asset.text;
        Debug.Log("Json content Loaded: " + json);

        var data = JsonUtility.FromJson<T>(json);
        
        Debug.Log("Data is Loaded from Resources/Json: " + path);

        return data;
    }

    public static void SavePlayerData<T>(T playData) where T : SaveData
    {
        if (!Directory.Exists(playData.GetDriectory()))
        {
            Directory.CreateDirectory(playData.GetDriectory());
        }

        var json = JsonUtility.ToJson(playData);
        File.WriteAllText(playData.GetFullPath(), json);
        
        Debug.Log("PlayerData is Saved: " + playData.GetFullPath());
    }
    
    public static T LoadPlayerData<T>(string dir) where T : SaveData
    {
        var json = File.ReadAllText(dir);
        var data = JsonUtility.FromJson<T>(json);

        Debug.Log("Player Data is Loaded");

        return data;
    }
}

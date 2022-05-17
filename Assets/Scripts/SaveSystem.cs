using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string directoryPath;
    private static bool initialized;
    public static void Init()
    {
        directoryPath = $"{Application.dataPath}/Saves/";

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        initialized = true;
    }
    public static void Save(object dataToSave, string fileName = "Save")
    {
        if (!initialized)
        {
            Init();
        }

        string jsonSave = JsonUtility.ToJson(dataToSave);

        if (File.Exists($"{directoryPath}{fileName}.json"))
        {
            File.Delete($"{directoryPath}{fileName}.json");
        }

        StreamWriter writer = new StreamWriter($"{directoryPath}{fileName}.json");
        writer.WriteLine(jsonSave);
        writer.Close();
    }
    public static void Load<T>(out T loadedData, string fileName = "Save")
    {
        if (!initialized)
        {
            Init();
        }

        StreamReader reader = new StreamReader($"{directoryPath}{fileName}.json");
        string jsonSave = reader.ReadLine();
        loadedData = JsonUtility.FromJson<T>(jsonSave);
    }
}

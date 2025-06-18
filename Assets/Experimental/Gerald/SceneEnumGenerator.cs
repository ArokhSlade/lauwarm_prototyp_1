#nullable disable

using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Collections.Generic;

public class SceneEnumGenerator : MonoBehaviour
{
    void ListSceneNames()
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; ++i)
        {
            Debug.Log($"Scene {i}: {SceneManager.GetSceneByBuildIndex(i).name}");
        }
    }

    void WriteEnumFile(string folderPath, List<string> sceneNames)
    {
        string fileName = "SceneNames.cs";
        string tab = "    ";

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        using (StreamWriter file = new StreamWriter(Path.Combine(folderPath, fileName)))
        {
            file.WriteLine("public enum SceneNames {");

            foreach (string sceneName in sceneNames)
            {
                if (sceneName != null && !sceneName.Equals(string.Empty))
                {
                    file.WriteLine(tab + sceneName + ",");
                }
            }

            file.WriteLine("}");
        }
    }

    void GenerateEnumFile()
    {
        List<String> sceneNames = new();

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; ++i)
        {
            sceneNames.Add(SceneManager.GetSceneByBuildIndex(i).name);
        }
        string folderPath = Environment.CurrentDirectory + "/Assets/Scripts/Generated/";
        Debug.Log($"cwd: {folderPath}");

        WriteEnumFile(folderPath, sceneNames);
    }


    void Start()
    {
        ListSceneNames();
        GenerateEnumFile();
    }
}

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorFileTools
{
    public static bool SaveTextOnAssets(string fileName, string text)
    {
        string filePath = Application.dataPath + "/" + fileName;

        try
        {
            Debug.LogWarning("[PM] Saving Editor File in: " + filePath);
            File.WriteAllText(filePath, text);
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Save Editor File Failed: " + e.ToString());
        }

        return false;
    }

    public static string LoadTextOnAssets(string fileName)
    {
        string filePath = Application.dataPath + "/" + fileName;

        try
        {
            return File.ReadAllText(filePath);
        }
        catch (Exception e)
        {
            Debug.LogWarning("LoadText Failed: " + e.ToString());
        }

        return null;
    }
}

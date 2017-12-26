using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileTools
{
    private const string sBackupFileExtension = ".bkp";

    public static bool SaveBytes(string fileName, byte [] bytes)
    {
        string filePath = Application.persistentDataPath + "/" + fileName;

        string backupFilePath = filePath + sBackupFileExtension;

        try
        {
            Debug.LogWarning("[PM] Saving local File in: " + filePath);

            File.WriteAllBytes(backupFilePath, bytes);
            byte[] newlyWrittenFileContent = File.ReadAllBytes(backupFilePath);

            if (IsSameData(newlyWrittenFileContent, bytes))
            {
                File.Copy(backupFilePath, filePath, true);
                return true;
            }
            else
            {
                Debug.LogWarning("Save Failed: Data Corruption");
                return false;
            }

        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Save failed: " + e.ToString());
        }

        return false;
    }

    public static bool SaveText(string fileName, string text, bool checkBackup = true)
    {
        string filePath = Application.persistentDataPath + "/" + fileName;

        string backupFilePath = filePath + sBackupFileExtension;

        try
        {
            Debug.LogWarning("[PM] Saving local File in: " + filePath);
            if (checkBackup)
            {
                File.WriteAllText(backupFilePath, text);

                string newlyWrittenFileContent = System.IO.File.ReadAllText(backupFilePath);

                if (newlyWrittenFileContent == text)
                {
                    File.Copy(backupFilePath, filePath, true);
                    return true;
                }
                else
                {
                    Debug.LogWarning("Save Failed: Data Corruption");
                    return false;
                }
            }
            else
            {
                File.WriteAllText(filePath, text);
                return true;
            }

        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Save failed: " + e.ToString());
        }

        return false;
    }

    public static string LoadText(string fileName)
    {
        string filePath = Application.persistentDataPath + "/" + fileName;

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

    public static byte[] LoadBytes(string fileName)
    {
        string filePath = Application.persistentDataPath + "/" + fileName;

        try
        {
            return File.ReadAllBytes(filePath);
        }
        catch (Exception e)
        {
            Debug.LogWarning("LoadBytes Failed: "+e.ToString());
        }

        return null;
    }

    public static bool DeleteFile(string fileName)
    {
        string filePath = Application.persistentDataPath + "/" + fileName;

        if (File.Exists(filePath))
        {

            File.Delete(filePath);
            return true;
        }
        return false;
    }

    public static bool DeleteBackupFile(string fileName)
    {
        string filePath = Application.persistentDataPath + "/" + fileName + sBackupFileExtension;

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return true;
        }
        return false;
    }


    public static bool IsSameData(byte[] data1, byte[] data2)
    {
        if (data1.Length != data2.Length)
        {
            return false;
        }

        for (int i = 0; i < data1.Length; i++)
        {
            if (data1[i] != data2[i])
            {
                return false;
            }
        }
        return true;
    }
}

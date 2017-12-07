using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public static class EditorUtils
{
    private const string c_saveFormat = "{0}/{1}";

    [MenuItem("Wand Options/Clear Save")]
    public static void DeleteDefaultSave()
    {
        File.Delete(string.Format(c_saveFormat, Application.persistentDataPath, GameSerializer.kDefaultSaveName));
    }
}

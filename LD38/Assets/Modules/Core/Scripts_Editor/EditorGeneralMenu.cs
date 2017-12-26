using UnityEngine;
using UnityEditor;

using System.Collections;
using System.IO;
using System;

public static class EditorGeneralMenu
{
    public const string kMenuName = "Tools";

    [MenuItem(kMenuName + "/Player Prefs/Clear Player Prefs")]
    private static void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem(kMenuName + "/Player Prefs/Clear Editor Prefs")]
    private static void DeleteAllEditorPrefs()
    {
        EditorPrefs.DeleteAll();
    }

    [MenuItem(kMenuName + "/Paths/Open Data Path")]
    private static void OpenDataPath()
    {
        EditorUtility.RevealInFinder(Application.dataPath);
    }

    [MenuItem(kMenuName + "/Paths/Open Persistent Data Path")]
    private static void OpenPersistentDataPath()
    {
        EditorUtility.RevealInFinder(Application.persistentDataPath);
    }

    [MenuItem(kMenuName + "/Paths/Open Temporary Cache Path")]
    private static void OpenTerporaryCachePath()
    {
        EditorUtility.RevealInFinder(Application.temporaryCachePath);
    }

    #region Extract Editor Folders
    [MenuItem(kMenuName + "/Extract Editor Folders")]
    private static void ExtractEditorFolders()
    {
        string path = GetSelectionFolder();

        if (path != null)
        {
            path = Path.GetFullPath(path);
            Debug.LogWarning("Extracting file from: "+path);
            SearchEditorFiles(path, path);
            Debug.LogWarning("Done!");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    [MenuItem(kMenuName + "/Extract Editor Folders",true)]
    private static bool ExtractEditorFoldersValidate()
    {
        return GetSelectionFolder() != null;
    }

    private static void SearchEditorFiles(string dirName, string root,bool insideEditorFolder = false)
    {
        if (!insideEditorFolder && (dirName.Contains("Editor\\") || dirName.Contains("Editor/") || dirName.EndsWith("Editor")))
        {
            insideEditorFolder = true;
        }

        foreach (string file in Directory.GetFiles(dirName))
        {
            if(insideEditorFolder)
            {
                MoveEditorFile(file);
            }
        }

        foreach (string dir in Directory.GetDirectories(dirName))
        {
            SearchEditorFiles(dir, root, insideEditorFolder);
        }
    }

    private static void MoveEditorFile(string originalPath)
    {
        string newPath = GetEditorPath(originalPath);

        // Create directory
        var fileInfo = new System.IO.FileInfo(newPath);

        fileInfo.Directory.Create();

        // Move file
        File.Move(originalPath, newPath);

    }

    private static string GetEditorPath(string originalPath)
    {
        string newPath = originalPath.Replace("\\Editor\\", "\\");
        newPath = newPath.Replace("/Editor/", "\\");
        newPath = newPath.Replace("\\Editor/", "\\");
        newPath = newPath.Replace("/Editor\\", "\\");

        newPath = newPath.Replace("\\Scripts\\", "\\Scripts_Editor\\");
        newPath = newPath.Replace("/Scripts/", "\\Scripts_Editor\\");
        newPath = newPath.Replace("\\Scripts/", "\\Scripts_Editor\\");
        newPath = newPath.Replace("/Scripts\\", "\\Scripts_Editor\\");

        return newPath;
    }

    private static string GetSelectionFolder()
    {
        var obj = Selection.activeObject;
        if (obj == null)
        {
            return null;
        }

        var path = AssetDatabase.GetAssetPath(obj.GetInstanceID());

        if (path.Length > 0)
        {
            if (Directory.Exists(path))
            {
                return path;
            }
        }
        return null;
    }
    #endregion
}

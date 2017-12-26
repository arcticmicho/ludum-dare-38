using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class BuildTools
{
    public static void AddDefineSymbol(string defineName)
    {
        string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
        List<string> allDefines = definesString.Split(';').ToList();
        if (!allDefines.Contains(defineName))
        {
            allDefines.Add(defineName);
            Debug.Log(defineName + " define symbol has been added to the player settings!");
        }
        string defines = string.Join(";", allDefines.ToArray());
        PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, defines);
    }
}

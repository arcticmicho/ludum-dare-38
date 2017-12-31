using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorScriptableObjectTools
{
    public static T AddOrGetAsset<T>(string assetPath) where T : ScriptableObject
    {
        T asset = AssetDatabase.LoadAssetAtPath<T>("Assets/"+assetPath);

        if(asset != null)
        {
            return asset;
        }

        asset = ScriptableObject.CreateInstance<T>();
        AssetDatabase.CreateAsset(asset, "Assets/" + assetPath);
        return asset;
    }


    public static T[] GetAssetsOfType<T>() where T : ScriptableObject
    {
        var foundAssets = AssetDatabase.FindAssets("t:" + typeof(T).Name);

        List<T> assetList = new List<T>();

        foreach (string path in foundAssets)
        {
            var lockeyData = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(path));
            if (lockeyData != null)
            {
                assetList.Add(lockeyData);
            }
        }
        return assetList.ToArray();
    }
}

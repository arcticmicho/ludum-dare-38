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
}

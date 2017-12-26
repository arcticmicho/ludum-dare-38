using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;

public class EditorTools
{
    public static void PlayClip(AudioClip clip)
    {
        if (clip != null)
        {
            StopAllClips();

            Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
            System.Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
            MethodInfo method = audioUtilClass.GetMethod(
                "PlayClip",
                BindingFlags.Static | BindingFlags.Public,
                null,
                new System.Type[] {
            typeof(AudioClip)
                },
                null
            );
            method.Invoke(
                null,
                new object[] {
            clip
                }
            );
        }
    }

    public static void StopAllClips()
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod(
            "StopAllClips",
            BindingFlags.Static | BindingFlags.Public,
            null,
            new System.Type[] { },
            null
        );
        method.Invoke(
            null,
            new object[] { }
        );
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public static class EditorSceneAutoLoad
{
    const string _kPlayFromFirstMenuStr = "Tools/AutoLoad Intro Scene";

    static bool playFromFirstScene
    {
        get { return EditorPrefs.HasKey(_kPlayFromFirstMenuStr) && EditorPrefs.GetBool(_kPlayFromFirstMenuStr); }
        set { EditorPrefs.SetBool(_kPlayFromFirstMenuStr, value); }
    }

    [MenuItem(_kPlayFromFirstMenuStr, false, 150)]
    static void PlayFromFirstSceneCheckMenu()
    {
        playFromFirstScene = !playFromFirstScene;
        Menu.SetChecked(_kPlayFromFirstMenuStr, playFromFirstScene);

        ShowNotifyOrLog(playFromFirstScene ? "Play from scene Intro" : "Play from current scene");
    }

    // The menu won't be gray out, we use this validate method for update check state
    [MenuItem(_kPlayFromFirstMenuStr, true)]
    static bool PlayFromFirstSceneCheckMenuValidate()
    {
        Menu.SetChecked(_kPlayFromFirstMenuStr, playFromFirstScene);
        return true;
    }

    // This method is called before any Awake. It's the perfect callback for this feature
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void LoadFirstSceneAtGameBegins()
    {
        if (!playFromFirstScene)
            return;

        if (EditorBuildSettings.scenes.Length == 0)
        {
            Debug.LogWarning("The scene build list is empty. Can't play from first scene.");
            return;
        }

        foreach (GameObject go in Object.FindObjectsOfType<GameObject>())
            go.SetActive(false);

        SceneManager.LoadScene(0);
    }

    static void ShowNotifyOrLog(string msg)
    {
        if (Resources.FindObjectsOfTypeAll<SceneView>().Length > 0)
            EditorWindow.GetWindow<SceneView>().ShowNotification(new GUIContent(msg));
        else
            Debug.Log(msg); // When there's no scene view opened, we just print a log
    }
}
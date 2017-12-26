using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class EditorPrefabTools
{
    public static void SaveObjectToPrefab(GameObject prefabOject)
    {
        Object prefab = PrefabUtility.GetPrefabParent(prefabOject);

        if (prefab != null)
        {
            PrefabUtility.ReplacePrefab(prefabOject, prefab, ReplacePrefabOptions.ConnectToPrefab);
        }
        else
        {
            Debug.LogError("This object does not have prefab parent", prefabOject);
        }
    }

    public static bool BelongToSamePrefab(GameObject A,GameObject B)
    {
        Object prefabA = PrefabUtility.GetPrefabParent(A);
        Object prefabB = PrefabUtility.GetPrefabParent(B);

        if (prefabA != null && prefabB != null && prefabA == prefabB)
        {
            return true;
        }
        return false;

    }
}

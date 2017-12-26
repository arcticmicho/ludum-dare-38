using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensionMethods
{
    public static GameObject AddOrGetChild(this GameObject go, string name)
    {
        var trans = go.transform.Find(name);
        if (trans == null)
        {
            var meshGO = new GameObject(name);
            meshGO.transform.SetParent(go.transform, false);
            return meshGO;
        }

        return trans.gameObject;
    }

    public static GameObject AddChild(this GameObject go, string name)
    {
        var newGo = new GameObject(name);
        newGo.transform.SetParent(go.transform, false);
        return newGo;
    }

    public static T AddChild<T>(this GameObject go, string name) where T : Component
    {
        var newGo = new GameObject(name);
        newGo.transform.SetParent(go.transform, false);
        return newGo.AddComponent<T>();
    }

    public static T AddOrGetComponent<T>(this GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
        {
            component = go.AddComponent<T>();

            return component;
        }

        return component;
    }

    public static T GetUniqueComponent<T>(this GameObject go) where T : Component
    {
        T[] objs = go.GetComponentsInChildren<T>(true);
        if (objs.Length == 1)
        {
            return objs[0];
        }
        else if(objs.Length > 1)
        {
            Debug.LogError("GetUniqueComponent found more than one T object.");
            return objs[0];
        }
        return default(T);
    }

    public static void Reset(this GameObject go)
    {
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
        go.transform.localRotation = Quaternion.identity;
    }

    public static void Reset(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
    }

    public static void Clear(this GameObject go)
    {
        foreach (Transform child in go.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public static void Clear(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public static void ClearImmediate(this GameObject go)
    {
        for (int i = go.transform.childCount - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(go.transform.GetChild(i).gameObject);
        }
    }

    public static void ClearImmediate(this Transform transform)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    public static void SetLayer(this Transform root, int layer)
    {
        Stack<Transform> children = new Stack<Transform>();
        children.Push(root);
        while (children.Count > 0)
        {
            Transform currentTransform = children.Pop();
            currentTransform.gameObject.layer = layer;
            foreach (Transform child in currentTransform)
            {
                children.Push(child);
            }
        }
    }

    public static void SetLayer(this GameObject root, int layer)
    {
        Stack<Transform> children = new Stack<Transform>();
        children.Push(root.transform);
        while (children.Count > 0)
        {
            Transform currentTransform = children.Pop();
            currentTransform.gameObject.layer = layer;
            foreach (Transform child in currentTransform)
            {
                children.Push(child);
            }
        }
    }
}

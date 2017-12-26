using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class is deprected Will be Removed later.
/// </summary>
public partial class EditorGUITools
{
    public static T ObjectField<T>(Object recordObject, T obj, params GUILayoutOption[] options) where T: Object
    {
        T newObj = EditorGUILayout.ObjectField(obj, typeof(T), false, options) as T;
        if (GUI.changed && newObj != obj)
        {
            Undo.RecordObject(recordObject, "Text Field Value Changed");
            EditorUtility.SetDirty(recordObject);
            return newObj;
        }

        return obj;
    }

    public static float FloatFieldSlider(Object recordObject, float value,float min,float max, params GUILayoutOption[] options)
    {
        float newValue = EditorGUILayout.Slider(value, min, max, options);
        if (GUI.changed && newValue != value)
        {
            Undo.RecordObject(recordObject, "Float Field Slider Value Changed");
            EditorUtility.SetDirty(recordObject);
            return newValue;
        }

        return value;
    }
}

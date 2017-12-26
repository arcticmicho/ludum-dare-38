using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,SerializedProperty property,GUIContent label)
    {
        Color old = GUI.color;
        GUI.color = new Color(0.4f,1.0f,0.8f);
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;

        GUI.color = old;
    }
}


using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

[CustomPropertyDrawer(typeof(ButtonBoolAttribute))]
public class ButtonBoolDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.Boolean)
        {
            bool value = property.boolValue;

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            Rect toggleRect = new Rect(position.x, position.y, position.width, position.height);

            EditorGUI.BeginChangeCheck();
            Color defaultColor = GUI.color;
            string status = "";
            if (value)
            {
                GUI.color = new Color(0.1f,1.0f,0.4f);
                status = "Enabled";
            }
            else
            {
                GUI.color = new Color(1.0f, 0.4f, 0.1f);
                status = "Disabled";
            }

            GUI.Toggle(toggleRect, false, status, "Button");
            GUI.color = defaultColor;
            if (EditorGUI.EndChangeCheck())
            {
                property.boolValue = !value;
            }
        }
        else
        {
            GUI.Label(new Rect(position.x, position.y, position.width, position.height), "<You can only use ButtonBool on booleans>");
        }
    }
}


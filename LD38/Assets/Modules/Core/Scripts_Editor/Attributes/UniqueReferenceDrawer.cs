using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

[CustomPropertyDrawer(typeof(UniqueReferenceAttribute))]
public class UniqueReferenceAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.ObjectReference && property.serializedObject.targetObject is MonoBehaviour)
        {
 
            Rect field = new Rect(position.x, position.y, position.width-16, position.height);
            Rect status = new Rect(position.x + position.width - 16, position.y, 16, position.height);

            object value = fieldInfo.GetValue(property.serializedObject.targetObject);

            Color old = GUI.color;

            if(value == null)
            {
                GUI.color = Color.red;
            }
            else
            {
                GUI.color = Color.green;
            }

            GUI.Box(status, GUIContent.none);

            GUI.enabled = false;
            EditorGUI.PropertyField(field, property, label, true);
  
            GUI.enabled = true;
            GUI.color = old;

            if (property.serializedObject.targetObject is MonoBehaviour)
            {
                MonoBehaviour obj = property.serializedObject.targetObject as MonoBehaviour;

                Component [] components = obj.GetComponentsInChildren(fieldInfo.FieldType);

                if(components != null && components.Length == 1)
                {
                    fieldInfo.SetValue(property.serializedObject.targetObject, components[0]);
                }
                else
                {
                    fieldInfo.SetValue(property.serializedObject.targetObject, null);
                }

            }
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "You can only use Unique Reference on Objects");
        }
    }
}


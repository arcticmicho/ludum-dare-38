using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class EditorBackToPreviousSelection
{
    // Editable Parameters
    private const string sBackToPreviousSelectionMenu = "Tools/Back To Previous Selection %#z";
    private const int sMaxCount = 300;

    // System Parameters
    private static List<Object> _objectPool = new List<Object>();
    private static Object _lastAddedObject = null;
    private static Object _lastSelectedObject = null;

    static EditorBackToPreviousSelection()
    {
        Selection.selectionChanged += OnSelectionChange;
    }

    [MenuItem(sBackToPreviousSelectionMenu)]
    private static void BackToPreviousSelectionMenu()
    {
        if (_objectPool.Count > 0)
        {
            Object selectedObject = _objectPool[_objectPool.Count-1];
            _objectPool.RemoveAt(_objectPool.Count - 1);

            _lastAddedObject = selectedObject;
            Selection.activeObject = selectedObject;

            if (selectedObject != null)
            {
                EditorWindow.GetWindow<SceneView>().ShowNotification(new GUIContent(Selection.activeObject.name));
            }
        }
    }

    [MenuItem(sBackToPreviousSelectionMenu,true)]
    private static bool BackToPreviousSelectionMenuValidate()
    {
        return _objectPool.Count > 0;
    }

    ~EditorBackToPreviousSelection()
    {
        Selection.selectionChanged -= OnSelectionChange;
    }
    
    private static void OnSelectionChange()
    {
        if(Selection.activeObject != _lastAddedObject)
        {
            _objectPool.Add(_lastSelectedObject);
            if(_objectPool.Count > sMaxCount)
            {
                _objectPool.RemoveAt(0);
            }
        }

        _lastSelectedObject = Selection.activeObject;
    }    
}
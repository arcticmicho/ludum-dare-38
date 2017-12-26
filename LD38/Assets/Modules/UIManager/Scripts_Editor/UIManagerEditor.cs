using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace GameModules
{
    [CustomEditor(typeof(UIManager))]
    public class GUIManagerEditor : Editor
    {
        void OnEnable()
        {

        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }

}
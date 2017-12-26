using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace GameModules
{
    [CustomEditor(typeof(UIPanel))]
    public class UIPanelEditor : Editor
    {
        public void OnEnable()
        {

        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}
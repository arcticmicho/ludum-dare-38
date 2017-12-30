﻿
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

// Skill List
public partial class GoogleDataMapperEditor : Editor
{
    private const string kWizardSkillAssignRange = "Export_WizardSkillAssign";

    private void LoadWizardSkillAssign(string range)
    {
        var googleSheet = new GoogleSheet(_target.DataSpreadSheet, _target.GoogleApiKey);

        GoogleDataManager.RequestData(googleSheet, range, GoogleDataDirection.ROWS, OnGetWizardSkillAssignData, _target.ShowResponse);
    }

    private void OnGetWizardSkillAssignData(bool success, string range, Dictionary<string, object> data)
    {
        if (success)
        {
            var tableElements = GetTableElements(data);

            for (int a = 0; a < tableElements.Count; ++a)
            {
                ProcessSkillAssignTableElement(tableElements[a]);
            }
        }
    }


    private void ProcessSkillAssignTableElement(Dictionary<string,object> elementData)
    {
        var wizardId = SerializeHelper.GetValue<string>(elementData, "WizardId");
        var skillId  = SerializeHelper.GetValue<string>(elementData, "SkillId");
        var level    = SerializeHelper.GetValue<string>(elementData, "Level");

        var wizard = EditorScriptableObjectTools.AddOrGetAsset<WizardDataTemplate>(kWizardPath + wizardId + ".asset");
        var skill  = EditorScriptableObjectTools.AddOrGetAsset<WizardDataTemplate>(kSkillPath + skillId + ".asset");

        wizard.Editor_SetExportData(elementData);
        EditorUtility.SetDirty(wizard);
    }

}

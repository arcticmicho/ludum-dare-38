
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

// Skill List
public partial class GoogleDataMapperEditor : Editor
{
    private const string kSkillPath      = "06_Resources/Skills/";
    private const string kSkillListRange = "Export_SkillList";

    private void LoadSkillList(string range)
    {
        var googleSheet = new GoogleSheet(_target.DataSpreadSheet, _target.GoogleApiKey);

        GoogleDataManager.RequestData(googleSheet, range, GoogleDataDirection.ROWS, OnGetSkillListData, _target.ShowResponse);
    }

    private void OnGetSkillListData(bool success, string range, Dictionary<string, object> data)
    {
        if (success)
        {
            var tableElements = GetTableElements(data);

            for (int a = 0; a < tableElements.Count; ++a)
            {
                ProcessSkillTableElement(tableElements[a]);
            }
        }
    }

    private void ProcessSkillTableElement(Dictionary<string,object> elementData)
    {
        var id = SerializeHelper.GetValue<string>(elementData, "Id");
        var skill = EditorScriptableObjectTools.AddOrGetAsset<SkillDefinition>(kSkillPath + id + ".asset");

        skill.Editor_SetExportData(elementData);
        EditorUtility.SetDirty(skill);
    }
}


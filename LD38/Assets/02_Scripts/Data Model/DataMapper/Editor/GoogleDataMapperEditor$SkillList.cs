
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

// Skill List
public partial class GoogleDataMapperEditor : Editor
{
    private static string sSkillPath      = "06_Resources/Skills/";
    private static string sSkillListRange = "Export_SpellList";

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

    private void ProcessSkillTableElement(Dictionary<string,object> skillTableElementData)
    {
        var skillName = SerializeHelper.GetValue<string>(skillTableElementData, "Name");
        var skill = EditorScriptableObjectTools.AddOrGetAsset<SkillDefinition>(skillName);

        skill.Editor_SetExportData(skillTableElementData);
        EditorUtility.SetDirty(skill);
    }
}


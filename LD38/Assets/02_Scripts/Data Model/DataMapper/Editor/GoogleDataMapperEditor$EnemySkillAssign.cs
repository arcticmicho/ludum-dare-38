
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

// Skill List
public partial class GoogleDataMapperEditor : Editor
{
    private const string kEnemySkillAssignRange = "Export_EnemySkillAssign";

    private void LoadEnemySkillAssign(string range)
    {
        var googleSheet = new GoogleSheet(_target.DataSpreadSheet, _target.GoogleApiKey);

        GoogleDataManager.RequestData(googleSheet, range, GoogleDataDirection.ROWS, OnGetEnemySkillAssignData, _target.ShowResponse);
    }

    private void OnGetEnemySkillAssignData(bool success, string range, Dictionary<string, object> data)
    {
        if (success)
        {
            var tableElements = GetTableElements(data);

            for (int a = 0; a < tableElements.Count; ++a)
            {
                ProcessEnemyAssignTableElement(tableElements[a]);
            }
        }
    }


    private void ProcessEnemyAssignTableElement(Dictionary<string,object> elementData)
    {
        var enemyId  = SerializeHelper.GetValue<string>(elementData, "EnemyId");
        var skillId  = SerializeHelper.GetValue<string>(elementData, "SkillId");
        var level    = SerializeHelper.GetValue<int>(elementData, "Level");

        if(string.IsNullOrEmpty(enemyId) || string.IsNullOrEmpty(skillId))
        {
            return;
        }

        var enemy  = EditorScriptableObjectTools.AddOrGetAsset<CharacterTemplate>(kEnemyPath + enemyId + ".asset");
        var skill  = EditorScriptableObjectTools.AddOrGetAsset<SkillDefinition>(kSkillPath + skillId + ".asset");

        enemy.Editor_AddSkill(skill, level);
        EditorUtility.SetDirty(enemy);
    }

}


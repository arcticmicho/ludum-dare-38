
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

// Skill List
public partial class GoogleDataMapperEditor : Editor
{
    private const string kEnemyPath      = "06_Resources/CharactersTemplate/";
    private const string kEnemyListRange = "Export_EnemyList";

    private void LoadEnemyList(string range)
    {
        var googleSheet = new GoogleSheet(_target.DataSpreadSheet, _target.GoogleApiKey);

        GoogleDataManager.RequestData(googleSheet, range, GoogleDataDirection.ROWS, OnGetEnemyListData, _target.ShowResponse);
    }

    private void OnGetEnemyListData(bool success, string range, Dictionary<string, object> data)
    {
        if (success)
        {
            var tableElements = GetTableElements(data);

            for (int a = 0; a < tableElements.Count; ++a)
            {
                ProcessEnemyTableElement(tableElements[a]);
            }
        }
    }


    private void ProcessEnemyTableElement(Dictionary<string,object> elementData)
    {
        var id = SerializeHelper.GetValue<string>(elementData, "Id");
        var character = EditorScriptableObjectTools.AddOrGetAsset<CharacterTemplate>(kEnemyPath + id + ".asset");

        character.Editor_SetExportData(elementData);
        EditorUtility.SetDirty(character);
    }

}


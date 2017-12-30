
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

// Skill List
public partial class GoogleDataMapperEditor : Editor
{
    private const string kWizardPath      = "06_Resources/CharactersTemplate/";
    private const string kWizardListRange = "Export_WizardList";

    private void LoadWizardList(string range)
    {
        var googleSheet = new GoogleSheet(_target.DataSpreadSheet, _target.GoogleApiKey);

        GoogleDataManager.RequestData(googleSheet, range, GoogleDataDirection.ROWS, OnGetWizardListData, _target.ShowResponse);
    }

    private void OnGetWizardListData(bool success, string range, Dictionary<string, object> data)
    {
        if (success)
        {
            var tableElements = GetTableElements(data);

            for (int a = 0; a < tableElements.Count; ++a)
            {
                ProcessWizardTableElement(tableElements[a]);
            }
        }
    }


    private void ProcessWizardTableElement(Dictionary<string,object> elementData)
    {
        var id = SerializeHelper.GetValue<string>(elementData, "Id");
        var character = EditorScriptableObjectTools.AddOrGetAsset<WizardDataTemplate>(kWizardPath + id + ".asset");

        character.Editor_SetExportData(elementData);
        EditorUtility.SetDirty(character);
    }

}


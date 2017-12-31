
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

[CustomEditor(typeof(GoogleDataMapper))]
public partial class GoogleDataMapperEditor : Editor
{
    private const string kNotExport = "NOT_EXPORT";
    private const string kDamageTypePath = "06_Resources/DamageTypes/";

    private GoogleDataMapper _target;

    void OnEnable()
    {
        _target = target as GoogleDataMapper;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Load Skill List"))
        {
            EditorUtility.DisplayProgressBar("Loading Data From Google", "Loading " + kSkillListRange, 0.5f);
            LoadSkillList(kSkillListRange);
        }

        if (GUILayout.Button("Load Wizard List"))
        {
            EditorUtility.DisplayProgressBar("Loading Data From Google", "Loading " + kWizardListRange, 0.5f);
            LoadWizardList(kWizardListRange);
        }

        if (GUILayout.Button("Load Enemy List"))
        {
            EditorUtility.DisplayProgressBar("Loading Data From Google", "Loading " + kEnemyListRange, 0.5f);
            LoadEnemyList(kEnemyListRange);
        }


        if (GUILayout.Button("Load Wizard Skill Assign"))
        {
            EditorUtility.DisplayProgressBar("Loading Data From Google", "Loading " + kWizardSkillAssignRange, 0.5f);
            LoadWizardSkillAssign(kWizardSkillAssignRange);
        }


        if (GUILayout.Button("Load Enemy Skill Assign"))
        {
            EditorUtility.DisplayProgressBar("Loading Data From Google", "Loading " + kEnemySkillAssignRange, 0.5f);
            LoadEnemySkillAssign(kEnemySkillAssignRange);
        }


  
        GUILayout.Space(10);

        if (GUILayout.Button("Load All"))
        {
            TrackProgress("Loading Data From Google", "Loading Sheets",
            () => { LoadSkillList(kSkillListRange);},
            () => { LoadEnemyList(kEnemyListRange);},
            () => { LoadWizardList(kWizardListRange);},
            () => { LoadWizardSkillAssign(kWizardSkillAssignRange);},
            () => { LoadEnemySkillAssign(kEnemySkillAssignRange);});
        }
    
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        base.OnInspectorGUI();
    }


    private void TrackProgress(string title,string text, params Action[] actions)
    {
        try
        {
            int loadCount = actions.Length;
            int loadCurrent = 0;

            for (int a = 0; a < actions.Length; ++a)
            {
                EditorUtility.DisplayProgressBar(title, text, loadCurrent / (float)loadCount);
                loadCurrent++;
                actions[a].SafeInvoke();
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error during load: " + e.Message);
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }
    }

    private List<Dictionary<string,object>> GetTableElements(Dictionary<string, object> data)
    {
        List<object> values = data["values"] as List<object>;

        List<Dictionary<string, object>> valueDictionaries = new List<Dictionary<string, object>>();

        List<string> keys = new List<string>();

        if (values.Count > 0)
        {
            var row = values[0] as List<object>;
            foreach (var val in row)
            {
                keys.Add(SerializeHelper.Interpret<string>(val));
            }

            for (int a = 1; a < values.Count; ++a)
            {
                var chunkValues = values[a] as List<object>;
                Dictionary<string, object> valueDictionarie = new Dictionary<string, object>();

                for (int b = 0; b < chunkValues.Count; ++b)
                {
                    if (keys[b] != kNotExport)
                    {
                        if (!valueDictionarie.ContainsKey(keys[b]))
                        {
                            valueDictionarie.Add(keys[b], chunkValues[b]);
                        }
                        else
                        {
                            Debug.LogError("Repeated Key: " + keys[b]);
                        }
                    }
                }

                valueDictionaries.Add(valueDictionarie);
            }
        }

        return valueDictionaries;
    }

    private void SetCharacterResistences(CharacterTemplate character, Dictionary<string, object> elementData)
    {
        // Get all damage type
        var damageTypeList = EditorScriptableObjectTools.GetAssetsOfType<DamageType>();

        Dictionary<string, DamageType> damageTypeDatabase = new Dictionary<string, DamageType>();

        foreach (var damageType in damageTypeList)
        {
            damageTypeDatabase.Add(damageType.Id, damageType);
        }

        string[] modList = new string[]
        {
            "NeutralMod",
            "FireMod",
            "ColdMod",
            "DarkMod",
            "HolyMod",
            "ArcanaMod",
            "LightningMod",
            "PoisonMod"
        };

        string[] modDamageType = new string[]
        {
            "Neutral",
            "Fire",
            "Cold",
            "Dark",
            "Holy",
            "Arcana",
            "Lightning",
            "Poison"
        };

        for (int a = 0; a < modList.Length; ++a)
        {
            var modName = modList[a];
            var damageId = modDamageType[a];

            var modValue = SerializeHelper.GetValue<float>(elementData, modName, 0);

            DamageType damageType;

            if (damageTypeDatabase.TryGetValue(damageId, out damageType))
            {

                character.Editor_AddResistance(damageType, modValue);
            }
        }
    }
}


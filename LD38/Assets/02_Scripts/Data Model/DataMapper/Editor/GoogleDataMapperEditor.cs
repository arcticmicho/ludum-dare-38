
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

[CustomEditor(typeof(GoogleDataMapper))]
public partial class GoogleDataMapperEditor : Editor
{
    private GoogleDataMapper _target;
    private const string kNotExport = "NOT_EXPORT";

    void OnEnable()
    {
        _target = target as GoogleDataMapper;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Clear Volatile Data"))
        {
            ClearVolatileData();
        }

        try
        {
            if (GUILayout.Button("Load All"))
            {
                float total = 0.95f / 4.0f;

                EditorUtility.DisplayProgressBar("Loading Data From Google", "Clearing volatile Data", 0.0f);
                ClearVolatileData();
                EditorUtility.DisplayProgressBar("Loading Data From Google", "Loading " + kSkillListRange, 0.05f);
                LoadSkillList(kSkillListRange);

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
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        base.OnInspectorGUI();
    }

    private void ClearVolatileData()
    {
     /*   var levelDirector = _target.LevelDirector;
        levelDirector.EditorClearVolatileData();
        EditorUtility.SetDirty(levelDirector);
        AssetDatabase.SaveAssets();*/
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

  /*  private void CreateChunks<T>(List<Dictionary<string,object>> values) where T : LevelChunkGenerator
    {
        var levelDirector = _target.LevelDirector;

        for (int a = 0; a < values.Count; ++a)
        {
            var chunkValues = values[a];

            string name       = SerializeHelper.GetValue<string>(chunkValues, "Name");
            string difficulty = SerializeHelper.GetValue<string>(chunkValues, "Difficulty");
            string setName    = SerializeHelper.GetValue<string>(chunkValues, "GroupName");
            int quantity      = SerializeHelper.GetValue<int>   (chunkValues, "Quantity");

            if(string.IsNullOrEmpty(name))
            {
                Debug.LogError("Invalid Name: "+name);
                continue;
            }

            var chunkGenerator = ScriptableObjectTools.AddOrGetAsset<T>(sChunkPath + difficulty + "/" + name + ".asset");
            if (chunkGenerator != null)
            {
                chunkGenerator.EditorSetupData(name, _target.ObjectSet);
                chunkGenerator.CreateFromData(chunkValues);
                EditorUtility.SetDirty(chunkGenerator);
            }

            var chunkSet = levelDirector.GetChunkSetByName(difficulty);

            if (chunkSet != null)
            {
                var set = chunkSet.GetChunkSetDefinition(setName);
                if (set == null)
                {
                    set = new LevelChunkSetDefinition(setName);
                    chunkSet.LevelChunks.Add(set);
                }

                set.LevelChunks.Add(new LevelChunkDefinition(quantity, chunkGenerator));
            }

            chunkSet.EditorSort();
            chunkSet.EditorSave();
        }

        // Save Changes
        EditorUtility.SetDirty(levelDirector);
        AssetDatabase.SaveAssets();
    }*/
}


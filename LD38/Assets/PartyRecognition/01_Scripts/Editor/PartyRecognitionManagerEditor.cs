using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Text;

[CustomEditor(typeof(PartyRecognitionManager))]
public class PartyRecognitionManagerEditor : Editor
{
    PartyRecognitionManager m_instance;

    public override void OnInspectorGUI()
    {
        if (m_instance == null)
            m_instance = target as PartyRecognitionManager;
        base.OnInspectorGUI();

        if(GUILayout.Button("Write Patterns"))
        {
            ReadSprites();
        }
        if(GUILayout.Button("Write Current Patterns To JSON File"))
        {
            WriteCurrentPatternsToJsonFile();
        }
    }

    private void ReadSprites()
    {
        StreamWriter stream = File.CreateText(m_instance.PathToFile);
        List<Dictionary<string, object>> definitions = new List<Dictionary<string, object>>();
        for (int i=0; i<m_instance.PatternTextures.Length; i++)
        {
            PRPatternDefinition def = GeneratePatternFromSprite(m_instance.PatternTextures[i]);
            definitions.Add(def.Serialize());
        }
        stream.WriteLine(MiniJSON.Json.Serialize(definitions));
        stream.Close();
    }

    private void WriteCurrentPatternsToJsonFile()
    {
        StreamWriter stream = File.CreateText(m_instance.PathToFile);
        List<Dictionary<string, object>> definitions = new List<Dictionary<string, object>>();
        foreach(PRPatternDefinition pattern in m_instance.PatternDefinitionSet)
        {
            definitions.Add(pattern.Serialize());
        }
        stream.WriteLine(MiniJSON.Json.Serialize(definitions));
        stream.Close();
    }

    private PRPatternDefinition GeneratePatternFromSprite(Texture2D tex)
    {
        float halfWidth = tex.width / 2f;
        float offSetWidth = 4f;
        float halftHeight = tex.height / 2f;
        float offSetHeight = 4f;
        List<Vector2> points = new List<Vector2>();
        for(int i = 0; i<tex.width; i++)
        {
            for(int n=0; n<tex.height; n++)
            {
                Color pixelColor = tex.GetPixel(i, n);
                float colorValue = (pixelColor.r + pixelColor.g + pixelColor.b) / 3f;
                if(colorValue <= 0.5f)
                {
                    points.Add(new Vector2((i - halfWidth) * offSetWidth, (n - halftHeight) * offSetHeight));
                }
            }
        }
        Vector2[] arrayPoints = m_instance.NormalizePoints(points.ToArray());
        PRPatternDefinition def = new PRPatternDefinition(arrayPoints, tex.name);
        return def;
    }
}

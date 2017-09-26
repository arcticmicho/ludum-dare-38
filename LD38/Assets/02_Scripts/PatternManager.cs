﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoSingleton<PatternManager>
{
    	
    public bool ProcessSkillPattern(Vector2[] points, SkillPattern pattern, out RecognitionResult result)
    {
        PRPatternDefinition prPattern;
        if (PartyRecognitionManager.Instance.TryGetPatternById(pattern.PatternRecognitionId, out prPattern))
        {
            result = PartyRecognitionManager.Instance.SimpleRecognize(points, prPattern);
            return result.Success;
        }
        result = null;
        return false;
    }

    public bool ProcessSkillPattern(List<Vector3> points, SkillPattern pattern, out RecognitionResult result)
    {
        return ProcessSkillPattern(ConvertList(points), pattern, out result);
    }


    private Vector2[] ConvertList(List<Vector3> points)
    {
        Vector2[] pointsV2 = new Vector2[points.Count];
        for (int i = 0; i < points.Count; i++)
        {
            pointsV2[i] = points[i];
        }
        return pointsV2;
    }
}
using GestureRecognizer;
using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoSingleton<PatternManager>
{
    [SerializeField]
    private Recognizer m_recognizer;

    	
    public bool ProcessSkillPattern(Vector2[] points, SkillPattern pattern, out PartyRecognition.RecognitionResult result)
    {
        float score;
        bool propagateResult = PartyRecognitionManager.Instance.Recognize(points, pattern.PatternRecognitionId, pattern.PatternThreshold, out score);
        result = new PartyRecognition.RecognitionResult(propagateResult, score, pattern.PatternRecognitionId);
        return propagateResult;
    }

    public bool ProcessSkillPattern(List<Vector3> points, SkillPattern pattern, out PartyRecognition.RecognitionResult result)
    {
        return ProcessSkillPattern(ConvertList(points), pattern, out result);
    }

    public bool ProcessSkillPattern(Vector2[] points, SkillPattern pattern, out RecognitionResult result)
    {
        result = ProcessPattern(points);
        return result.gesture == pattern.Pattern && result.score.score >= pattern.PatternThreshold;
    }

    public RecognitionResult ProcessPattern(Vector2[] points)
    {
        GestureData data = new GestureData();
        GestureLine line = new GestureLine();
        line.points = new List<Vector2>(points);
        data.lines.Add(line);

        return m_recognizer.Recognize(data);
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

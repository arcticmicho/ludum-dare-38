using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class PRPatternDefinition
{
    [SerializeField]
    private Vector2[] m_cloudPoints;
    public Vector2[] CloudPoints
    {
        get { return m_cloudPoints; }
    }

    /// <summary>
    /// Only for pre-defined patterns;
    /// </summary>
    [SerializeField]
    private string m_patternName;
    public string PatternName
    {
        get { return m_patternName; }
    }

    public PRPatternDefinition(Vector2[] points, string name = "")
    {
        m_cloudPoints = points;
        m_patternName = name;
    }

    public static PRPatternDefinition Deserialize(Dictionary<string,object> dict)
    {
        string m_patternName = dict["m_patternName"] as string;
        List<object> point = ((List<object>)dict["m_cloudPoints"]);
        Vector2[] m_cloudPoints = new Vector2[point.Count];
        for(int i=0; i<point.Count;i++)
        {
            m_cloudPoints[i] = ParseVector2((string)point[i]);
        }
        return new PRPatternDefinition(m_cloudPoints, m_patternName);
    }

    public static Vector2 ParseVector2(string vectorStr)
    {
        float xValue = float.Parse(vectorStr.Split(',')[0]);
        float yValue = float.Parse(vectorStr.Split(',')[1]);
        return new Vector2(xValue, yValue);
    }

    public Dictionary<string,object> Serialize()
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        dict.Add("m_patternName", m_patternName);
        dict.Add("m_cloudPoints", TransformVectors(m_cloudPoints));
        return dict;
    }

    private List<Vector2i> TransformVectors(Vector2[] points)
    {
        List<Vector2i> newPoints = new List<Vector2i>();
        foreach(Vector2 point in points)
        {
            newPoints.Add(new Vector2i(point.x, point.y));
        }
        return newPoints;
    }

    /// <summary>
    /// Auxiliar class to help the serialization of a Vector2
    /// </summary>
    public class Vector2i
    {
        [SerializeField]
        private float x;
        public float xValue
        {
            get { return x; }
        }

        [SerializeField]
        private float y;
        public float yValue
        {
            get { return y; }
        }

        public Vector2i(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return x.ToString() + "," + y.ToString();
        }
    }
}



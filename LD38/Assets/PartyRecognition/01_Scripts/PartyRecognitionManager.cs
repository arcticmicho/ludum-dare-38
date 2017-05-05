using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PartyRecognitionManager : MonoSingleton<PartyRecognitionManager>
{
    private List<PRPatternDefinition> m_patternDefinitionSet;
    public List<PRPatternDefinition> PatternDefinitionSet
    {
        get { return m_patternDefinitionSet; }
    }

    [SerializeField]
    private Texture2D[] m_patternTextures;
    public Texture2D[] PatternTextures
    {
        get { return m_patternTextures; }
    }

    [SerializeField]
    private string m_pathToFile;
    public string PathToFile
    {
        get { return m_pathToFile; }
    }

    [SerializeField]
    private int m_cloundPointsSampling = 32;
    public int CloudPointsSamplig
    {
        set
        {
            m_cloundPointsSampling = value;
            LoadDefinitionSet();
        }
        get { return m_cloundPointsSampling; }
    }

    [SerializeField]
    private float m_successThresholdPercent = 0.95f;

    public override void Init()
    {
        base.Init();
        m_patternDefinitionSet = new List<PRPatternDefinition>();
        LoadDefinitionSet();
    }

    private void LoadDefinitionSet()
    {        
        string json = File.ReadAllText(m_pathToFile);
        List<object> definitions = (List<object>) MiniJSON.Json.Deserialize(json);
        foreach(Dictionary<string,object> pattern in definitions)
        {
            PRPatternDefinition newPattern = PRPatternDefinition.Deserialize(pattern);
            m_patternDefinitionSet.Add(newPattern);
        }
    }

    public void AddPattern(PRPatternDefinition newDef)
    {
        m_patternDefinitionSet.Add(newDef);
    }

    /// <summary>
    /// Initalize a Recognition process using the Greedy5 Strategy as default Heuristic Strategy
    /// </summary>
    /// <param name="points"></param>
    public RecognitionResult Recognize(Vector2[] points)
    {
        return Recognize(points, new Greedy5RecognitionStrategy(0.5f, true));
    }

    public RecognitionResult Recognize(Vector2[] points, IRecognitionHeuristicStrategy strategy)
    {
        Vector2[] normalizedPoints = NormalizePoints(points, m_cloundPointsSampling);

        PRPatternDefinition pattern = new PRPatternDefinition(normalizedPoints);
        RecognitionResult result = new RecognitionResult(false,float.MaxValue);
        for(int i=0; i< m_patternDefinitionSet.Count; i++)
        {
            RecognitionProcess process = new RecognitionProcess(pattern, m_patternDefinitionSet[i], strategy, m_successThresholdPercent);
            RecognitionResult newResult = process.Recognize();
            if(newResult.Success && result.RecognitionScore > newResult.RecognitionScore)
            {
                result = newResult;
            }
        }
        return result;
    }

    public Vector2[] NormalizePoints(Vector2[] points)
    {
        return NormalizePoints(points, m_cloundPointsSampling);
    }

    private Vector2[] NormalizePoints(Vector2[] points, int samplingFactor)
    {
        Vector2[] normalizedPoints = new Vector2[points.Length];
        normalizedPoints = ScalePoints(points);
        normalizedPoints = TranslatePointsByPoint(normalizedPoints, CalculateCentroid(normalizedPoints));
        normalizedPoints = ResamplePoints(normalizedPoints, m_cloundPointsSampling);
        return normalizedPoints;
    }

    private Vector2[] ScalePoints(Vector2[] points)
    {
        float minX = float.MaxValue;
        float minY = float.MaxValue;
        float maxX = float.MinValue;
        float maxY = float.MinValue;

        for (int i = 0; i < points.Length; i++)
        {
            if (points[i].x > maxX)
            {
                maxX = points[i].x;
            }
            if (points[i].x < minX)
            {
                minX = points[i].x;
            }
            if (points[i].y > maxY)
            {
                maxY = points[i].y;
            }
            if (points[i].y < minY)
            {
                minY = points[i].y;
            }
        }

        Vector2[] newPoints = new Vector2[points.Length];
        float scale = Mathf.Max(maxX - minY, maxY - minX);
        for (int i = 0; i < points.Length; i++)
        {
            newPoints[i] = new Vector2((points[i].x - minX) / scale, (points[i].y - minY) / scale);
        }
        return newPoints;
    }

    private Vector2[] TranslatePointsByPoint(Vector2[] points, Vector2 point)
    {
        Vector2[] newPoints = new Vector2[points.Length];
        for(int i=0; i<points.Length; i++)
        {
            newPoints[i] = new Vector2(points[i].x - point.x, points[i].y - point.y);
        }
        return newPoints;
    }

    private Vector2 CalculateCentroid(Vector2[] points)
    {
        float cx = 0;
        float cy = 0;

        for(int i=0; i<points.Length; i++)
        {
            cx += points[i].x;
            cy += points[i].y;
        }
        return new Vector2(cx / (float)points.Length, cy / (float)points.Length);
    }

    private Vector2[] ResamplePoints(Vector2[] points, int samplingFactor)
    {
        if(points.Length == samplingFactor)
        {
            return points;
        }

        Vector2[] newPoints = new Vector2[samplingFactor];
        newPoints[0] = new Vector2(points[0].x, points[0].y);
        int numPoints = 1;
        float I = (float)PointsLenght(points) / (float)(samplingFactor - 1);
        float D = 0;

        for(int i=1; i < points.Length; i++)
        {
            float pDistance = MathUtils.EuclideanDistance(points[i - 1], points[i]);
            if(D + pDistance >= I)
            {
                Vector2 currentPoint = points[i - 1];
                while(D + pDistance >= I)
                {
                    float t = Mathf.Min(Mathf.Max((I - D) / pDistance, 0f), 1.0f);
                    if(float.IsNaN(t))
                    {
                        t = 0.5f;
                    }
                    newPoints[numPoints++] = new Vector2((1.0f - t) * currentPoint.x + t * points[i].x,
                        (1.0f - t) * currentPoint.y + t * points[i].y);
                    pDistance = D + pDistance - I;
                    D = 0;
                    currentPoint = newPoints[numPoints - 1];
                }
                D = pDistance;
            }else
            {
                D += pDistance;
            }            
        }
        if (numPoints == samplingFactor - 1)
        {
            newPoints[numPoints++] = new Vector2(points[points.Length - 1].x, points[points.Length - 1].y);
        }

        return newPoints;
    }

    private float PointsLenght(Vector2[] points)
    {
        float totalLenght = 0;
        for(int i=1; i<points.Length; i++)
        {
            totalLenght += MathUtils.EuclideanDistance(points[i - 1], points[i]);
        }
        return totalLenght;
    }

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Example : MonoBehaviour
{
    public enum ExampleMode
    {
        Recognize = 0,
        SavePattern = 1
    }

    [SerializeField]
    private GameObject m_pointRenderer;
    [SerializeField]
    private ExampleMode m_mode;
    [SerializeField]
    private string m_patternName;

    private List<GameObject> m_instantiatedObjects;
    private List<Vector2> m_points;

    private Rect m_drawArea;

	// Use this for initialization
	void Start ()
    {
        m_drawArea = new Rect(0, 0, Screen.width, Screen.height);
        m_points = new List<Vector2>();
        m_instantiatedObjects = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 currentPoint = Vector3.zero;
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0)
            {
                currentPoint = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                currentPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            }
        }

        if(m_drawArea.Contains(currentPoint))
        {
            if(Input.GetMouseButtonDown(0))
            {
                m_points.Add(new Vector2(currentPoint.x, currentPoint.y));
                GameObject newLine = GameObject.Instantiate(m_pointRenderer);
                newLine.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(currentPoint.x, currentPoint.y, 10));
                m_instantiatedObjects.Add(newLine);
            }

            if(Input.GetMouseButton(0))
            {
                m_points.Add(new Vector2(currentPoint.x, currentPoint.y));
                GameObject newLine = GameObject.Instantiate(m_pointRenderer);
                newLine.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(currentPoint.x, currentPoint.y, 10));
                m_instantiatedObjects.Add(newLine);
            }

            if(Input.GetMouseButtonUp(0))
            {
                ProcessPoints();
            }
        }else
        {
            ProcessPoints();
        }


    }

    private void ProcessPoints()
    {
        switch (m_mode)
        {
            case ExampleMode.Recognize:
                RecognitionResult result = PartyRecognitionManager.Instance.Recognize(m_points.ToArray());
                Debug.Log("Result: " + result.Success + "Score: " + result.RecognitionScore + " ScorePercent: "+ result.RecognitionScoreAsPercent + " PatternName: "+ result.PatternName);
                break;
            case ExampleMode.SavePattern:
                Vector2[] normalizedPoints = PartyRecognitionManager.Instance.NormalizePoints(m_points.ToArray());
                PRPatternDefinition newPattern = new PRPatternDefinition(normalizedPoints, m_patternName);
                PartyRecognitionManager.Instance.AddPattern(newPattern);
                break;
        }

        m_points.Clear();
        for (int i = 0; i < m_instantiatedObjects.Count; i++)
        {
            Destroy(m_instantiatedObjects[i]);
        }
    }
}

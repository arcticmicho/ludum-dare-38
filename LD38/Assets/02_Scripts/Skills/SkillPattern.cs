using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable, CreateAssetMenu(fileName = "SkillPattern", menuName ="GamePlay/Skills/SkillPattern")]
public class SkillPattern : ScriptableObject
{
    [SerializeField]
    private string m_skillPatternName;
    public string SkillPatternName
    {
        get { return m_skillPatternName; }
    }

    [SerializeField]
    private string m_patternRecognitionId;    
    public string PatternRecognitionId
    {
        get { return m_patternRecognitionId; }
    }

    [SerializeField]
    private GestureRecognizer.GesturePattern[] m_patterns;
    public GestureRecognizer.GesturePattern[] Patterns
    {
        get { return m_patterns; }
    }

    [SerializeField]
    private Sprite m_skillPatternImage;
    public Sprite SkillPatternImage
    {
        get { return m_skillPatternImage; }
    }

    [SerializeField]
    private float m_patternThreshold;
    public float PatternThreshold
    {
        get { return m_patternThreshold; }
    }

    public bool ContainPattern(GestureRecognizer.GesturePattern pattern)
    {
        for(int i=0, count=m_patterns.Length; i<count; i++)
        {
            if(pattern == m_patterns[i])
            {
                return true;
            }
        }
        return false;
    }
}

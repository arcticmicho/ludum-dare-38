using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
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
}

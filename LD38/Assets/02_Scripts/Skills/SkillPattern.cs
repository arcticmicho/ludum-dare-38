using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillPattern : ScriptableObject
{
    [SerializeField]
    private string m_skillPatternName;

    [SerializeField]
    private string m_patternRecognitionId;    	
}

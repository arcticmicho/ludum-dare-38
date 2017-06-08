using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDefinition : ScriptableObject
{
    [SerializeField]
    private string m_skillName;
    public string SkillName
    {
        get { return m_skillName; }
    }

    [SerializeField]
    private SkillPattern[] m_skillPatterns;
    public SkillPattern[] SkillPatterns
    {
        get { return m_skillPatterns; }
    }
}

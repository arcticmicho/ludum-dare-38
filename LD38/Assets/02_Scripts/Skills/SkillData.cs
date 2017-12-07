using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillData
{
    [SerializeField]
    private int m_skillLevel;
    public int SkillLevel
    {
        get { return m_skillLevel; }
    }

    [SerializeField]
    private SkillDefinition m_skillDef;	
    public SkillDefinition SkillDefinition
    {
        get { return m_skillDef; }
    }

    public SkillData(int level, SkillDefinition skillDef)
    {
        m_skillLevel = level;
        m_skillDef = skillDef;
    }

    public SkillData(int level, string skillDefId)
    {
        m_skillLevel = level;
        m_skillDef = ResourceManager.Instance.SkillRecources.GetSkillsById(skillDefId);
    }

    public DamageTable CalculateSpellDamage()
    {
        return m_skillDef.SpellDamage.Clone() * (Mathf.Pow(1 + m_skillDef.DamageIncrease, Mathf.Max(0,m_skillLevel-1)));
    }
}

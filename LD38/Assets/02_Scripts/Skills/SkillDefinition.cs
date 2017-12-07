using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDefinition : ScriptableObject
{
    [SerializeField]
    private string m_skillId;
    public string SkillId
    {
        get { return m_skillId; }
    }

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

    [SerializeField]
    private DamageTable m_spellDamage;
    public DamageTable SpellDamage
    {
        get { return m_spellDamage; }
    }

    [SerializeField, Range(0f, 1f)]
    private float m_damageIncrease;
    public float DamageIncrease
    {
        get { return m_damageIncrease; }
    }

    [SerializeField, Range(0f,1f)]
    private float m_criticalChance;
    public float CriticalChange
    {
        get { return m_criticalChance; }
    }

    [SerializeField]
    private float m_critialMultiplier;
    public float CriticalMultiplier
    {
        get { return m_critialMultiplier; }
    }

    [SerializeField]
    private float m_castingTime;
    public float CastingTime
    {
        get { return m_castingTime; }
    }

    [SerializeField]
    private GameObject m_spellPrefab;
    public GameObject SpellPrefab
    {
        get { return m_spellPrefab; }
    }

    [SerializeField]
    private GameObject m_impactPrefab;
    public GameObject ImpactPrefab
    {
        get { return m_impactPrefab; }
    }

    [SerializeField]
    private float m_spellSpeed;
    public float SpellSpeed
    {
        get { return m_spellSpeed; }
    }
}

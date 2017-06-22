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

    [SerializeField]
    private float m_damage;
    public float Damage
    {
        get { return m_damage; }
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

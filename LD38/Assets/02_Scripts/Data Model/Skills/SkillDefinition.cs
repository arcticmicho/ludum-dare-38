using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable, CreateAssetMenu(fileName = "SkillDefinition", menuName = "GamePlay/Skills/SkillDefinition")]
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

    [SerializeField]
    private ESkillTarget m_skillTarget;
    public ESkillTarget SkillTarget
    {
        get { return m_skillTarget; }
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
    private bool m_isHealingSkill;
    public bool IsHealingSkill
    {
        get { return m_isHealingSkill; }
    }

    [SerializeField]
    private float m_healingAmount;
    public float HealingAmount
    {
        get { return m_healingAmount; }
    }

    [SerializeField]
    private string m_castingAnimationTrigger;
    public string CastingAnimationTrigger
    {
        get { return m_castingAnimationTrigger; }
    }

    [SerializeField]
    private float m_castingTime;
    public float CastingTime
    {
        get { return m_castingTime; }
    }

    [SerializeField]
    private string m_skillAnimationTrigger;
    public string SkillAnimationTrigger
    {
        get { return m_skillAnimationTrigger; }
    }

    [SerializeField]
    private float m_skillAnimationWaitingTime;
    public float SkillAnimationWaitingTime
    {
        get { return m_skillAnimationWaitingTime; }
    }

    [SerializeField]
    private List<SkillEffect> m_skillEffects;
    public List<SkillEffect> SkillEffects
    {
        get { return m_skillEffects; }
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

public enum ESkillTarget
{
    SimpleTarget = 1,
    Self = 2,
    MultiTarget = 4
}



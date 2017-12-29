using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESkillTarget
{
    Single      = 1,
    Self        = 2,
    Multi       = 3,
    MultiSelf   = 4
}

[Serializable, CreateAssetMenu(fileName = "SkillDefinition", menuName = "GamePlay/Skills/SkillDefinition")]
public class SkillDefinition : ScriptableObject
{
    [Header("Info")]
    [SerializeField]
    private string m_skillId;

    [SerializeField]
    private string m_skillName;

    [Header("Not Exported")]
    [SerializeField]
    private SkillPattern[] m_skillPatterns;

    [Header("Attack")]
    [SerializeField]
    private DamageTable m_spellDamage;

    [SerializeField]
    private ESkillTarget m_skillTarget;

    [SerializeField, Range(0f, 1f)]
    private float m_damageIncrease;

    [SerializeField, Range(0f,1f)]
    private float m_criticalChance;

    [SerializeField]
    private float m_critialMultiplier;

    [SerializeField]
    private bool m_isHealingSkill;

    [SerializeField]
    private float m_healingAmount;

    [SerializeField]
    private string m_castingAnimationTrigger;

    [SerializeField]
    private float m_castingTime;


    [Header("Effect")]
    [SerializeField]
    private string m_skillAnimationTrigger;

    [SerializeField]
    private float m_skillAnimationWaitingTime;

    [SerializeField]
    private List<SkillEffect> m_skillEffects;

    [SerializeField]
    private BaseEffectController m_spellPrefab;

    [SerializeField]
    private BaseEffectController m_impactPrefab;

    [SerializeField]
    private float m_spellSpeed;

    #region Get/Set
    public string SkillId
    {
        get { return m_skillId; }
    }

    public string SkillName
    {
        get { return m_skillName; }
    }

    public SkillPattern[] SkillPatterns
    {
        get { return m_skillPatterns; }
    }

    public DamageTable SpellDamage
    {
        get { return m_spellDamage; }
    }

    public ESkillTarget SkillTarget
    {
        get { return m_skillTarget; }
    }

    public float DamageIncrease
    {
        get { return m_damageIncrease; }
    }

    public float CriticalChange
    {
        get { return m_criticalChance; }
    }

    public float CriticalMultiplier
    {
        get { return m_critialMultiplier; }
    }

    public bool IsHealingSkill
    {
        get { return m_isHealingSkill; }
    }

    public float HealingAmount
    {
        get { return m_healingAmount; }
    }

    public string CastingAnimationTrigger
    {
        get { return m_castingAnimationTrigger; }
    }

    public float CastingTime
    {
        get { return m_castingTime; }
    }

    public string SkillAnimationTrigger
    {
        get { return m_skillAnimationTrigger; }
    }

    public float SkillAnimationWaitingTime
    {
        get { return m_skillAnimationWaitingTime; }
    }

    public List<SkillEffect> SkillEffects
    {
        get { return m_skillEffects; }
    }

    public GameObject SpellPrefab
    {
        get { return m_spellPrefab; }
    }

    public GameObject ImpactPrefab
    {
        get { return m_impactPrefab; }
    }

    public float SpellSpeed
    {
        get { return m_spellSpeed; }
    }
    #endregion

    #region Editor Functions and export
    public void Editor_SetExportData(Dictionary<string,object> exportData)
    {
        m_skillId     = SerializeHelper.GetValue<string>(exportData, "Id");
        m_skillName   = SerializeHelper.GetValue<string>(exportData, "Name");
        m_skillTarget = Editor_ParseTarget(SerializeHelper.GetValue<string>(exportData, "Target"));

        m_criticalChance    = SerializeHelper.GetValue<float>(exportData, "Critical");
        m_critialMultiplier = SerializeHelper.GetValue<float>(exportData, "CriticalMultiplier");

        m_damageIncrease = SerializeHelper.GetValue<float>(exportData, "PowerIncrease");
        m_castingTime = SerializeHelper.GetValue<float>(exportData, "CastingTime");

        // Speel Damage Table
        var spellPower = SerializeHelper.GetValue<int>(exportData, "Power");
        var spellElement = SerializeHelper.GetValue<string>(exportData, "Element");
    }

    private ESkillTarget Editor_ParseTarget(string target)
    {
        switch(target)
        {
            case "Single":
                return ESkillTarget.Single;
            case "Multi":
                return ESkillTarget.Multi;
            case "Self":
                return ESkillTarget.Self;
            case "Multi Self":
                return ESkillTarget.MultiSelf;
        }

        return ESkillTarget.Single;

    }
        #endregion
}




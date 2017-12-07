using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlowData
{
    private Character m_owner;
    public Character Owner
    {
        get { return m_owner; }
    }

    private Character m_target;
    public Character Target
    {
        get { return m_target; }
    }

    private SkillData m_skillData;
    public SkillData SkillData
    {
        get { return m_skillData; }
    }

    private DamageTable m_spellDamage;
    public DamageTable SpellDamage
    {
        get { return m_spellDamage; }
    }

    private DamageTable m_resitanceTable;
    public DamageTable ResistanceTable
    {
        get { return m_resitanceTable; }
    }

    public DamageFlowData(Character owner, Character target, SkillData skillData, DamageTable resistanceTable = null)
    {
        m_owner = owner;
        m_target = target;
        m_skillData = skillData;
        m_spellDamage = skillData.SkillDefinition.SpellDamage.Clone();
        if(resistanceTable ==  null)
        {
            m_resitanceTable = m_target.ResistanceTable;   
        }else
        {
            m_resitanceTable = resistanceTable.Clone();
        }
    }

}

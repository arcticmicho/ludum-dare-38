using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlow
{
    public enum EDamageFlowStep
    {
        Init = 0,
        CriticalMod = 1,
        WandMod = 2,
        ComboMod = 3,
        SideEffect = 4,        
        TotalDamage = 5,
        ResistanceStep = 6

    }

    private DamageFlowData m_flowInfo;
    private List<DamageTable> m_extaDamages;
    private DamageTable m_totalDamage;

    public DamageFlow(Character owner, Character target, SkillData skillData, DamageTable resistanceTable = null)
    {        
        m_flowInfo = new DamageFlowData(owner, target, skillData, resistanceTable);
    }

    public DamageTable ExecuteFlow()
    {
        InitStep();
        CriticalModStep();
        WandModStep();
        ComboModStep();
        SideEffectStep();
        m_totalDamage = TotalDamageStep();
        m_totalDamage = ResistanceStep(m_totalDamage);
        return m_totalDamage;
    }

    #region Damage Calculations: TODO, replace with a more scalable design
    private void InitStep()
    {
        m_extaDamages = new List<DamageTable>();
    }

    private void CriticalModStep()
    {
        float dice = RandomUtils.Random01();
        if(dice < m_flowInfo.SkillData.SkillDefinition.CriticalChange)
        {
            m_extaDamages.Add(m_flowInfo.SpellDamage * m_flowInfo.SkillData.SkillDefinition.CriticalMultiplier);
        }
    }

    private void WandModStep()
    {
        if(m_flowInfo.Owner.Data.WeaponTemplate != null)
        {
            m_extaDamages.Add(m_flowInfo.SpellDamage * m_flowInfo.Owner.Data.WeaponTemplate.ItemDamageSpellIncrease);
        }
    }

    private void ComboModStep()
    {
        //TODO: ComboModStep
    }

    private void SideEffectStep()
    {
        //TODO: Create side effects
    }

    private DamageTable TotalDamageStep()
    {
        DamageTable totalDamage = m_flowInfo.SpellDamage;
        for(int i=0, count =m_extaDamages.Count; i<count; i++)
        {
            totalDamage += m_extaDamages[i];
        }
        return totalDamage;
    }

    private DamageTable ResistanceStep(DamageTable totalDamage)
    {
        return totalDamage - (totalDamage * m_flowInfo.ResistanceTable);
    }
    #endregion
}

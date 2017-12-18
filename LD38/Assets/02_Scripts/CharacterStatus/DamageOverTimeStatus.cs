using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageOverTimeStatus : CharacterStatus
{
    private DamageTable m_damageTable;

    public DamageOverTimeStatus(DamageTable damageTable, int numberOfTicks, float statusDuration, Character owner, Character caster, string statusName, string statusDesc, Sprite statusImage) : base(statusDuration, numberOfTicks, owner, caster, statusName, statusDesc, statusImage)
    {
        m_damageTable = damageTable;
    }

    public override void ApplyStatus()
    {
        m_owner.ApplyDamage(CalculateDamage());
    }

    public override ECharacterStatus GetStatusType()
    {
        return ECharacterStatus.DamageOverTime;
    }

    public override void RemoveStatus()
    {
        
    }

    protected override float GetStatusPower()
    {
        DamageType dType;
        return m_damageTable.GetTotalDamage(out dType) * m_numberOfTicks;
    }

    private DamageTable CalculateDamage()
    {
        return m_damageTable - (m_damageTable * m_owner.ResistanceTable);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterResistanceStatus : CharacterStatus
{
    private DamageTable m_modTable;
    private Modifier<DamageTable> m_modifier;

    public AlterResistanceStatus(DamageTable modTable, float statusDuration, Character owner, Character caster, string statusName, string statusDesc, Sprite statusImage) : base(statusDuration, 0, owner, caster, statusName, statusDesc, statusImage)
    {
        m_modTable = modTable;
    }

    public override void ApplyStatus()
    {
        if(m_modifier == null)
        {
            m_modifier = m_owner.ResistanceTableModifier.AddModifier(m_modTable);
        }
    }

    public override ECharacterStatus GetStatusType()
    {
        return ECharacterStatus.ResistanceDown;
    }

    public override void RemoveStatus()
    {
        if(m_modifier != null)
        {
            m_owner.ResistanceTableModifier.RemoveModifier(m_modifier);
        }
    }

    protected override float GetStatusPower()
    {
        DamageType type;
        return m_modTable.GetTotalDamage(out type);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageOverTimeEffect", menuName = "GamePlay/SkillEffects/DamageOverTime")]
public class DamageOverTimeEffect : SkillEffect
{
    [SerializeField]
    private DamageTable m_damagePerTick;

    public override CharacterStatus GenerateStatus(Character caster, Character target)
    {
        return new DamageOverTimeStatus(m_damagePerTick, m_skillNumberOfTicks, m_skillEffectDuration, target, caster, m_skillEffectName, m_skillEffectDescription, m_skillEffectImage);
    }
}

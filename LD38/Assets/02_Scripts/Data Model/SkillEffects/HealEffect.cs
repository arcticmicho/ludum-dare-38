using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealEffect", menuName = "GamePlay/SkillEffects/HealEffect")]
public class HealEffect : SkillEffect
{
    [SerializeField]
    private float m_healAmount;

    public override CharacterStatus GenerateStatus(Character caster, Character target)
    {
        return new HealingStatus(m_healAmount, m_skillNumberOfTicks, m_skillEffectDuration, target, caster, m_skillEffectName, m_skillEffectDescription, m_skillEffectImage);
    }
}

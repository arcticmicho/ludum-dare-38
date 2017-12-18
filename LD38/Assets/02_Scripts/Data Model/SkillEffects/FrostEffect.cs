using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FrostEffect", menuName = "GamePlay/SkillEffects/FrostEffect")]
public class FrostEffect : SkillEffect
{
    [SerializeField, Range(0f, 1f)]
    private float m_frostEffectPercent;

    public override CharacterStatus GenerateStatus(Character caster, Character target)
    {
        return new FrozenStatus(m_frostEffectPercent, m_skillEffectDuration, target, caster, m_skillEffectName, m_skillEffectDescription, m_skillEffectImage);
    }
}

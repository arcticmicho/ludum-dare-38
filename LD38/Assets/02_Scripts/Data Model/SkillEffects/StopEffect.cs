using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StopEffect", menuName = "GamePlay/SkillEffects/StopEffect")]
public class StopEffect : SkillEffect
{

    public override CharacterStatus GenerateStatus(Character caster, Character target)
    {
        return new StopStatus(m_skillEffectDuration, target, caster);
    }    
}

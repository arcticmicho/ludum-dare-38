using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FrostEffect", menuName = "GamePlay/SkillEffects/DownResistanceEffect")]
public class DownResistanceEffect : SkillEffect
{
    [SerializeField]
    private DamageTable m_modTable;

    public override CharacterStatus GenerateStatus(Character caster, Character target)
    {
        return new AlterResistanceStatus(m_modTable, m_skillEffectDuration, target, caster, m_skillEffectName, m_skillEffectDescription, m_skillEffectImage);
    }
}

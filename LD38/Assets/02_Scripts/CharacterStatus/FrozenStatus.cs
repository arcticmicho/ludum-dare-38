﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrozenStatus : CharacterStatus
{
    private float m_frozenModifier;
    private Modifier<float> m_modifier;

    public FrozenStatus(float frozenModifier, float statusDuration, Character owner, Character caster, string statusName, string statusDesc, Sprite statusImage) : base(statusDuration, 0, owner, caster, statusName, statusDesc, statusImage)
    {
        m_frozenModifier = frozenModifier;
    }

    public override void ApplyStatus()
    {
        if(m_modifier == null)
        {
            m_modifier = m_owner.MovementSpeedModifier.AddModifier(m_frozenModifier);
        }        
    }

    public override void RemoveStatus()
    {
        if(m_modifier != null)
        {
            m_owner.MovementSpeedModifier.RemoveModifier(m_modifier);
        }
    }

    public override ECharacterStatus GetStatusType()
    {
        return ECharacterStatus.Frozen;
    }

    protected override float GetStatusPower()
    {
        return m_statusDuration;
    }
}

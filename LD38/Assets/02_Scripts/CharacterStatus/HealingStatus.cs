using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealingStatus : CharacterStatus
{
    private float m_healAmount;
    
    public HealingStatus(float healAmount, int numberOfTicks, float statusDuration, Character owner, Character caster, string statusName, string statusDesc, Sprite statusImage) : base(statusDuration, numberOfTicks, owner, caster, statusName, statusDesc, statusImage)
    {
        m_healAmount = healAmount;
    }

    public override void ApplyStatus()
    {
        m_owner.ApplyHeal(m_healAmount);
    }

    public override ECharacterStatus GetStatusType()
    {
        return ECharacterStatus.Healing;
    }

    public override void RemoveStatus()
    {
        
    }

    protected override float GetStatusPower()
    {
        return m_healAmount * m_numberOfTicks;
    }
}

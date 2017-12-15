using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealingStatus : CharacterStatus
{
    private float m_healAmount;

    public override string Description
    {
        get
        {
            return "Heal the target.";
        }
    }

    public override Image StatusImage
    {
        get
        {
            return null;
        }
    }

    public override string StatusName
    {
        get
        {
            return "Healing";
        }
    }

    public HealingStatus(float healAmount, int numberOfTicks, float statusDuration, Character owner, Character caster) : base(statusDuration, numberOfTicks, owner, caster)
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

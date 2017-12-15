using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopStatus : CharacterStatus
{

    public override string Description
    {
        get
        {
            return "Stop all the target actions, including movements, cooldowns and Casting.";
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
            return "Stop";
        }
    }

    public StopStatus(float statusDuration, Character owner, Character caster) : base(statusDuration, 0, owner, caster)
    {
    }

    public override void ApplyStatus()
    {
        m_owner.Entity.SetAnimatorTimeScale(0f);
    }

    public override ECharacterStatus GetStatusType()
    {
        return ECharacterStatus.Stop;
    }

    public override void RemoveStatus()
    {
        m_owner.Entity.ResetAnimatorTimeScale();
    }

    protected override float GetStatusPower()
    {
        return m_statusDuration;
    }
}

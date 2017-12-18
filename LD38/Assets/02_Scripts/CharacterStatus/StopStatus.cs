using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopStatus : CharacterStatus
{    

    public StopStatus(float statusDuration, Character owner, Character caster, string statusName, string statusDesc, Sprite statusImage) : base(statusDuration, 0, owner, caster, statusName, statusDesc, statusImage)
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

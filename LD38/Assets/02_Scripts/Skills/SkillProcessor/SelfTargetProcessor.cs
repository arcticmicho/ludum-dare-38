using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfTargetProcessor : BaseSkillProcessor
{
    public SelfTargetProcessor(GameSession gameSession, Character owner, Character[] target, SkillData skillData) : base(gameSession, owner, target, skillData)
    {
    }

    protected override bool OnActionStep()
    {
        return true;
    }

    protected override void OnChangedToActionStep()
    {
        
    }

    protected override void OnChangedToHittingStep()
    {

    }

    protected override bool OnHittingStep()
    {
        return true;
    }

    protected override void OnActionCanceled()
    {

    }

    protected override bool ShouldCancelAction()
    {
        return false;
    }
}

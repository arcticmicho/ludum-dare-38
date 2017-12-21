using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastAction : GameAction
{
    private SkillData m_skillData;
    private Character m_owner;

    public Action OnActionEnds;
    public Action OnFinishCasting;

    private BaseSkillProcessor m_processor;

    public override DateTime ActionTime
    {
        get
        {
            return TimeManager.Instance.CurrentTime;
        }
    }

    public CastAction(GameSession session, SkillData skillData, Character owner) : base(session)
    {
        m_skillData = skillData;
        m_owner = owner;
    }

    public override void StartAction()
    {
        m_processor = CreateProcessorSkill();
        m_processor.OnFinishCasting += FinishCasting;
        m_processor.StartProcessor();
    }

    public override void EndAction()
    {
        if(OnActionEnds != null)
        {
            OnActionEnds();
        }
    }

    public override bool ProcessAction(float deltaTime)
    {
        return m_processor.UpdateProcessor();
    }

    private void FinishCasting()
    {
        if(OnFinishCasting != null)
        {
            OnFinishCasting();
        }
    }

    private BaseSkillProcessor CreateProcessorSkill()
    {
        switch (m_skillData.SkillDefinition.SkillTarget)
        {
            case ESkillTarget.SimpleTarget:
                return new SingleTargetProcessor(m_contextSession, m_owner, m_owner.CurrentTarget, m_skillData);
            case ESkillTarget.Self:
                return new SelfTargetProcessor(m_contextSession, m_owner, new Character[] { m_owner }, m_skillData);
            case ESkillTarget.MultiTarget:
                Debug.LogError("To Be Implemented");
                return null;
            default:
                return new SingleTargetProcessor(m_contextSession, m_owner, m_owner.CurrentTarget, m_skillData);
        }
    }
}

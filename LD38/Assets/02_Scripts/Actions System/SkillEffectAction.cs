using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectAction : GameAction
{
    private SkillEffect m_effect;

    private Character m_target;

    private Character m_caster;

    private CharacterStatus m_status;

    private CharacterStatusWidget m_statusWidget;

    private bool m_cancelAction;

    private float m_elapsedTime;
    private float m_nextStatusEffect;

    public SkillEffectAction(SkillEffect effect, Character target, Character caster, GameSession contextSession) : base(contextSession)
    {
        m_effect = effect;
        m_target = target;
        m_caster = caster;
        m_cancelAction = false;
        m_elapsedTime = 0f;
    }

    public override DateTime ActionTime
    {
        get
        {
            return TimeManager.Instance.CurrentTime;
        }
    }

    public override void EndAction()
    {
        if(!m_cancelAction)
        {
            m_target.RemoveStatus(m_status);
        }
        if(m_statusWidget != null)
        {
            m_target.Entity.CharacterCanvas.RemoveCharacterStatus(m_statusWidget);
        }
    }

    public override bool ProcessAction(float deltaTime)
    {
        m_elapsedTime += TimeManager.Instance.DeltaTime;

        if(m_target.IsDeath || m_cancelAction)
        {
            return true;
        }

        if(m_effect.NumberOfTicks > 0)
        {
            m_nextStatusEffect -= TimeManager.Instance.DeltaTime;
            if(m_nextStatusEffect <= 0f)
            {
                m_nextStatusEffect = m_effect.EffectDuration / m_effect.NumberOfTicks;
                m_status.ApplyStatus();
            }
        }else
        {
            return true;
        }
        if(m_elapsedTime >= m_effect.EffectDuration)
        {
            return true;
        }else
        {
            return false;
        }
    }

    public override void StartAction()
    {
        m_status = m_effect.GenerateStatus(m_caster, m_target);
        if(!m_target.IsDeath && m_target.AddStatus(m_status))
        {
            m_statusWidget = m_target.Entity.CharacterCanvas.CreateCharacterStatus(m_status);

            if(m_effect.ApplyEffectAtStart)
            {
                m_status.ApplyStatus();
            }
            if(m_effect.NumberOfTicks > 0)
            {
                m_nextStatusEffect = m_effect.EffectDuration / m_effect.NumberOfTicks;
            }
        }else
        {
            m_cancelAction = true;
        }
    }
}

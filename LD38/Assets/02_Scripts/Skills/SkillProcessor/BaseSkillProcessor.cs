using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkillProcessor
{
    public enum ESkillProcessorStep
    {
        Casting,
        SkillAnimation,
        Action,
        Hitting,
        Ending
    }

    protected ESkillProcessorStep m_currentStep;
    protected SkillData m_skillData;
    protected Character m_owner;
    protected GameSession m_session;

    protected Character[] m_targets;

    private float m_elapsedCastingTime;
    private float m_castingTime;

    private float m_skillAnimationTime;
    private float m_elapsedSkillAnimationTime;

    public Action OnProcessorEnds;
    public Action OnFinishCasting;

    public BaseSkillProcessor(GameSession gameSession, Character owner, Character[] targets, SkillData skillData)
    {
        m_owner = owner;
        m_session = gameSession;
        m_skillData = skillData;
        m_elapsedSkillAnimationTime = 0f;
        m_elapsedCastingTime = 0f;
        m_targets = targets;
    }

    public void StartProcessor()
    {
        ChangeStep(ESkillProcessorStep.Casting);
    }

    public bool UpdateProcessor()
    {
        if ((m_owner.IsDeath && m_currentStep == ESkillProcessorStep.Casting) || ShouldCancelAction())
        {
            OnActionCanceled();
            return true;
        }
        UpdateStep();
        return m_currentStep == ESkillProcessorStep.Ending;
    }

    private void UpdateStep()
    {
        switch (m_currentStep)
        {
            case ESkillProcessorStep.Casting:
                m_elapsedCastingTime += TimeManager.Instance.DeltaTime;
                if (m_elapsedCastingTime >= m_castingTime)
                {
                    
                    ChangeStep(ESkillProcessorStep.SkillAnimation);
                }
                break;
            case ESkillProcessorStep.SkillAnimation:
                m_elapsedSkillAnimationTime += TimeManager.Instance.DeltaTime;
                if(m_elapsedSkillAnimationTime >= m_skillAnimationTime)
                {
                    if (OnFinishCasting != null)
                    {
                        OnFinishCasting();
                    }
                    ChangeStep(ESkillProcessorStep.Action);
                }
                break;
            case ESkillProcessorStep.Action:
                if(OnActionStep())
                {
                    ChangeStep(ESkillProcessorStep.Hitting);
                }
                break;
            case ESkillProcessorStep.Hitting:
                if(OnHittingStep())
                {
                    ChangeStep(ESkillProcessorStep.Ending);
                }
                break;
            case ESkillProcessorStep.Ending:
                break;
        }
    }

    private void ChangeStep(ESkillProcessorStep newStep)
    {
        m_currentStep = newStep;
        switch (newStep)
        {
            case ESkillProcessorStep.Casting:
                m_castingTime = m_skillData.SkillDefinition.CastingTime;
                m_elapsedCastingTime = 0f;
                m_owner.Entity.PlayCastAnimation();
                break;
            case ESkillProcessorStep.SkillAnimation:
                m_skillAnimationTime = m_skillData.SkillDefinition.SkillAnimationWaitingTime;
                m_elapsedSkillAnimationTime = 0;
                m_owner.Entity.PlayAnimationWithTrigger(m_skillData.SkillDefinition.SkillAnimationTrigger);
                break;
            case ESkillProcessorStep.Action:
                OnChangedToActionStep();
                break;
            case ESkillProcessorStep.Hitting:
                ApplyDamage();

                OnChangedToHittingStep();
                break;
            case ESkillProcessorStep.Ending:
                break;
        }
    }

    private void ApplyDamage()
    {
        for(int i=0, count = m_targets.Length; i<count; i++)
        {
            BaseEffectController particle = GameObject.Instantiate< BaseEffectController>(m_skillData.SkillDefinition.ImpactPrefab);
            particle.transform.position = m_targets[i].Entity.transform.position;
            particle.StartEffect(0, true);
            if (!m_skillData.SkillDefinition.IsHealingSkill)
            {
                DamageFlow flow = new DamageFlow(m_owner, m_targets[i], m_skillData, m_session);
                m_targets[i].ApplyDamage(flow.ExecuteFlow());
            }
            else
            {
                m_targets[i].ApplyHeal(m_skillData.SkillDefinition.HealingAmount);
            }            
        }        
    }

    /// <summary>
    /// Method called every frame when the processor is in the Action step. Must return true when the step is completed.
    /// </summary>
    /// <returns></returns>
    protected abstract bool OnActionStep();
    /// <summary>
    /// Method called when the processor change to the Action step (called just once)
    /// </summary>
    protected abstract void OnChangedToActionStep();

    /// <summary>
    /// Method called every frame when the processor is in the Hitting step. Must return true when the step is completed.
    /// </summary>
    /// <returns></returns>
    protected abstract bool OnHittingStep();
    /// <summary>
    /// Method called when the processor change to the Hitting step (called just once)
    /// </summary>
    protected abstract void OnChangedToHittingStep();

    /// <summary>
    /// Method called to check if the action should be canceled 
    /// </summary>
    /// <returns></returns>
    protected abstract bool ShouldCancelAction();

    /// <summary>
    /// Method called when the action is caneled by the ShouldCancelAction or if the Owner dies before launching the Skill
    /// </summary>
    protected abstract void OnActionCanceled();
}

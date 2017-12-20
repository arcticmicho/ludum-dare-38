﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastAction : GameAction
{
    public enum ECastStep
    {
        Casting,
        Throwing,
        Hitting,
        Ending
    }

    private ECastStep m_currentStep;
    private SkillData m_skillData;
    private Character m_owner;

    private Character[] m_targets;

    private float m_elapsedCastingTime;
    private float m_castingTime;

    private GameObject m_projectile;
    private Character m_projectileTarget;

    private float m_hitWaitTime = 1f;
    private float m_elapsedHitTime = 0f;

    public Action OnActionEnds;
    public Action OnFinishCasting;

    public override DateTime ActionTime
    {
        get
        {
            return TimeManager.Instance.CurrentTime;
        }
    }

    public CastAction(GameSession session, SkillData skillData, Character owner, Character[] targets) : base(session)
    {
        m_skillData = skillData;
        m_targets = targets;
        m_owner = owner;
    }

    public override void StartAction()
    {
        ChangeStep(ECastStep.Casting);
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
        if (m_owner.IsDeath && m_currentStep == ECastStep.Casting)
        {
            return true;
        }
        UpdateStep();
        return m_currentStep == ECastStep.Ending;
    }    

    private void UpdateStep()
    {
        switch (m_currentStep)
        {
            case ECastStep.Casting:
                m_elapsedCastingTime += TimeManager.Instance.DeltaTime;
                if(m_elapsedCastingTime >= m_castingTime)
                {
                    if(OnFinishCasting != null)
                    {
                        OnFinishCasting();
                    }
                    ChangeStep(ECastStep.Throwing);
                }
                break;
            case ECastStep.Throwing:
                Vector3 dir = (m_projectileTarget.Entity.transform.position - m_projectile.transform.position);
                float magn = dir.sqrMagnitude;
                if(magn > 0.1f)
                {
                    m_projectile.transform.position += dir.normalized * (m_skillData.SkillDefinition.SpellSpeed * TimeManager.Instance.DeltaTime);
                }
                else
                {
                    m_projectile.transform.position = m_projectileTarget.Entity.transform.position;
                    ChangeStep(ECastStep.Hitting);
                }
                break;
            case ECastStep.Hitting:
                m_elapsedHitTime += TimeManager.Instance.DeltaTime;
                if(m_elapsedHitTime >= m_hitWaitTime)
                {
                    ChangeStep(ECastStep.Ending);
                }
                break;
            case ECastStep.Ending:
                break;
        }
    }

    private void ChangeStep(ECastStep newStep)
    {
        m_currentStep = newStep;
        switch (newStep)
        {
            case ECastStep.Casting:
                m_castingTime = m_skillData.SkillDefinition.CastingTime;
                m_elapsedCastingTime = 0f;
                m_owner.Entity.PlayCastAnimation();
                break;
            case ECastStep.Throwing:
                m_projectile = GameObject.Instantiate<GameObject>(m_skillData.SkillDefinition.SpellPrefab);
                m_projectileTarget = m_targets[0];
                m_projectile.transform.position = m_owner.Entity.transform.position + new Vector3(0.5f * Mathf.Sign((m_projectileTarget.Entity.transform.position - m_owner.Entity.transform.position).x), 0f, 0f);
                m_owner.Entity.PlayThrowAbilityAnimation();
                break;
            case ECastStep.Hitting:
                GameObject.Destroy(m_projectile.gameObject);
                GameObject particle = GameObject.Instantiate(m_skillData.SkillDefinition.ImpactPrefab);
                particle.transform.position = m_projectileTarget.Entity.transform.position;
                m_elapsedCastingTime = 0f;
                ApplyDamage();
                break;
            case ECastStep.Ending:
                break;
        }
    }

    private void ApplyDamage()
    {
        DamageFlow flow = new DamageFlow(m_owner, m_projectileTarget, m_skillData, m_contextSession);
        m_projectileTarget.ApplyDamage(flow.ExecuteFlow());
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTargetProcessor : BaseSkillProcessor
{
    private GameObject m_projectile;
    private Character m_projectileTarget;

    public SingleTargetProcessor(GameSession gameSession, Character owner, Character target, SkillData skillData) : base(gameSession, owner, new Character[]{ target }, skillData)
    {
        m_projectileTarget = target;
    }

    protected override bool OnActionStep()
    {
        Vector3 dir = (m_projectileTarget.Entity.transform.position - m_projectile.transform.position);
        float magn = dir.sqrMagnitude;
        if (magn > 0.1f)
        {
            m_projectile.transform.position += dir.normalized * (m_skillData.SkillDefinition.SpellSpeed * TimeManager.Instance.DeltaTime);
            return false;
        }
        else
        {
            m_projectile.transform.position = m_projectileTarget.Entity.transform.position;
            return true;
        }
    }

    protected override void OnChangedToActionStep()
    {
        m_projectile = GameObject.Instantiate<GameObject>(m_skillData.SkillDefinition.SpellPrefab);
        m_projectile.transform.position = m_owner.Entity.transform.position + new Vector3(0.5f * Mathf.Sign((m_projectileTarget.Entity.transform.position - m_owner.Entity.transform.position).x), 0f, 0f);
        m_owner.Entity.PlayThrowAbilityAnimation();
    }

    protected override void OnChangedToHittingStep()
    {
        GameObject.Destroy(m_projectile.gameObject);
    }

    protected override bool OnHittingStep()
    {
        return true;
    }

    protected override bool ShouldCancelAction()
    {
        return m_projectileTarget.IsDeath;
    }


    protected override void OnActionCanceled()
    {
        if(m_projectile != null)
        {
            GameObject.Destroy(m_projectile.gameObject);
        }
    }
}

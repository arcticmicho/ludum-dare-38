using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveToCurrentPointState : GenericState<EnemyCharacter, EnemyTransitionData>
{
    private Vector3 m_destination;
    private bool m_reachedPosition;

    public override void OnEnter(EnemyTransitionData data)
    {
        base.OnEnter(data);
        m_reachedPosition = false;
        m_destination = m_character.CurrentPoint.transform.position;
        m_character.Entity.PlayWalkAnimation();
    }

    public override StateTransition<EnemyTransitionData> EvaluateTransition()
    {
        if(m_reachedPosition)
        {
            return StateTransition<EnemyTransitionData>.Transition<EnemyIdleState,EnemyCharacter>();
        }
        return base.EvaluateTransition();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Vector3 dir = (m_destination - m_character.Entity.transform.position);
        float magn = dir.sqrMagnitude;
        if (magn > 0.05f)
        {
            m_character.Entity.TranslateEntity(m_character.Entity.transform.position + dir.normalized * (m_character.MovementSpeed * TimeManager.Instance.DeltaTime));
        }
        else
        {
            m_character.Entity.TranslateEntity(m_destination);
            m_reachedPosition = true;
        }
    }

    public override bool CanUpdate()
    {
        return !m_character.IsStunned || !m_character.IsDeath;
    }

    public override void OnExtit()
    {
        base.OnExtit();
        m_character.Entity.PlayIdleAnimation();
    }

}

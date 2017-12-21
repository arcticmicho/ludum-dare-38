using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : GenericState<EnemyCharacter, EnemyTransitionData>
{
    private float m_idleTime;
    private float m_elapsedIdleTime;

    public override void OnEnter(EnemyTransitionData data)
    {
        base.OnEnter(data);
        SetIdleTime();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        m_elapsedIdleTime += TimeManager.Instance.DeltaTime;
        if (m_elapsedIdleTime >= m_idleTime)
        {
            SetIdleTime();
            SkillData skillData = m_character.GetRandomAbility();
            m_character.Session.ActionManager.EnqueueAction(new CastAction(m_character.Session, skillData, m_character));
        }
    }

    private void SetIdleTime()
    {
        m_idleTime = UnityEngine.Random.Range(8f, 10f);
        m_elapsedIdleTime = 0;
    }

}

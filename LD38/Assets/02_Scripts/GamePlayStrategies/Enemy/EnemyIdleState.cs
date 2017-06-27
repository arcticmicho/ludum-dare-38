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
            GameSession session = GameManager.Instance.ActiveGameSession;
            SetIdleTime();
            SkillDefinition skillDef = m_character.GetRandomAbility();
            session.ActionManager.EnqueueAction(new CastAction(skillDef, m_character, new Character[] {session.MainCharacter}));
        }
    }

    private void SetIdleTime()
    {
        m_idleTime = UnityEngine.Random.Range(8f, 10f);
        m_elapsedIdleTime = 0;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingGameplayState : GenericState<Wizard, TransitionData>
{
    private bool m_finishCasting;

    public override void OnEnter(TransitionData data)
    {
        base.OnEnter(data);
        CastAction action = new CastAction(m_character.Session, data.SelectedSkill, m_character, new Character[]{ m_character.Session.CurrentTarget });
        action.OnActionEnds += OnFinishCasting;
        m_character.Session.ActionManager.EnqueueAction(action);
        UIPartyManager.Instance.RequestView<CastingView>();
        UIPartyManager.Instance.GetView<CastingView>().SetSkillName(data.SelectedSkill.SkillDefinition.SkillName);
    }

    public override StateTransition<TransitionData> EvaluateTransition()
    {
        if(m_finishCasting)
        {
            return StateTransition<TransitionData>.Transition<IdleGameplayState,Wizard>();
        }
        return StateTransition<TransitionData>.None;
    }

    private void OnFinishCasting()
    {
        m_finishCasting = true;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void OnExtit()
    {
        base.OnExtit();
        m_finishCasting = false;
    }
}

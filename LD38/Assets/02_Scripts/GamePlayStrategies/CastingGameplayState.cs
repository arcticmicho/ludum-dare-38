using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingGameplayState : GameplayState<Wizard, TransitionData>
{
    private bool m_finishCasting;

    public override void OnEnter(TransitionData data)
    {
        base.OnEnter(data);
        CastAction action = new CastAction(data.SelectedSkill, m_character, new Character[]{ GameManager.Instance.ActiveGameSession.CurrentTarget });
        action.OnActionEnds += OnFinishCasting;
        GameManager.Instance.ActiveGameSession.ActionManager.EnqueueAction(action);
        UIPartyManager.Instance.RequestView<CastingView>();
        UIPartyManager.Instance.GetView<CastingView>().SetSkillName(data.SelectedSkill.SkillName);
    }

    public override GameplayTransition EvaluateTransition()
    {
        if(m_finishCasting)
        {
            return GameplayTransition.Transition<IdleGameplayState>();
        }
        return GameplayTransition.None;
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

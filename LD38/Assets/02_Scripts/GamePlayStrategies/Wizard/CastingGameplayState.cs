using GameModules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingGameplayState : GenericState<Wizard, TransitionData>
{
    private bool m_finishCasting;

    public override void OnEnter(TransitionData data)
    {
        base.OnEnter(data);
        if(m_character.Session.CurrentTarget != null)
        {
            CastAction action = new CastAction(m_character.Session, data.SelectedSkill, m_character);
            action.OnFinishCasting += OnFinishCasting;
            m_character.Session.ActionController.EnqueueAction(action);

            UIManager.Instance.HideHUD();
            CastingHUD hud = UIManager.Instance.GetHUDElement<CastingHUD>();
            if(hud == null)
            {
                hud = UIManager.Instance.AddHUDElement<CastingHUD>();
            }
            hud.Show();
            hud.SetSkillName(data.SelectedSkill.SkillDefinition.SkillName);
        }else
        {
            m_finishCasting = true;
            m_character.Entity.CharacterCanvas.ShowBubbleText("I don't have a target!!", 1f);
        }        
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
        UIManager.Instance.GetHUDElement<CastingHUD>().Hide();
    }
}

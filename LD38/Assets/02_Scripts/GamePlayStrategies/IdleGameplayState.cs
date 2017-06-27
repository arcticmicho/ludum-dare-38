using GameModules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleGameplayState : GameplayState<Wizard, TransitionData>
{
    private bool m_transitionToActiveState;
    private Vector3 m_initialDragPosition;

    public override void OnEnter(TransitionData data)
    {
        base.OnEnter(data);
        m_transitionToActiveState = false;
        InputManager.Instance.OnDragEvent += OnDrag;
        Debug.Log("Entering Idle Gameplay State");
        UIPartyManager.Instance.RequestView<IdleView>();
        UIPartyManager.Instance.GetView<IdleView>().CreateSkillsBook(m_character.SkillTree);
        m_character.Entity.PlayIdleAnimation();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override GameplayTransition EvaluateTransition()
    {
        if(!GameManager.Instance.ActiveGameSession.HasTarget)
        {
            return GameplayTransition.None;
        }

        if(m_transitionToActiveState)
        {
            TransitionData data = new TransitionData(m_initialDragPosition);
            return new GameplayTransition(typeof(ActiveGameplayState), false, data);
        }
        return base.EvaluateTransition();
    }

    private void OnDrag(DragStatus status, Vector3 position, Vector3 lastPosition)
    {
        switch (status)
        {
            case DragStatus.Begin:
                m_transitionToActiveState = true;
                m_initialDragPosition = position;
                break;
        }
    }

    public override void OnExtit()
    {
        base.OnExtit();
        InputManager.Instance.OnDragEvent -= OnDrag;
    }
}

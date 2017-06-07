using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleGameplayState : GameplayState<Wizard, TransitionData>
{

    private bool m_transitionToActiveState;

    public override void OnEnter(TransitionData data)
    {
        base.OnEnter(data);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIdleState : GenericState<GameStateManager, GameStateData>
{

    public override void OnEnter(GameStateData data)
    {
        base.OnEnter(data);
        LoadResources();
        UIPartyManager.Instance.RequestView<MainMenuView>();
    }

    public override StateTransition<GameStateData> EvaluateTransition()
    {
        return base.EvaluateTransition();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void OnExtit()
    {
        base.OnExtit();
        UIPartyManager.Instance.UnloadViews(ResourceManager.Instance.UIResources.MainMenuViewContainer);
    }

    private void LoadResources()
    {
        UIPartyManager.Instance.LoadViews(ResourceManager.Instance.UIResources.MainMenuViewContainer);
    }
}

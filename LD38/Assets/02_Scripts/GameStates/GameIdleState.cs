using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameModules;


public class GameIdleState : GenericState<GameStateManager, GameStateData>
{
    private GameStateData m_sessionData;

    public override void OnEnter(GameStateData data)
    {
        base.OnEnter(data);
        m_sessionData = null;
        LoadResources();
        UIManager.Instance.RequestPopup<MainMenuPopup>().Show();
    }

    public override StateTransition<GameStateData> EvaluateTransition()
    {
        if(m_sessionData != null)
        {
            return new StateTransition<GameStateData>(typeof(GameSessionState), false, m_sessionData);
        }
        return base.EvaluateTransition();
    }

    public void StartGameSession(GameStateData data)
    {
        m_sessionData = data;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void OnExtit()
    {
        base.OnExtit();
       // UIPartyManager.Instance.UnloadViews(ResourceManager.Instance.UIResources.MainMenuViewContainer);
    }

    private void LoadResources()
    {
       // UIPartyManager.Instance.LoadViews(ResourceManager.Instance.UIResources.MainMenuViewContainer);
    }
}

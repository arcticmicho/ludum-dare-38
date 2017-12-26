using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionState : GenericState<GameStateManager, GameStateData>
{
    private GameSession m_currentGameSession;
    public GameSession ActiveGameSession
    {
        get { return m_currentGameSession; }
    }

    public override void OnEnter(GameStateData data)
    {
        base.OnEnter(data);
        m_currentGameSession = new GameSession(data);
        LoadResources();
        m_currentGameSession.StartSession();
    }

    public override void UpdateState()
    {
        if (m_currentGameSession != null && !m_currentGameSession.SessionFinished)
        {
            if (m_currentGameSession.ProcessSession(TimeManager.Instance.DeltaTime))
            {
                m_currentGameSession.EndSession();
            }
        }
    }

    public override StateTransition<GameStateData> EvaluateTransition()
    {
        if(m_currentGameSession.SessionClosed)
        {
            return new StateTransition<GameStateData>(typeof(GameIdleState), false, null);
        }
        return base.EvaluateTransition();
    }

    public override void OnExtit()
    {
        base.OnExtit();
        UnloadResources();
    }

    private void LoadResources()
    {
        // UIPartyManager.Instance.LoadViews(ResourceManager.Instance.UIResources.GameSessionViewContainer);
    }

    private void UnloadResources()
    {
        // UIPartyManager.Instance.UnloadViews(ResourceManager.Instance.UIResources.GameSessionViewContainer);
    }
}

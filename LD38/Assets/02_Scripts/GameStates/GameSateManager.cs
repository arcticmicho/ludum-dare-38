using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoSingleton<GameStateManager>
{
    private GenericStateMachine<GameStateManager, GameStateData> m_stateMachine;

    public void Initialize()
    {
        m_stateMachine = new GenericStateMachine<GameStateManager, GameStateData>(this);
        m_stateMachine.StartGameplayStateMachine<GameIdleState>();
    }

    public void RequestGameSession()
    {
        if (m_stateMachine.CurrentState is GameIdleState)
        {
            GameIdleState idleState = (GameIdleState)m_stateMachine.CurrentState;
            idleState.StartGameSession(new GameStateData(GameManager.Instance.GetSelectedRoomData(), GameManager.Instance.GetSelectedWizard()));
        }
    }

    internal void StartGameSession()
    {
        RequestGameSession();
    }

    private void Update()
    {
        if(m_stateMachine != null)
        {
            m_stateMachine.UpdateSM();
        }
    }
}

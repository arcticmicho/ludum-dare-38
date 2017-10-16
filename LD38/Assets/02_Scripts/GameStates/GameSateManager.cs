using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoSingleton<GameStateManager>
{
    private GameIdleState m_idleState;
    private GameSessionState m_sessionState;

    private GenericStateMachine<GameStateManager, GameStateData> m_stateMachine;

    public void Initialize()
    {
        m_stateMachine = new GenericStateMachine<GameStateManager, GameStateData>(this);
        m_stateMachine.StartGameplayStateMachine<GameIdleState>();
    }



}

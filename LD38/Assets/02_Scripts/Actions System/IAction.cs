using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameAction
{
    protected GameSession m_contextSession;

    public GameAction(GameSession contextSession)
    {
        m_contextSession = contextSession;
    }

    public abstract DateTime ActionTime { get; }
    public abstract void StartAction();
    public abstract void EndAction();
    public abstract bool ProcessAction(float deltaTime);
}

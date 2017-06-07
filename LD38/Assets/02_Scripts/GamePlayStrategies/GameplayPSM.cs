using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayPSM
{
    private GameplayState<Wizard, TransitionData> m_currentState;
    private Dictionary<Type, GameplayState<Wizard, TransitionData>> m_states;

    private Wizard m_psmOwner;

    public GameplayPSM(Wizard owner)
    {
        m_psmOwner = owner;
        m_states = new Dictionary<Type, GameplayState<Wizard, TransitionData>>();
    }

    public void UpdatePSM()
    {
        if(m_currentState != null)
        {
            GameplayTransition transition = m_currentState.EvaluateTransition();
            if(!transition.IsNullTransition)
            {
                ChangeState(transition);
            }
            m_currentState.UpdateState();
        }
    }

    private void ChangeState(GameplayTransition transition)
    {
        GameplayState<Wizard, TransitionData> newState = GetCachedState(transition.StateType);
        if(m_currentState != null)
        {
            m_currentState.OnExtit();
        }
        m_currentState = newState;
        m_currentState.OnEnter(transition.TransitionData);
    }

    private GameplayState<Wizard, TransitionData> GetCachedState(Type stateType)
    {
        GameplayState<Wizard, TransitionData> newState;
        if (m_states.TryGetValue(stateType, out newState))
        {
            return newState;
        }else
        {
            newState = (GameplayState < Wizard, TransitionData > )Activator.CreateInstance(stateType);
            newState.Character = m_psmOwner;
            m_states.Add(stateType, newState);
            return newState;
        }
    }
}

public class GameplayTransition
{
    private Type m_stateType;
    public Type StateType
    {
        get { return m_stateType; }
    }
    private bool m_nullTransition;
    public bool IsNullTransition
    {
        get { return m_nullTransition; }
    }

    private TransitionData m_transitionData;
    public TransitionData TransitionData
    {
        get { return m_transitionData; }
    }

    public static GameplayTransition None = new GameplayTransition(null, true);

    public GameplayTransition(Type stateType, bool nullTransition, TransitionData transitionData = null)
    {
        m_stateType = stateType;
        m_nullTransition = nullTransition;
        m_transitionData = transitionData;
    }

    public static GameplayTransition Transition<T>() where T: GameplayState<Wizard,TransitionData>
    {
        return new GameplayTransition(typeof(T), false);
    }
}

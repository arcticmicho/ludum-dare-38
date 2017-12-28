using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericStateMachine<OwnerType, Data> where Data : class
{
    private GenericState<OwnerType, Data> m_currentState;
    private Dictionary<Type, GenericState<OwnerType, Data>> m_states;

    private OwnerType m_psmOwner;

    public GenericState<OwnerType,Data> CurrentState
    {
        get { return m_currentState; }
    }

    public GenericStateMachine(OwnerType owner)
    {
        m_psmOwner = owner;
        m_states = new Dictionary<Type, GenericState<OwnerType, Data>>();
    }

    public void StartGameplayStateMachine<T>() where T: GenericState<OwnerType, Data>
    {
        ChangeState(new StateTransition<Data>(typeof(T), false, null));
    }

    public void UpdateSM()
    {
        if(m_currentState != null)
        {
            StateTransition<Data> transition = m_currentState.EvaluateTransition();
            if(!transition.IsNullTransition)
            {
                ChangeState(transition);
            }
            if(m_currentState.CanUpdate())
            {
                m_currentState.UpdateState();
            }            
        }
    }

    private void ChangeState(StateTransition<Data> transition)
    {
        GenericState<OwnerType, Data> newState = GetCachedState(transition.StateType);
        if(m_currentState != null)
        {
            m_currentState.OnExtit();
        }
        m_currentState = newState;
        m_currentState.OnEnter(transition.TransitionData);
    }

    private GenericState<OwnerType, Data> GetCachedState(Type stateType)
    {
        GenericState<OwnerType, Data> newState;
        if (m_states.TryGetValue(stateType, out newState))
        {
            return newState;
        }else
        {
            newState = (GenericState <OwnerType, Data> )Activator.CreateInstance(stateType);
            newState.Character = m_psmOwner;
            m_states.Add(stateType, newState);
            return newState;
        }
    }
}

public class StateTransition<Data> where Data : class
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

    private Data m_transitionData;
    public Data TransitionData
    {
        get { return m_transitionData; }
    }

    public static StateTransition<Data> None = new StateTransition<Data>(null, true);

    public StateTransition(Type stateType, bool nullTransition, Data transitionData = null)
    {
        m_stateType = stateType;
        m_nullTransition = nullTransition;
        m_transitionData = transitionData;
    }

    public StateTransition(Type stateType, Data transitionData)
    {
        m_stateType = stateType;
        m_nullTransition = false;
        m_transitionData = transitionData;
    }

    public static StateTransition<Data> Transition<T,Actor>() where T: GenericState<Actor, Data> where Actor: Character
    {
        return new StateTransition<Data>(typeof(T), false);
    }
}

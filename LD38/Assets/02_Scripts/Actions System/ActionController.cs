using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController
{
    private PriorityQueue<long,GameAction> m_actions;
    private List<GameAction> m_activeActions;

	public void Initialize()
    {
        m_actions = new PriorityQueue<long, GameAction>(new MinComparer());
        m_activeActions = new List<GameAction>();
	}
	
	public void UpdateActions()
    {
        //One action per frame?
		/*while*/if(!m_actions.IsEmpty && m_actions.Peek().Value.ActionTime <= TimeManager.Instance.CurrentTime)
        {
            GameAction newAction = m_actions.Dequeue().Value;
            newAction.StartAction();
            m_activeActions.Add(newAction);
        }

        for(int i=0; i< m_activeActions.Count; i++)
        {
            if(m_activeActions[i].ProcessAction(TimeManager.Instance.DeltaTime))
            {
                m_activeActions[i].EndAction();
                m_activeActions.Remove(m_activeActions[i]);
                --i;
            }
        }
	}

    public void EnqueueAction(GameAction newAction)
    {
        m_actions.Enqueue(newAction.ActionTime.Ticks, newAction);
    }

    public void ClearAction()
    {
        m_actions.Clear();
        m_activeActions.Clear();
    }
}

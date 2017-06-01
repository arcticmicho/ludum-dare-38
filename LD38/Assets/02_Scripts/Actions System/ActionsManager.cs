using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsManager : MonoSingleton<ActionsManager>
{
    private PriorityQueue<float,IAction> m_actions;
    private List<IAction> m_activeActions;

	// Use this for initialization
	void Start ()
    {
        m_actions = new PriorityQueue<float,IAction>(new MinComparer());
        m_activeActions = new List<IAction>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //One action per frame?
		/*while*/if(m_actions.Peek().Value != null & m_actions.Peek().Value.ActionTime <= TimeManager.Instance.CurrentTime)
        {
            IAction newAction = m_actions.Dequeue().Value;
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

    public void EnqueueAction(IAction newAction)
    {
        m_actions.Enqueue((float) newAction.ActionTime.Ticks, newAction);
    }
}

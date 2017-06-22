using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UIPartyManager : UIManagerParty.MonoSingleton<UIPartyManager>
{
    private Stack<UIView> m_viewStack;
    private Queue<ViewRequest> m_requests;
    private ViewRequest m_currentRequest;

    [SerializeField]
    private List<UIView> m_views;

    [SerializeField]
    private List<UIElement> m_elements;

    public UIView ActiveView
    {
        get
        {
            if(m_viewStack.Count > 0)
            {
                return m_viewStack.Peek();
            }
            return null;            
        }
    }

    
    public override void Init()
    {
        base.Init();
        m_viewStack = new Stack<UIView>();
        m_requests = new Queue<ViewRequest>();

        foreach(UIView view in GetComponentsInChildren<UIView>(true))
        {
            if(!m_views.Contains(view))
            {
                m_views.Add(view);
                view.Initialize();
                view.gameObject.SetActive(false);
            }
        }

        foreach(UIElement element in GetComponentsInChildren<UIElement>(true))
        {
            if(!m_elements.Contains(element))
            {
                m_elements.Add(element);
                element.gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if(m_currentRequest != null && m_currentRequest.IsFinished)
        {
            if(m_currentRequest.IsFinished)
            {
                m_currentRequest = null;
                DequeueNextViewRequest();
            }
        }else
        {
            DequeueNextViewRequest();
        }
    }

    private void DequeueNextViewRequest()
    {
        if(m_requests.Count > 0)
        {
            m_currentRequest = m_requests.Dequeue();
            m_currentRequest.ProcessRequest();
        }
    }

    public UIView PopView()
    {
        if(m_viewStack.Count > 0)
        {
            return m_viewStack.Pop();
        }
        return null;
    }

    public void PushState(UIView newView)
    {
        m_viewStack.Push(newView);
    }

    public void RequestView<T>() where T: UIView
    {
        RequestView(GetView<T>());
    }    

    public void RequestView(UIView view)
    {
        if(m_currentRequest == null)
        {
            m_currentRequest = new ViewRequest(view);
            m_currentRequest.ProcessRequest();
        }else
        {
            m_requests.Enqueue(new ViewRequest(view));
        }
    }

    public T GetView<T>() where T : UIView
    {
        for(int i=0; i<m_views.Count; i++)
        {
            if(m_views[i] is T)
            {
                return m_views[i] as T;
            }
        }
        return null;
    }

}

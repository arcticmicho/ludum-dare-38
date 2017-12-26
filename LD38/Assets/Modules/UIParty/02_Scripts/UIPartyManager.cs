using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UIPartyManager : MonoSingleton<UIPartyManager>
{
    private Stack<UIView> m_viewStack;
    private Queue<ViewRequest> m_requests;
    private ViewRequest m_currentRequest;

    [SerializeField]
    private Transform m_viewParent;

    [SerializeField]
    private Transform m_elementParent;

    [SerializeField]
    private List<ViewContainer> m_initialContainers;

    //[SerializeField]
    //private List<UIView> m_views;

    [SerializeField]
    private List<UIElement> m_elements;

    private Dictionary<Type, UIView> m_cachedViews;

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


    protected override void OnAwake()
    {
        base.OnAwake();
        m_viewStack = new Stack<UIView>();
        m_requests = new Queue<ViewRequest>();
        m_cachedViews = new Dictionary<Type, UIView>();

        for(int i=0, count=m_initialContainers.Count; i<count; i++)
        {
            LoadViews(m_initialContainers[i]);
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

    public void LoadViews(ViewContainer container)
    {
        List<UIView> views = container.Views;
        for(int i=0, count= views.Count; i<count; i++)
        {
            if(!m_cachedViews.ContainsKey(views[i].GetType()))
            {
                CreateView(views[i]);
            }
        }
    }

    private IEnumerator LoadViewsAsync(ViewContainer container)
    {
        List<UIView> views = container.Views;
        for (int i = 0, count = container.Containers.Count; i < count; i++)
        {
            if (!m_cachedViews.ContainsKey(views[i].GetType()))
            {
                CreateView(views[i]);
                yield return null;
            }
        }
    }

    private UIView CreateView(UIView view)
    {
        UIView newView = Instantiate<UIView>(view);
        newView.transform.SetParent(m_viewParent, false);
        newView.Initialize();
        m_cachedViews.Add(view.GetType(), newView);
        return newView;
    }

    public void UnloadViews(ViewContainer container)
    {
        List<UIView> views = container.Views;
        for (int i = 0, count = container.Containers.Count; i < count; i++)
        {
            if (m_viewStack.Peek() != null && m_viewStack.Peek() != views[i] && m_currentRequest.TargetView != views[i] && m_cachedViews.ContainsKey(views[i].GetType()))
            {
                Destroy(m_cachedViews[views[i].GetType()].gameObject);
            }else
            {
                Debug.LogError("trying to Unload a used View: " + views[i].gameObject.name);
            }
        }
    }

    private IEnumerator UnloadViewsAsync(ViewContainer container)
    {
        List<UIView> views = container.Views;
        for (int i = 0, count = container.Containers.Count; i < count; i++)
        {
            if (m_viewStack.Peek() != null && m_viewStack.Peek() != views[i] && m_currentRequest.TargetView != views[i] && m_cachedViews.ContainsKey(views[i].GetType()))
            {
                Destroy(m_cachedViews[views[i].GetType()].gameObject);
                yield return null;
            }
            else
            {
                Debug.LogError("trying to Unload a used View: " + views[i].gameObject.name);
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
        if (m_cachedViews.ContainsKey(typeof(T)))
        {
            return m_cachedViews[typeof(T)] as T;
        }
        return null;
    }

}

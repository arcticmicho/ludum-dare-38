using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(Animation))]
public class UIView : MonoBehaviour
{
    [SerializeField]
    protected List<ChildElementDescriptor> m_childElements;

    [SerializeField]
    protected UIAnimationSync m_animationConfig;

    private List<UIElement> m_currentElements;

    public void Initialize()
    {
        m_currentElements = new List<UIElement>();
        OnInitialize();
    }

    protected virtual void OnInitialize()
    {
        m_animationConfig.Initialize(GetComponent<Animation>());
    }

    public void RequestView()
    {
        OnViewRequested();
    }


    public void OpenView(System.Action onAnimationFinish)
    {
        gameObject.SetActive(true);
        GetChilds();
        PlayOpenTransitionAnimation(onAnimationFinish);
        OnViewOpen();
    }

    private void PlayOpenTransitionAnimation(System.Action onAnimationFinish)
    {
        UIAnimationSync.UIAnimationTask task;
        if(!m_animationConfig.TryPlayOpenAnimation(out task, onAnimationFinish))
        {
            onAnimationFinish();
        }
    }

    private void GetChilds()
    {
        m_currentElements.Clear();
        for(int i=0; i<m_childElements.Count; i++)
        {
            if(m_childElements[i].ElementParent)
            {
                m_childElements[i].UIElement.transform.SetParent(m_childElements[i].ElementParent, false);
            }else
            {
                m_childElements[i].UIElement.transform.SetParent(transform, false);
                m_childElements[i].UIElement.transform.position = Vector3.zero;
            }
            if(m_childElements[i].PlayOpenAnimation)
            {
                m_childElements[i].UIElement.ElementRequested(this);
            }
            m_currentElements.Add(m_childElements[i].UIElement);
        }
    }

    public void ViewOpened()
    {
        OnViewOpened();
    }    

    public void CloseView(System.Action onAnimationFinish)
    {
        OnViewClose();
        PlayCloseTransitionAnimation(onAnimationFinish);
        for (int i = 0; i < m_childElements.Count; i++)
        {
            if (m_childElements[i].PlayCloseAnimation)
            {
                m_childElements[i].UIElement.PlayCloseAnimation(onAnimationFinish);
            }
        }
    }    

    public void ViewClosed()
    {
        gameObject.SetActive(false);
        OnViewClosed();
    }

    private void PlayCloseTransitionAnimation(System.Action onAnimationFinish)
    {
        UIAnimationSync.UIAnimationTask task;
        if (!m_animationConfig.TryPlayCloseAnimation(out task, onAnimationFinish))
        {
            onAnimationFinish();
        }
    }

    protected virtual void OnViewClosed()
    {

    }

    protected virtual void OnViewRequested()
    {

    }

    protected virtual void OnViewClose()
    {

    }

    protected virtual void OnViewOpened()
    {

    }

    protected virtual void OnViewOpen()
    {

    }

    public void RequestView(UIView view)
    {
        UIPartyManager.Instance.RequestView(view);
    }

    protected virtual void Update()
    {
        m_animationConfig.UpdateAnimationSync(Time.deltaTime);
    }
}

[Serializable]
public class ChildElementDescriptor
{
    [SerializeField]
    private bool m_openOnRequest;
    public bool OpenOnRequest
    {
        get { return m_openOnRequest; }
    }

    [SerializeField]
    private Transform m_elementParent;
    public Transform ElementParent
    {
        get { return m_elementParent; }
    }

    [SerializeField]
    private bool m_playOpenAnimation;
    public bool PlayOpenAnimation
    {
        get { return m_playOpenAnimation; }
    }

    [SerializeField]
    private bool m_playCloseAnimation;
    public bool PlayCloseAnimation
    {
        get { return m_playCloseAnimation; }
    }

    [SerializeField]
    private UIElement m_uiElement;
    public UIElement UIElement
    {
        get
        {
            return m_uiElement;
        }
    }
}

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

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

    }

    public void RequestView()
    {
        OnViewRequested();
    }


    public void OpenView(Action onAnimationFinish)
    {
        gameObject.SetActive(true);
        GetChilds();
        PlayTransitionAnimation(onAnimationFinish);
    }

    private void PlayTransitionAnimation(Action onAnimationFinish)
    {
        
    }

    private void GetChilds()
    {
        m_currentElements.Clear();
        for(int i=0; i<m_childElements.Count; i++)
        {
            if(m_childElements[i].ElementParent)
            {
                m_childElements[i].UIElement.transform.SetParent(m_childElements[i].ElementParent, false);
            }
            m_currentElements.Add(m_childElements[i].UIElement);
        }
    }

    public void ViewOpened()
    {

    }    

    public void Close()
    {

    }    

    public void ViewClose()
    {

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
    private UIElement m_uiElement;
    public UIElement UIElement
    {
        get
        {
            return m_uiElement;
        }
    }
}

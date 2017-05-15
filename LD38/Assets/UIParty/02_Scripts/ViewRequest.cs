using UnityEngine;
using System.Collections;
using System;

public class ViewRequest
{
    private UIView m_targetView;
    private UIView m_currentView;

    private bool m_isFinished;
    public bool IsFinished
    {
        get { return m_isFinished; }
    }

	public ViewRequest(UIView target)
    {
        m_targetView = target;
        m_isFinished = false;
    }

    public void ProcessRequest()
    {
        m_currentView = UIPartyManager.Instance.ActiveView;
        if(m_currentView != null)
        {
            m_currentView.CloseView(OnCurrentViewClosed);
        }else
        {
            OpenNewView();
        }        
    }

    private void OnCurrentViewClosed()
    {
        m_currentView.ViewClosed();
        UIPartyManager.Instance.PopView();
        OpenNewView();
    }

    private void OpenNewView()
    {
        m_targetView.OpenView(OnNewViewOpened);
        UIPartyManager.Instance.PushState(m_targetView);
    }

    private void OnNewViewOpened()
    {
        m_targetView.ViewOpened();
        m_isFinished = true;
    }
}

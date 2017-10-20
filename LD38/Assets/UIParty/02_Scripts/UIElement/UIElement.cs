using UnityEngine;
using System.Collections;
using System;

public class UIElement : MonoBehaviour
{
    [SerializeField]
    private UIAnimationSync m_animationSync;

    private UIView m_currentOwner;

    public void InitiElement()
    {
        Animation animC = GetComponent<Animation>();
        if(animC != null)
        {
            m_animationSync.Initialize(animC);
        }
    }

    public void ElementRequested(UIView newOwner, System.Action callback = null, bool playAnimation = false)
    {
        m_currentOwner = newOwner;
        if(playAnimation && m_animationSync != null)
        {
            UIAnimationSync.UIAnimationTask task;
            if(!m_animationSync.TryPlayOpenAnimation(out task, null) && callback != null)
            {
                callback();   
            }
        }
    }

    public void PlayCloseAnimation(System.Action callback = null)
    {
        if(m_animationSync != null)
        {
            UIAnimationSync.UIAnimationTask task;
            if(!m_animationSync.TryPlayCloseAnimation(out task, callback) && callback != null)
            {
                callback();
            }
        }
    }
}

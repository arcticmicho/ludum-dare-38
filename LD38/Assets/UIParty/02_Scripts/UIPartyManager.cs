using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIPartyManager : UIManagerParty.MonoSingleton<UIPartyManager>
{
    private Stack m_viewsStack;

    [SerializeField]
    private List<UIView> m_views;

    [SerializeField]
    private List<UIElement> m_elements;

    public override void Init()
    {
        base.Init();
        foreach(UIView view in GetComponentsInChildren<UIView>())
        {
            if(!m_views.Contains(view))
            {
                m_views.Add(view);
            }
        }

        foreach(UIElement element in GetComponentsInChildren<UIElement>())
        {
            if(!m_elements.Contains(element))
            {
                m_elements.Add(element);
            }
        }
    }
}

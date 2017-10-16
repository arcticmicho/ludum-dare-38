using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UIResources
{

    [SerializeField]
    private ViewContainer m_mainMenuViewContainer;
    public ViewContainer MainMenuViewContainer
    {
        get { return m_mainMenuViewContainer; }
    }

    [SerializeField]
    private ViewContainer m_gameSessionViewContainer;
    public ViewContainer GameSessionViewContainer
    {
        get { return m_gameSessionViewContainer; }
    }
}

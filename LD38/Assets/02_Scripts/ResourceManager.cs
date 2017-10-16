using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoSingleton<ResourceManager>
{
    [SerializeField]
    private UIResources m_uiResources;
    public UIResources UIResources
    {
        get { return m_uiResources; }
    }
	
}

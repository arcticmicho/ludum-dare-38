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

    [SerializeField]
    private SkillResources m_skillResources;
    public SkillResources SkillRecources
    {
        get { return m_skillResources; }
    }

    [SerializeField]
    private DamageTypeResources m_damageTypeResources;
    public DamageTypeResources DamageTypeResources
    {
        get { return m_damageTypeResources; }
    }

}

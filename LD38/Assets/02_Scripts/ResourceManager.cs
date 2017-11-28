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

    [SerializeField]
    private CharacterTemplateResources m_templateResources;
    public CharacterTemplateResources CharacterTemplates
    {
        get { return m_templateResources; }
    }

    [SerializeField]
    private RoomTemplateResources m_roomTemplates;
    public RoomTemplateResources RoomTemplateResources
    {
        get { return m_roomTemplates; }
    }

    public void Initialize()
    {
        m_skillResources.Initialize();
        m_damageTypeResources.Initialize();
        m_templateResources.Initialize();
        m_roomTemplates.Initialize();
    }

}

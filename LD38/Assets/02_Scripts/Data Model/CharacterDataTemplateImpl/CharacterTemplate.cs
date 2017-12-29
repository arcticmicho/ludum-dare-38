using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data Template", menuName = "GamePlay/Characters/Enemy")]
public class CharacterTemplate : ScriptableObject, ICharacterData
{
    [Header("Info")]
    [SerializeField]
    private string m_templateId;

    [SerializeField]
    private string m_name;

    [Header("Info - Not Imported")]
    [SerializeField]
    private CharacterEntity m_prefab;

    [Header("Character")]
    [SerializeField]
    private float m_hp;

    [SerializeField]
    private float m_HpMultiplier = 1.01f;

    [SerializeField]
    private DamageTable m_resistanceTable;

    [SerializeField]
    private List<SkillData> m_skills;

    [SerializeField]
    private float m_movementSpeed = 5f;

    [SerializeField]
    private string m_entityTemplateId;
   
    [SerializeField]
    private EquippableItemTemplate m_weaponTemplate;
 
    #region Get/Set
    public string TemplateId
    {
        get { return m_templateId; }
        set { }
    }

    public string Name
    {
        get { return m_name; }
        set { }
    }

    public CharacterEntity Prefab
    {
        get { return m_prefab; }
        set { }
    }

    public float Hp
    {
        get { return m_hp; }
        set { }
    }

    public float HpMultiplier
    {
        get { return m_HpMultiplier; }
        set { }
    }

    public DamageTable ResistanceTable
    {
        get { return m_resistanceTable; }
        set { }
    }

    public List<SkillData> Skills
    {
        get { return m_skills; }
        set { }
    }

    public float MovementSpeed
    {
        get { return m_movementSpeed; }
        set { }
    }


    public string EntityTemplateId
    {
        get { return m_entityTemplateId; }
        set { }
    }

    public EquippableItemTemplate WeaponTemplate
    {
        get { return m_weaponTemplate; }
        set { }
    }

    #endregion

    #region Editor Functions and export
   
    public virtual void Editor_SetExportData(Dictionary<string, object> exportData)
    {
        m_templateId    = SerializeHelper.GetValue<string>(exportData, "Id");
        m_name          = SerializeHelper.GetValue<string>(exportData, "Name");
        m_hp            = SerializeHelper.GetValue<int>(exportData, "BaseHp");
        m_HpMultiplier  = SerializeHelper.GetValue<int>(exportData, "HpMultiplier");
    }
    #endregion

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTemplate : ScriptableObject, ICharacterData
{
    [SerializeField]
    private string m_templateId;
    public string TemplateId
    {
        get { return m_templateId; }
    }

    [SerializeField]
    private CharacterEntity m_prefab;
    public CharacterEntity Prefab
    {
        get { return m_prefab; }
    }

    [SerializeField]
    private float m_hp;
    public float HealthPoints
    {
        get { return m_hp; }
    }

    [SerializeField]
    private DamageTable m_resistanceTable;
    public DamageTable ResistanceTable
    {
        get { return m_resistanceTable; }
    }

    [SerializeField]
    private List<SkillDefinition> m_skills;
    public List<SkillDefinition> Skills
    {
        get { return m_skills; }
    }

    [SerializeField]
    private float m_movementSpeed = 5f;
    public float MovementSpeed
    {
        get { return m_movementSpeed; }
    }

    [SerializeField]
    private string m_name;
    public string Name
    {
        get
        {
            return m_name;
        }
    }
}

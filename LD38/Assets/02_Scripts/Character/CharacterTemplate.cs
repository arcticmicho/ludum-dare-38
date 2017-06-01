using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTemplate : ScriptableObject
{
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
    private List<SkillDefinition> m_skills;
    public List<SkillDefinition> Skills
    {
        get { return m_skills; }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : ICharacter
{
    private SkillTree m_skillTree;
    private float m_healthPoints;

    private CharacterEntity m_entity;
    public CharacterEntity Entity
    {
        get { return m_entity; }
    }

    private CharacterTemplate m_template;

    private bool m_isDead;

    public Wizard(CharacterTemplate template)
    {
        SetTemplateData(template);
        m_template = template;
        InstantiateCharacterEntity();
    }

    public void InstantiateCharacterEntity(bool force = false)
    {
        if (m_entity == null || force)
        {
            m_entity = GameObject.Instantiate<CharacterEntity>(m_template.Prefab);
            m_entity.transform.SetParent(CharactersManager.Instance.transform);
        }
    }

    private void SetTemplateData(CharacterTemplate template)
    {
        m_template = template;
        m_healthPoints = template.HealthPoints;
        m_skillTree = new SkillTree(template.Skills);
        m_skillTree.InitializeSkillTree(template.Skills);
    }

    public float HealthPoints
    {
        get
        {
            return m_healthPoints;
        }
    }

    public bool IsDeath
    {
        get
        {
            return m_isDead;
        }
    }

    public void ApplyDamage(float damage)
    {
        m_healthPoints -= damage;
        if(m_healthPoints <= 0)
        {
            m_isDead = true;
        }
    }

    public void KillCharacter()
    {

    }

    public bool TryProcessPattern(Vector2[] patternPoints, out SkillDefinition skillDef)
    {
        if(m_skillTree != null)
        {
            if(m_skillTree.SearchForPattern(patternPoints, out skillDef, true))
            {
                return true;
            }
        }
        skillDef = null;
        return false;
    }

    public void ResetSkillTree()
    {
        m_skillTree.ResetSearch();
    }
}

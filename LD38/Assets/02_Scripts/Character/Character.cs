using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    private CharacterTemplate m_template;
    private bool m_isDead;
    private float m_healthPoints;

    private BaseRoomPoint m_currentPoint;

    private CharacterEntity m_entity;
    public CharacterEntity Entity
    {
        get { return m_entity; }
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

    public float MovementSpeed
    {
        get { return m_template.MovementSpeed; }
    }

    public DamageTable ResistanceTable
    {
        get { return m_template.ResistanceTable; }
    }

    public BaseRoomPoint CurrentPoint
    {
        get { return m_currentPoint; }
        set { m_currentPoint = value; }
    }

    public Character(CharacterTemplate template)
    {
        SetTemplateData(template);
        InstantiateCharacterEntity();
    }

    protected virtual void SetTemplateData(CharacterTemplate template)
    {
        m_template = template;
        m_healthPoints = template.HealthPoints;
    }

    public virtual void InstantiateCharacterEntity(bool force = false)
    {
        if (m_entity == null || force)
        {
            m_entity = GameObject.Instantiate<CharacterEntity>(m_template.Prefab);
            m_entity.transform.SetParent(CharactersManager.Instance.transform);
        }
    }

    public virtual void ApplyDamage(DamageTable damageTable)
    {
        DamageType prominentType;
        float damageValue = damageTable.GetTotalDamage(out prominentType);
        m_healthPoints -= damageValue;
        if (m_healthPoints <= 0)
        {
            m_isDead = true;
            KillCharacter();
        }

        m_entity.CharacterCanvas.AddFlotingText("-" + damageValue);
        m_entity.PlayHitAnimation();
    }

    public SkillDefinition GetRandomAbility()
    {
        return m_template.Skills[UnityEngine.Random.Range(0, m_template.Skills.Count)];
    }

    public virtual void KillCharacter()
    {
        m_entity.PlayDeadAnimation();
    }
}

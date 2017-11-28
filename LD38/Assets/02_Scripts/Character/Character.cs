using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    protected GameSession m_contextSession;

    protected ICharacterData m_data;
    public ICharacterData Data
    {
        get { return m_data; }
    }
    private bool m_isDead;
    private float m_healthPoints;

    private BaseRoomPoint m_currentPoint;

    private CharacterEntity m_entity;
    public CharacterEntity Entity
    {
        get { return m_entity; }
    }

    public GameSession Session
    {
        get { return m_contextSession; }
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
        get { return m_data.MovementSpeed; }
    }

    public DamageTable ResistanceTable
    {
        get { return m_data.ResistanceTable; }
    }

    public BaseRoomPoint CurrentPoint
    {
        get { return m_currentPoint; }
        set { m_currentPoint = value; }
    }

    public Character(GameSession session, ICharacterData data, CharacterEntity entityTemplate)
    {
        m_contextSession = session;
        SetTemplateData(data);
        InstantiateCharacterEntity(entityTemplate);
    }

    protected virtual void SetTemplateData(ICharacterData data)
    {
        m_data = data;
        m_healthPoints = data.HealthPoints;
    }

    public virtual void InstantiateCharacterEntity(CharacterEntity entityTemplate, bool force = false)
    {
        if (m_entity == null || force)
        {
            m_entity = GameObject.Instantiate<CharacterEntity>(entityTemplate);
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
        return m_data.Skills[UnityEngine.Random.Range(0, m_data.Skills.Count)];
    }

    public virtual void KillCharacter()
    {
        m_entity.PlayDeadAnimation();
    }
}

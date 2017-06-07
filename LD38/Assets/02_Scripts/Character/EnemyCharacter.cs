using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : ICharacter
{
    private float m_healthPoints;
    private bool m_isDead;
    private CharacterTemplate m_characterTemplate;

    private CharacterEntity m_entity;
    public CharacterEntity Entity
    {
        get { return m_entity; }
    }

    private CharacterTemplate m_template;

    public EnemyCharacter(CharacterTemplate template)
    {
        SetTemplateData(template);
        InstantiateCharacterEntity();
    }

    private void SetTemplateData(CharacterTemplate template)
    {
        m_template = template;
        m_characterTemplate = template;
        m_healthPoints = template.HealthPoints;
    }

    public void InstantiateCharacterEntity(bool force = false)
    {
        if(m_entity == null || force)
        {
            m_entity = GameObject.Instantiate<CharacterEntity>(m_characterTemplate.Prefab);
            m_entity.transform.SetParent(CharactersManager.Instance.transform);
        }
    }

    public void ApplyDamage(float damage)
    {

    }

    public float HealthPoints
    {
        get
        {
            return HealthPoints;
        }
    }

    public bool IsDeath
    {
        get
        {
            return m_isDead;
        }
    }
}

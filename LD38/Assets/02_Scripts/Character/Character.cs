using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character
{
    protected GameSession m_contextSession;

    protected ICharacterData m_data;

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

    public bool IsStunned
    {
        get
        {
            return (((int)ECharacterStatus.Stunned & GetAllStatus()) > 0);
        }
    }

    private List<CharacterStatus> m_status = new List<CharacterStatus>();
    

    public EquippableItemTemplate Weapon
    {
        get { return m_data.WeaponTemplate; }
    }

    protected StatModifier<float> m_movementSpeedModifier;
    public StatModifier<float> MovementSpeedModifier
    {
        get { return m_movementSpeedModifier; }
    }

    public float MovementSpeed
    {
        get { return m_data.MovementSpeed * (m_movementSpeedModifier.HasModifiers ? m_movementSpeedModifier.TotalModifier : 1); }
    }

    protected StatModifier<DamageTable> m_resistanceTableModifier;
    public StatModifier<DamageTable> ResistanceTableModifier
    {
        get { return m_resistanceTableModifier; }
    }

    public DamageTable ResistanceTable
    {
        get { return m_data.ResistanceTable - (m_data.ResistanceTable * m_resistanceTableModifier.TotalModifier); }
    }

    public BaseRoomPoint CurrentPoint
    {
        get { return m_currentPoint; }
        set { m_currentPoint = value; }
    }

    public abstract Character CurrentTarget { get; }

    public Character(GameSession session, ICharacterData data, CharacterEntity entityTemplate)
    {
        m_contextSession = session;
        SetTemplateData(data);
        InstantiateCharacterEntity(entityTemplate);
        InitializeModifiers();
    }

    protected virtual void SetTemplateData(ICharacterData data)
    {
        m_data = data;
        m_healthPoints = data.HealthPoints;
    }

    private void InitializeModifiers()
    {        
        m_movementSpeedModifier = new StatModifier<float>((mod1, mod2) => { return mod1 + mod2; }, 0f);
        m_resistanceTableModifier = new StatModifier<DamageTable>((mod1, mod2) => { return mod1 + mod2;}, new DamageTable());
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

        m_entity.CharacterCanvas.RequestFlotingText("-" + damageValue);
        m_entity.PlayHitAnimation();
    }

    public virtual void ApplyHeal(float healAmount)
    {
        m_healthPoints = Mathf.Min(m_data.HealthPoints, m_healthPoints + healAmount);
        m_entity.CharacterCanvas.RequestFlotingText(healAmount.ToString());
    }

    public SkillData GetRandomAbility()
    {
        return m_data.Skills[UnityEngine.Random.Range(0, m_data.Skills.Count)];
    }

    public virtual void KillCharacter()
    {
        m_entity.PlayDeadAnimation();
    }

    public bool AddStatus(CharacterStatus newStatus)
    {
        CharacterStatus currentStatus = m_status.Find((x) => x.GetStatusType() == newStatus.GetStatusType());
        //replace the currentStatus with the old one.
        if(currentStatus !=  null)
        {
            if(newStatus.CompareStatus(currentStatus))
            {
                currentStatus.Invalidate();
                m_status.Remove(currentStatus);
                m_status.Add(newStatus);
                return true;
            }
            return false;           
        }else
        {
            m_status.Add(newStatus);
            return true;
        }
    }

    public void RemoveStatus(CharacterStatus status)
    {
        if(m_status.Contains(status))
        {
            m_status.Remove(status);
        }
    }

    public bool HasStatus(ECharacterStatus status)
    {
        return m_status.Find((x) => x.GetStatusType() == status) != null;
    }

    private int GetAllStatus()
    {
        int status = 0;
        for(int i=0, count=m_status.Count; i<count; i++)
        {
            status += (int)m_status[i].GetStatusType();
        }
        return status;
    }
}

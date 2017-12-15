using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ECharacterStatus
{
    Healing = 1,
    DamageOverTime = 2,
    Frozen = 4,
    Stop = 8,
    ResistanceUp = 16,
    ResistanceDown = 32,


    Stunned = Stop,
}

public abstract class CharacterStatus
{
    protected float m_statusDuration;
    protected float m_numberOfTicks;

    protected bool m_isValid;
    /// <summary>
    /// Who is affected by the Status
    /// </summary>
    protected Character m_owner;

    /// <summary>
    /// Who throws the Status
    /// </summary>
    protected Character m_caster;
    
    public abstract string StatusName { get; }

    public abstract string Description { get; }

    public abstract Image StatusImage { get; }

    public CharacterStatus(float statusDuration, int numberOfTicks, Character owner, Character caster)
    {
        m_statusDuration = statusDuration;
        m_numberOfTicks = numberOfTicks;
        m_owner = owner;
        m_caster = caster;
        m_isValid = true;
    }

    public abstract void ApplyStatus();

    public abstract void RemoveStatus();

    /// <summary>
    /// Method to calculate the status power. It returns a float value that represents the power of the status.
    /// </summary>
    /// <returns></returns>
    protected abstract float GetStatusPower();

    public abstract ECharacterStatus GetStatusType();

    /// <summary>
    /// Compare this Status with another. If the status are from different status type, it returns false.
    /// Returns true if the current status power is equals or higher than the otherStatus.
    /// </summary>
    /// <param name="otherStatus"></param>
    /// <returns></returns>
    public bool CompareStatus(CharacterStatus otherStatus)
    {
        if(GetStatusType() != otherStatus.GetStatusType())
        {
            return false;
        }

        return GetStatusPower() >= otherStatus.GetStatusPower();
    }

    public void Invalidate()
    {
        m_isValid = false;
    }
}

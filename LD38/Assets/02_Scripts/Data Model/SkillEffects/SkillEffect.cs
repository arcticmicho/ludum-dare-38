using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SkillEffect : ScriptableObject
{
    [SerializeField]
    protected string m_skillEffectName;
    public string EffectName
    {
        get { return m_skillEffectName; }
    }

    [SerializeField]
    protected string m_skillEffectDescription;
    public string SkillEffectDesc
    {
        get { return m_skillEffectDescription; }
    }

    [SerializeField]
    protected Sprite m_skillEffectImage;
    public Sprite EffectImage
    {
        get { return m_skillEffectImage; }
    }

    [SerializeField]
    protected float m_skillEffectDuration;
    public float EffectDuration
    {
        get { return m_skillEffectDuration; }
    }

    [SerializeField]
    protected int m_skillNumberOfTicks;
    public int NumberOfTicks
    {
        get { return m_skillNumberOfTicks; }
    }

    [SerializeField]
    protected bool m_applyEffectAtStart;
    public bool ApplyEffectAtStart
    {
        get
        {
            return m_applyEffectAtStart;
        }
       
    }

    public abstract CharacterStatus GenerateStatus(Character caster, Character target);
}

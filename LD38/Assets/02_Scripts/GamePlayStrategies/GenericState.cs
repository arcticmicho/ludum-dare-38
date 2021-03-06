﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericState<Actor, Data> where Data : class
{
    protected Actor m_character;
    public Actor Character
    {
        set { m_character = value; }
    }
    protected Data m_data;

    public virtual void OnEnter(Data data)
    {
        m_data = data;
    }

    public virtual StateTransition<Data> EvaluateTransition()
    {
        return StateTransition<Data>.None;
    }

    public virtual void UpdateState()
    {

    }

    public virtual void OnExtit()
    {

    }

    public virtual bool CanUpdate()
    {
        return true;
    }
}

public class TransitionData
{
    private SkillData m_selectedSkill;
    public SkillData SelectedSkill
    {
        get { return m_selectedSkill; }
    }

    public TransitionData(SkillData skillData)
    {
        m_selectedSkill = skillData;
    }
}

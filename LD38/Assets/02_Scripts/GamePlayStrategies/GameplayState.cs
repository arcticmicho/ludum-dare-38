using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState<Actor, Data> 
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

    public virtual GameplayTransition EvaluateTransition()
    {
        return GameplayTransition.None;
    }

    public virtual void UpdateState()
    {

    }

    public virtual void OnExtit()
    {

    }
}

public class TransitionData
{
    private bool m_hasInputData;
    public bool HasInputData
    {
        get { return m_hasInputData; }
    }

    private Vector3 m_initDrag;
    public Vector3 InitialDrag
    {
        get { return m_initDrag; }
    }

    private SkillDefinition m_selectedSkill;
    public SkillDefinition SelectedSkill
    {
        get { return m_selectedSkill; }
    }

    public TransitionData()
    {
        m_hasInputData = false;
    }

    public TransitionData(Vector3 initialDrag)
    {
        m_hasInputData = true;
        m_initDrag = initialDrag;
    }

    public TransitionData(SkillDefinition skillDef)
    {
        m_hasInputData = false;
        m_selectedSkill = skillDef;
    }
}

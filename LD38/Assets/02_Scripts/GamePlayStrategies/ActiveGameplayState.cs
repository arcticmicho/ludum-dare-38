using GameModules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActiveGameplayState : GameplayState<Wizard, TransitionData>
{
    private bool m_detectingPattern;
    private float m_detectingTime;

    private bool m_finishActiveGameplay;

    private SkillDefinition m_skillToCast;

    private List<Vector3> m_dragPositions = new List<Vector3>();

    public override void OnEnter(TransitionData data)
    {
        base.OnEnter(data);
        m_dragPositions.Clear();
        InputManager.Instance.OnDragEvent += OnDrag;
        if(data != null && data.HasInputData)
        {
            m_detectingPattern = true;
            m_dragPositions.Add(data.InitialDrag);
        }
    }

    public override GameplayTransition EvaluateTransition()
    {
        if(m_finishActiveGameplay)
        {
            return null;
        }
        return null;
    }

    public override void UpdateState()
    {
        if(!m_detectingPattern)
        {
            if(m_detectingTime >= 2f)
            {
                m_finishActiveGameplay = true;
            }
        }
    }

    public override void OnExtit()
    {
        base.OnExtit();
        InputManager.Instance.OnDragEvent -= OnDrag;
    }

    private void OnDrag(DragStatus status, Vector3 position, Vector3 lastPosition)
    {
        switch (status)
        {
            case DragStatus.Begin:
                m_detectingPattern = true;
                m_dragPositions.Clear();
                m_dragPositions.Add(position);
                break;
            case DragStatus.Moving:
                m_dragPositions.Add(position);
                break;
            case DragStatus.End:
                m_detectingPattern = false;
                ProcessPattern();
                break;
        }
    }

    private void ProcessPattern()
    {
        SkillDefinition newDef;
        if(m_character.TryProcessPattern(ConvertList(m_dragPositions), out newDef))
        {
            m_skillToCast = newDef;
        }
    }

    private Vector2[] ConvertList(List<Vector3> points)
    {
        Vector2[] pointsV2 = new Vector2[points.Count];
        for(int i=0; i<points.Count; i++)
        {
            pointsV2[i] = points[i];
        }
        return pointsV2;
    }
}

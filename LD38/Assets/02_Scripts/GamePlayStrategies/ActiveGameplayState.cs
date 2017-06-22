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
    private TrailRenderer m_currentTrailRenderer;

    private Camera m_mainCamera;
    private Vector3 m_trailRendererDepth;

    private List<Vector3> m_dragPositions = new List<Vector3>();

    public override void OnEnter(TransitionData data)
    {
        m_skillToCast = null;
        m_mainCamera = Camera.main;
        m_trailRendererDepth = new Vector3(0f, 0f, m_mainCamera.transform.position.z + 2);
        base.OnEnter(data);
        m_dragPositions.Clear();
        InputManager.Instance.OnDragEvent += OnDrag;
        m_detectingPattern = false;
        m_finishActiveGameplay = false;
        m_detectingTime = 0f;
        m_character.ResetSkillTree();

        if (data != null && data.HasInputData)
        {
            m_detectingPattern = true;
            m_dragPositions.Add(data.InitialDrag);
            m_currentTrailRenderer = EffectsManager.Instance.RequestTrailRenderer();
            SetTrailPosition(data.InitialDrag);
        }
        Debug.Log("Entering Active Gameplay State");
    }

    public override GameplayTransition EvaluateTransition()
    {
        if(m_finishActiveGameplay)
        {
            if(m_skillToCast != null)
            {
                TransitionData data = new TransitionData(m_skillToCast);
                return new GameplayTransition(typeof(CastingGameplayState), false, data);
            }
            return new GameplayTransition(typeof(IdleGameplayState), false, null);
        }
        return GameplayTransition.None;
    }

    public override void UpdateState()
    {
        if(!m_detectingPattern)
        {
            m_detectingTime += TimeManager.Instance.DeltaTime;
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
                m_currentTrailRenderer = EffectsManager.Instance.RequestTrailRenderer();
                SetTrailPosition(position);
                m_detectingPattern = true;
                m_dragPositions.Clear();
                m_dragPositions.Add(position);
                m_detectingTime = 0;
                break;
            case DragStatus.Moving:
                m_dragPositions.Add(position);
                SetTrailPosition(position);
                break;
            case DragStatus.End:
                m_detectingPattern = false;
                m_currentTrailRenderer = null;
                ProcessPattern();
                break;
        }
    }

    private void SetTrailPosition(Vector3 position)
    {
        Vector3 worldPosition = m_mainCamera.ScreenToWorldPoint(position) - m_trailRendererDepth;
        m_currentTrailRenderer.transform.position = worldPosition;
    }

    private void ProcessPattern()
    {
        SkillDefinition newDef;
        if(m_character.TryProcessPattern(ConvertList(m_dragPositions), out newDef))
        {
            Debug.Log("Ability Found!: " + newDef.SkillName);
            m_skillToCast = newDef;
            m_character.Entity.PlayCastAnimation();
            m_character.Entity.CharacterCanvas.ShowBubbleText(newDef.SkillName + "!!", 3);
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

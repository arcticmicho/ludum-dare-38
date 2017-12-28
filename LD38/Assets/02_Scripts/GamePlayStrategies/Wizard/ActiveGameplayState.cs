using GameModules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GestureRecognizer;

public class ActiveGameplayState : GenericState<Wizard, TransitionData>
{
    private const float k_maxWaitingPatternTime = 2.5f;

    private bool m_detectingPattern;
    private float m_detectingTime;
    private bool m_finishActiveGameplay;
    private bool m_forceFinish;
    private SkillData m_skillToCast;
    private TrailRenderer m_currentTrailRenderer;

    private DetectingHUD m_detectingHud;

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
        m_forceFinish = false;
        m_detectingTime = 0f;

        if (data != null && data.SelectedSkill != null)
        {
            m_detectingPattern = true;
            m_skillToCast = data.SelectedSkill;
            m_currentTrailRenderer = EffectsManager.Instance.RequestTrailRenderer();
        }

        UIManager.Instance.HideHUD();
        m_detectingHud = UIManager.Instance.GetHUDElement<DetectingHUD>();
        if(m_detectingHud == null)
        {
            m_detectingHud = UIManager.Instance.AddHUDElement<DetectingHUD>();
        }
        m_detectingHud.Show();
        m_detectingHud.SetTimerValue(1f);

        Debug.Log("Entering Active Gameplay State");
    }

    public override StateTransition<TransitionData> EvaluateTransition()
    {
        if (m_finishActiveGameplay || m_forceFinish)
        {
            if(m_skillToCast != null)
            {
                TransitionData data = new TransitionData(m_skillToCast);
                return new StateTransition<TransitionData>(typeof(CastingGameplayState), false, data);
            }
            return new StateTransition<TransitionData>(typeof(IdleGameplayState), false, null);
        }
        return StateTransition<TransitionData>.None;
    }

    public override void UpdateState()
    {
        //if(!m_detectingPattern)
        //{
            m_detectingTime += TimeManager.Instance.DeltaTime;
            if(m_detectingTime >= k_maxWaitingPatternTime)
            {
                m_finishActiveGameplay = true;
            }
            m_detectingHud.SetTimerValue(m_detectingTime / k_maxWaitingPatternTime);
        //}
    }

    public override void OnExtit()
    {
        base.OnExtit();
        InputManager.Instance.OnDragEvent -= OnDrag;
        m_detectingHud.Hide();
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
                break;
            case DragStatus.Moving:
                m_dragPositions.Add(position);
                SetTrailPosition(position);
                break;
            case DragStatus.End:
                m_detectingPattern = false;
                m_currentTrailRenderer = null;
                if(ProcessPattern())
                {
                    m_detectingTime = 0;
                }
                break;
        }
    }

    private void SetTrailPosition(Vector3 position)
    {
        Vector3 worldPosition = m_mainCamera.ScreenToWorldPoint(position) - m_trailRendererDepth;
        m_currentTrailRenderer.transform.position = worldPosition;
    }

    private bool ProcessPattern()
    {
        //First we check if is a FinishPattern;
        if(GameManager.Instance.IsFinishPatternActive && IsFinishPattern(MathUtils.ConvertList(m_dragPositions)))
        {
            return true;
        }

        SkillData newSkillData;
        if(m_character.TrySearchSkill(MathUtils.ConvertList(m_dragPositions), out newSkillData))
        {
            Debug.Log("Ability Found!: " + newSkillData.SkillDefinition.SkillName);            
            m_skillToCast = newSkillData;
            m_character.Entity.PlayCastAnimation();
            m_character.Entity.CharacterCanvas.ShowBubbleText(newSkillData.SkillDefinition.SkillName + "!!", 3);
            return true;
        }
        return false;
    }

    private bool IsFinishPattern(Vector2[] vector2)
    {
        RecognitionResult result;
        return m_forceFinish = PatternManager.Instance.ProcessSkillPattern(vector2, GameManager.Instance.FinishPattern, out result);
    }
    
}

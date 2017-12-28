using GameModules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleGameplayState : GenericState<Wizard, TransitionData>
{
    private SkillData m_skillToCast;
    private Vector3 m_trailRendererDepth;
    private TrailRenderer m_currentTrailRenderer;
    private List<Vector3> m_dragPositions = new List<Vector3>();
    private Camera m_mainCamera;

    public override void OnEnter(TransitionData data)
    {
        base.OnEnter(data);
        m_mainCamera = Camera.main;
        m_trailRendererDepth = new Vector3(0f, 0f, m_mainCamera.transform.position.z + 2);
        m_dragPositions.Clear();
        InputManager.Instance.OnDragEvent += OnDrag;
        Debug.Log("Entering Idle Gameplay State");

        IdleHUD idleHud = UIManager.Instance.GetHUDElement<IdleHUD>();
        if(idleHud == null)
        {
            idleHud = UIManager.Instance.AddHUDElement<IdleHUD>();
        }
        idleHud.Show();
        idleHud.CreateSkillsBook(m_character.SkillTree);

        m_character.ResetSkillTree();
        m_character.Entity.PlayIdleAnimation();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override StateTransition<TransitionData> EvaluateTransition()
    {
        if(!m_character.Session.HasTarget)
        {
            return StateTransition<TransitionData>.None;
        }

        if(m_skillToCast != null)
        {
            TransitionData data = new TransitionData(m_skillToCast);
            return new StateTransition<TransitionData>(typeof(ActiveGameplayState), false, data);
        }
        return base.EvaluateTransition();
    }

    private void OnDrag(DragStatus status, Vector3 position, Vector3 lastPosition)
    {
        switch (status)
        {
            case DragStatus.Begin:
                m_currentTrailRenderer = EffectsManager.Instance.RequestTrailRenderer();
                m_dragPositions.Clear();
                m_dragPositions.Add(position);
                break;
            case DragStatus.Moving:
                m_dragPositions.Add(position);
                SetTrailPosition(position);
                break;
            case DragStatus.End:
                ProcessPattern();
                m_currentTrailRenderer = null;
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
        SkillData newSkillData;
        if (m_character.TryProcessPattern(MathUtils.ConvertList(m_dragPositions), out newSkillData))
        {
            Debug.Log("Ability Found!: " + newSkillData.SkillDefinition.SkillName);
            m_skillToCast = newSkillData;
            m_character.Entity.PlayCastAnimation();
            m_character.Entity.CharacterCanvas.ShowBubbleText(newSkillData.SkillDefinition.SkillName + "!!", 3);
            return true;
        }
        return false;
    }

    public override void OnExtit()
    {
        base.OnExtit();
        m_dragPositions.Clear();
        m_skillToCast = null;
        InputManager.Instance.OnDragEvent -= OnDrag;
        UIManager.Instance.GetHUDElement<IdleHUD>().Hide();
    }
}

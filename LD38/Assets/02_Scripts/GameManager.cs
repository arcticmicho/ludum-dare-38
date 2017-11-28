using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private GameSerializer m_serializer;
    public GameSerializer Serializer
    {
        get { return m_serializer; }
    }

    [SerializeField]
    private bool m_isFinishPatternActive;
    public bool IsFinishPatternActive { get { return m_isFinishPatternActive; } }

    [SerializeField]
    private SkillPattern m_finishPattern;
    public SkillPattern FinishPattern
    {
        get { return m_finishPattern; }
    }

    private GameData m_gameData;

    public override void Init()
    {
        m_serializer = new GameSerializer();
        m_serializer.ShiftData = true;
        m_serializer.EncrypData = false;
    }

    public void PostLoad()
    {
        m_gameData = m_serializer.GameData;
    }

    public void SetSelectedWizard(WizardData newWizard)
    {
        if(newWizard != null)
        {
            m_gameData.SelectedWizardId = newWizard.WizardID;
        }
    }

    public void SetRoomId(RoomSessionData newRoom)
    {
        if(newRoom != null)
        {
            m_gameData.SelectRoomId = newRoom.RoomSessionId;
        }
    }

    public WizardData GetSelectedWizard()
    {
        WizardData wizardData = CharactersManager.Instance.UnlockedWizards.Find((w) => string.Equals(w.WizardID, m_gameData.SelectedWizardId));
        if(wizardData == null)
        {
            wizardData = CharactersManager.Instance.UnlockedWizards[0];
        }
        return wizardData;
    }

    public RoomSessionData GetSelectedRoomData()
    {
        return ResourceManager.Instance.RoomTemplateResources.GetRoomSessionTemplateById(m_gameData.SelectRoomId);
    }

    private void Start()
    {

    }

    public void RequestGameSession()
    {

    }

    private void Update()
    {
        if(m_serializer.IsDirty)
        {
            m_serializer.SerializeData();
        }
    }    
}

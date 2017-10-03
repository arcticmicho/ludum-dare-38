﻿using System.Collections;
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

    private GameSession m_currentGameSession;
    public GameSession ActiveGameSession
    {
        get { return m_currentGameSession; }
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

    #region Test
    [SerializeField]
    private RoomSessionData m_testData;
    #endregion

    public void Init()
    {
        m_serializer = new GameSerializer();
        m_serializer.ShiftData = true;
        m_serializer.EncrypData = true;
    }

    public GameSession StartGameSession(RoomSessionData data)
    {
        if(m_currentGameSession != null)
        {
            Debug.LogError("Cannot start a gamession with another active");
            return null;
        }

        m_currentGameSession = new GameSession(data);
        m_currentGameSession.StartSession();
        return m_currentGameSession;
    }

    private void Start()
    {
        StartGameSession(m_testData);
    }

    public void RequestGameSession()
    {
        if(m_currentGameSession == null || m_currentGameSession.SessionFinished)
        {
            if(m_currentGameSession != null)
            {
                m_currentGameSession.UnloadSession();
                m_currentGameSession = null;
            }
            StartGameSession(m_testData);
        }
    }

    private void Update()
    {
        if(m_currentGameSession != null && !m_currentGameSession.SessionFinished)
        {
            if(m_currentGameSession.ProcessSession(TimeManager.Instance.DeltaTime))
            {
                m_currentGameSession.EndSession();
            }
        }
    }    
}

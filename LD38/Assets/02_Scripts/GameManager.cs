using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private GameSession m_currentGameSession;
    public GameSession ActiveGameSession
    {
        get { return m_currentGameSession; }
    }

    #region Test
    [SerializeField]
    private RoomSessionData m_testData;
    #endregion

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

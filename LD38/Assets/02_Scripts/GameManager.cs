using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private GameSession _currentGameSession;
    public GameSession ActiveGameSession
    {
        get { return _currentGameSession; }
    }

    #region Test
    [SerializeField]
    private RoomSessionData m_testData;
    #endregion

    public GameSession StartGameSession(RoomSessionData data)
    {
        if(_currentGameSession != null)
        {
            Debug.LogError("Cannot start a gamession with another active");
            return null;
        }

        _currentGameSession = new GameSession(data);
        _currentGameSession.StartSession();
        return _currentGameSession;
    }

    private void Start()
    {
        StartGameSession(m_testData);
    }

    private void Update()
    {
        if(_currentGameSession != null)
        {
            _currentGameSession.UpdateSession(TimeManager.Instance.DeltaTime);
        }
    }
}

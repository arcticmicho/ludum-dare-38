using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSession
{
    private bool m_sessionFinished;

    private RoomSessionData m_sessionData;
    private RoomSessionView m_sessionView;

    private Wizard m_mainCharacter;
    private EnemyCharacter m_enemy;

    private float m_timeSession;

    public RoomSession(RoomSessionData data, RoomSessionView view)
    {
        m_sessionData = data;
        m_sessionView = view;
    }

	private void StartSession()
    {
        m_mainCharacter = CharactersManager.Instance.GetSelectedWizard();
        m_enemy = CharactersManager.Instance.GetEnemy();
    }

    private void EndSession()
    {

    }

    private void UpdateSession()
    {

    }

    
}

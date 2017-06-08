using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession
{
    private bool m_sessionFinished;

    private GameplayPSM m_gameplayStateMachine;

    private RoomSessionData m_sessionData;
    private RoomSessionView m_sessionView;

    private Wizard m_mainCharacter;
    private EnemyCharacter m_enemy;

    private float m_timeSession;

    public GameSession(RoomSessionData data)
    {
        m_sessionData = data;
    }

	public void StartSession()
    {
        InstantiateRoomView();
        InstantiateCharacters();        

        m_gameplayStateMachine = new GameplayPSM(m_mainCharacter);
        m_gameplayStateMachine.StartGameplayStateMachine();
    }

    private void InstantiateCharacters()
    {
        m_mainCharacter = CharactersManager.Instance.GetSelectedWizard();
        m_mainCharacter.Entity.transform.position = m_sessionView.CharacterPosition;

        m_enemy = CharactersManager.Instance.GetEnemy();
        m_enemy.Entity.transform.position = m_sessionView.EnemyPosition;
    }

    private void InstantiateRoomView()
    {
        m_sessionView = GameObject.Instantiate<RoomSessionView>(m_sessionData.RoomViewTemplate);
        m_sessionView.transform.position = Vector3.zero;
    }

    public void EndSession()
    {

    }

    public void UpdateSession(float deltaTime)
    {
        m_gameplayStateMachine.UpdatePSM();
    }

    
}

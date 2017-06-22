using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession
{
    private bool m_sessionFinished;

    private GameplayPSM m_mainCharacterStateMachine;

    private RoomSessionData m_sessionData;
    private RoomSessionView m_sessionView;

    private Wizard m_mainCharacter;
    private EnemyCharacter m_enemy;

    private EnemyCharacter m_currentTarget;
    public EnemyCharacter CurrentTarget
    {
        get { return m_currentTarget; }
    }

    private ActionsManager m_actions;
    public ActionsManager ActionManager
    {
        get { return m_actions; }
    }

    private float m_timeSession;

    public GameSession(RoomSessionData data)
    {
        m_sessionData = data;
        m_actions = new ActionsManager();
    }

	public void StartSession()
    {
        m_actions.Initialize();
        InstantiateRoomView();
        InstantiateCharacters();        

        m_mainCharacterStateMachine = new GameplayPSM(m_mainCharacter);
        m_mainCharacterStateMachine.StartGameplayStateMachine();

        m_currentTarget = m_enemy;
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
        m_mainCharacterStateMachine.UpdatePSM();
        m_actions.UpdateActions();
    }

    internal void NotifyEnemyDeath(EnemyCharacter enemyCharacter)
    {
        if(enemyCharacter == m_currentTarget)
        {
            //Change Target;
            //For now show Restart state;
        }
    }
}

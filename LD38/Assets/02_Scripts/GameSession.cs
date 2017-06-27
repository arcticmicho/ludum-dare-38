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

    private List<EnemyCharacter> m_enemies;
    private int m_totalEnemies;
    private int m_defeatedEnemies;

    private EnemyCharacter m_currentTarget;
    public EnemyCharacter CurrentTarget
    {
        get { return m_currentTarget; }
    }

    public bool HasTarget
    {
        get { return m_currentTarget != null; }
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
        InstantiateCharacter();        

        m_mainCharacterStateMachine = new GameplayPSM(m_mainCharacter);
        m_mainCharacterStateMachine.StartGameplayStateMachine();

        GenerateSpawnEnemyAction();

        m_currentTarget = null;
    }

    private void InstantiateCharacter()
    {
        m_mainCharacter = CharactersManager.Instance.GetSelectedWizard();
        m_sessionView.MainCharacterPoint.AssignCharacter(m_mainCharacter);
        m_mainCharacter.Entity.TranslateEntity(m_sessionView.MainCharacterPoint.transform.position);
    }

    private void GenerateSpawnEnemyAction()
    {        
        for(int i=0, count=m_sessionData.EnemiesSpawnData.Count; i<count; i++)
        {
            m_actions.EnqueueAction(new SpawnEnemyAction(m_sessionData.EnemiesSpawnData[i].EnemyTemplate, TimeManager.Instance.CurrentTime.AddSeconds(m_sessionData.EnemiesSpawnData[i].SpawnTime)));
        }
        m_totalEnemies = m_sessionData.EnemiesSpawnData.Count;
        m_defeatedEnemies = 0;
    }

    private void InstantiateRoomView()
    {
        m_sessionView = GameObject.Instantiate<RoomSessionView>(m_sessionData.RoomViewTemplate);
        m_sessionView.transform.position = Vector3.zero;
    }

    public void EndSession()
    {

    }

    public bool ProcessSession(float deltaTime)
    {
        m_mainCharacterStateMachine.UpdatePSM();
        m_actions.UpdateActions();
        if(m_currentTarget == null)
        {
            FindNewTarget();
        }

        if(m_defeatedEnemies >= m_totalEnemies)
        {
            return true;
        }
        return false;
    }

    private void FindNewTarget()
    {
        if(m_enemies.Count > 0)
        {
            EnemyCharacter enemy = m_enemies[UnityEngine.Random.Range(0, m_enemies.Count)];
            m_mainCharacter.Entity.SetDirection(((m_mainCharacter.Entity.transform.position - enemy.Entity.transform.position).x > 0 ? EDirection.Left : EDirection.Right));
            m_currentTarget = enemy;
        }
    }

    public void NotifyEnemyDeath(EnemyCharacter enemyCharacter)
    {
        if(m_enemies.Contains(enemyCharacter))
        {
            m_enemies.Remove(enemyCharacter);
            m_defeatedEnemies++;
            if(enemyCharacter == m_currentTarget)
            {
                m_currentTarget = null;
            }
        }else
        {
            Debug.LogError("Trying to Remove a not valid enemy");
        }
    }

    public void SpawnEnemy(CharacterTemplate enemyTemplate)
    {
        BaseRoomPoint spawnPoint = m_sessionView.GetRandomAvailableEnemyPoint();

        EnemyCharacter enemy = CharactersManager.Instance.GetEnemy();
        spawnPoint.AssignCharacter(enemy);
        enemy.Entity.TranslateEntity(spawnPoint.transform.position);
        enemy.Entity.SetDirection(spawnPoint.Direction);
        m_enemies.Add(enemy);
    }
}


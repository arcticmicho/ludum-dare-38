using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession
{
    private bool m_sessionFinished;
    private bool m_sessionClosed;

    private GameStateData m_gameStateData;

    private GenericStateMachine<Wizard,TransitionData> m_mainCharacterStateMachine;

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

    public Character MainCharacter
    {
        get
        {
            return m_mainCharacter;
        }
    }

    public bool SessionFinished
    {
        get { return m_sessionFinished; }
    }

    public bool SessionClosed
    {
        get { return m_sessionClosed; }
    }

    private float m_timeSession;

    private bool m_gameOver;

    public GameSession(GameStateData data)
    {
        m_sessionData = data.SessionData;
        m_actions = new ActionsManager();
        m_gameStateData = data;
    }

	public void StartSession()
    {
        m_gameOver = false;
        m_actions.Initialize();
        InstantiateRoomView();
        InstantiateCharacter();        

        m_mainCharacterStateMachine = new GenericStateMachine<Wizard, TransitionData>(m_mainCharacter);
        m_mainCharacterStateMachine.StartGameplayStateMachine<IdleGameplayState>();

        GenerateSpawnEnemyAction();

        m_currentTarget = null;
    }

    private void InstantiateCharacter()
    {
        m_enemies = new List<EnemyCharacter>();
        m_mainCharacter = new Wizard(this, m_gameStateData.WizardData, CharactersManager.Instance.MainCharacterEntity);
        m_sessionView.MainCharacterPoint.AssignCharacter(m_mainCharacter);
        m_mainCharacter.Entity.TranslateEntity(m_sessionView.MainCharacterPoint.transform.position);
    }

    private void GenerateSpawnEnemyAction()
    {        
        for(int i=0, count=m_sessionData.EnemiesSpawnData.Count; i<count; i++)
        {
            m_actions.EnqueueAction(new SpawnEnemyAction(this, m_sessionData.EnemiesSpawnData[i].EnemyTemplate, TimeManager.Instance.CurrentTime.AddSeconds(m_sessionData.EnemiesSpawnData[i].SpawnTime)));
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
        m_mainCharacterStateMachine.UpdateSM();
        m_actions.UpdateActions();
        if(m_currentTarget == null)
        {
            FindNewTarget();
        }

        for (int i = 0, count = m_enemies.Count; i < count; i++)
        {
            m_enemies[i].UpdateEnemy();
        }

        if (m_defeatedEnemies >= m_totalEnemies)
        {
            WinSession();
            m_sessionFinished = true;
            return true;
        }else if(m_gameOver)
        {
            GameOver();
            m_sessionFinished = true;
            return true;
        }
        return false;
    }

    private void GameOver()
    {
        EndView.RequestEndView(OnEndViewFinished);
        UIPartyManager.Instance.GetView<EndView>().SetEndText("You Lose!");
    }
    private void WinSession()
    {
        EndView.RequestEndView(OnEndViewFinished);
        UIPartyManager.Instance.GetView<EndView>().SetEndText("You Win!");
    }

    private void OnEndViewFinished()
    {
        m_sessionClosed = true;
        UnloadSession();
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
            enemyCharacter.CurrentPoint.ReleasePoint();
            if(enemyCharacter == m_currentTarget)
            {
                m_currentTarget = null;
            }
        }else
        {
            Debug.LogError("Trying to Remove a not valid enemy");
        }
    }

    public void NotifyMainCharacterDeath(Wizard mainCharacter)
    {
        //lets check if its true
        if(m_mainCharacter == mainCharacter)
        {
            m_gameOver = true;
        }
    }

    public void SpawnEnemy(CharacterTemplate enemyTemplate)
    {
        BaseRoomPoint spawnPoint = m_sessionView.GetRandomAvailableEnemyPoint();

        EnemyCharacter enemy = CharactersManager.Instance.GetEnemy(this);
        spawnPoint.AssignCharacter(enemy);
        enemy.Entity.TranslateEntity(m_sessionView.GetNearestSpawnPoint(spawnPoint.transform.position));
        enemy.Entity.SetDirection(spawnPoint.Direction);
        m_enemies.Add(enemy);
        enemy.EnemySpawned();
    }


    internal void UnloadSession()
    {
        GameObject.Destroy(m_mainCharacter.Entity.gameObject);
        m_mainCharacter = null;
        for(int i=0, count=m_enemies.Count; i<count; i++)
        {
            GameObject.Destroy(m_enemies[i].Entity.gameObject);
            m_enemies[i] = null;
        }
        GameObject.Destroy(m_sessionView.gameObject);
        m_actions.ClearAction();

    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameModules;

public class GameSession
{
    // change those for an enum Session Status
    private bool m_finished;
    private bool m_closed;

    private float m_sessionTime;

    private bool m_gameOver;

    private GameStateData m_gameStateData;

    private RoomSessionData m_sessionData;
    private RoomSessionView m_roomView;

    private Wizard m_mainCharacter;

    private List<EnemyCharacter> m_activeEnemies;
    private List<EnemyCharacter> m_enemies;

    private EnemyCharacter   m_currentTarget;
    private ActionController m_actionController;

    private LevelController m_levelController;

    #region Get/Set
    public EnemyCharacter CurrentTarget
    {
        get { return m_currentTarget; }
    }

    public bool HasTarget
    {
        get { return m_currentTarget != null; }
    }

    public ActionController ActionController
    {
        get { return m_actionController; }
    }

    public Character MainCharacter
    {
        get { return m_mainCharacter; }
    }

    public bool SessionFinished
    {
        get { return m_finished; }
    }

    public bool SessionClosed
    {
        get { return m_closed; }
    }
    #endregion

    public GameSession(GameStateData data)
    {
        m_gameStateData = data;
        m_sessionData = data.SessionData;
        m_actionController = new ActionController();
        m_levelController = new LevelController(this);
    
    }

	public void StartSession()
    {
        m_gameOver = false;
        m_actionController.Initialize();
        InstantiateRoomView();
        InstantiateCharacter();        

        m_currentTarget = null;

        m_levelController.StartLevel(m_sessionData.Level);

        DebugManager.Instance.AddDebugAction("Kill Target", () =>
        {
            if (m_currentTarget != null)
            {
                m_currentTarget.KillCharacter();
            }
        },false);
    }

    private void InstantiateCharacter()
    {
        m_activeEnemies = new List<EnemyCharacter>();
        m_enemies = new List<EnemyCharacter>();

        m_mainCharacter = new Wizard(this, m_gameStateData.WizardData, CharactersManager.Instance.MainCharacterEntity);

        m_roomView.MainCharacterPoint.AssignCharacter(m_mainCharacter);
        m_mainCharacter.Entity.TranslateEntity(m_roomView.MainCharacterPoint.transform.position);
    }

    /*private void GenerateSpawnEnemyAction()
    {        
        for(int i=0, count=m_sessionData.EnemiesSpawnData.Count; i<count; i++)
        {
            m_actionController.EnqueueAction(new SpawnEnemyAction(this, m_sessionData.EnemiesSpawnData[i].EnemyTemplate, TimeManager.Instance.CurrentTime.AddSeconds(m_sessionData.EnemiesSpawnData[i].SpawnTime)));
        }
        m_totalEnemies = m_sessionData.EnemiesSpawnData.Count;
        m_defeatedEnemies = 0;
    }*/

    private void InstantiateRoomView()
    {
        m_roomView = GameObject.Instantiate<RoomSessionView>(m_sessionData.RoomViewTemplate);
        m_roomView.transform.position = Vector3.zero;
    }

    public void EndSession()
    {
        DebugManager.Instance.RemoveDebugAction("Kill Target");
    }

    public bool Update(float deltaTime)
    {
        m_mainCharacter.Update();

        m_actionController.UpdateActions();

        if(m_currentTarget == null)
        {
            FindNewTarget();
        }

        var levelResult = m_levelController.Update();

        if(levelResult == LevelStatus.Finished)
        {
            Debug.LogWarning("Level Status Finished");
            WinSession();
            m_finished = true;
            return true;
        }

        for (int i = 0, count = m_activeEnemies.Count; i < count; i++)
        {
            m_activeEnemies[i].UpdateEnemy();
        }

        if(m_gameOver)
        {
            GameOver();
            m_finished = true;
            return true;
        }
        return false;
    }

    private void GameOver()
    {
        EndPopup.RequestEndView(OnEndViewFinished);
    }

    private void WinSession()
    {
        EndPopup.RequestEndView(OnEndViewFinished);
    }

    private void OnEndViewFinished()
    {
        m_closed = true;
        UnloadSession();
    }

    private void FindNewTarget()
    {
        if(m_activeEnemies.Count > 0)
        {
            EnemyCharacter enemy = m_activeEnemies[UnityEngine.Random.Range(0, m_activeEnemies.Count)];
            m_mainCharacter.Entity.SetDirection(((m_mainCharacter.Entity.transform.position - enemy.Entity.transform.position).x > 0 ? EDirection.Left : EDirection.Right));
            m_currentTarget = enemy;
        }
    }

    public void NotifyMainCharacterDeath(Wizard mainCharacter)
    {
        if(m_mainCharacter == mainCharacter)
        {
            m_gameOver = true;
        }
    }

    public EnemyCharacter CreateEnemy(CharacterTemplate enemyTemplate)
    {
        BaseRoomPoint spawnPoint = m_roomView.GetRandomAvailableEnemyPoint();

        EnemyCharacter enemy = CharactersManager.Instance.GetEnemy(this, enemyTemplate);
        spawnPoint.AssignCharacter(enemy);
        enemy.Entity.TranslateEntity(m_roomView.GetNearestSpawnPoint(spawnPoint.transform.position));
        enemy.Entity.SetDirection(spawnPoint.Direction);

        m_activeEnemies.Add(enemy);
        m_enemies.Add(enemy);

        enemy.EnemySpawned();

        return enemy;
    }

    public void NotifyEnemyDeath(EnemyCharacter enemyCharacter)
    {
        if (m_activeEnemies.Remove(enemyCharacter))
        {
            enemyCharacter.CurrentPoint.ReleasePoint();
            if (enemyCharacter == m_currentTarget)
            {
                m_currentTarget = null;
            }

            if(m_enemies.Remove(enemyCharacter))
            {
                GameObject.Destroy(enemyCharacter.Entity.gameObject);
            }
        }
        else
        {
            Debug.LogError("Trying to Remove a not valid enemy");
        }
    }

    internal void UnloadSession()
    {
        GameObject.Destroy(m_mainCharacter.Entity.gameObject);
        m_mainCharacter = null;
        for(int i=0, count= m_enemies.Count; i<count; i++)
        {
            GameObject.Destroy(m_enemies[i].Entity.gameObject);
            m_enemies[i] = null;
        }
        GameObject.Destroy(m_roomView.gameObject);
        m_actionController.ClearAction();
    }
}


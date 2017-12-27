using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemySetRunStatus
{
    NotStarted = 0,
    Running    = 1,
    Finished   = 2,
}
public class EnemyWaveSetStatus
{
    private GameSession m_session;
    private EnemyWaveSet m_enemySet;

    private EnemySetRunStatus m_runStatus;

    private int m_enemiesLeft = 0;

    private float m_nextSpawnTime = 0;

    private List<EnemyCharacter> m_liveEnemies = new List<EnemyCharacter>();

    #region Get/Set
    public EnemyWaveSet EnemySet
    {
        get { return m_enemySet; }
    }

    public EnemySetRunStatus RunStatus
    {
        get { return m_runStatus; }
    }
    #endregion

    private void SpawnNext()
    {
        if (m_enemiesLeft > 0)
        {
            var enemyToSpawn = m_enemySet.Enemies.RandomElement();

            var enemy = m_session.CreateEnemy(enemyToSpawn);
            if(enemy != null)
            {
                enemy.SetOwner(this);
                m_liveEnemies.Add(enemy);
            }
            m_enemiesLeft--;
        }
    }

    public EnemyWaveSetStatus(GameSession session, EnemyWaveSet enemySet)
    {
        m_session = session;
        m_enemySet = enemySet;
    }

    public void Start()
    {
        Debug.LogWarning("Enemy Set Start "+m_enemySet.name);
        m_enemiesLeft = m_enemySet.EnemyCount;
        m_nextSpawnTime = 0;
        m_runStatus = EnemySetRunStatus.Running;
    }

    public void Finish()
    {
        Debug.LogWarning("Enemy Set End " + m_enemySet.name);
        m_runStatus = EnemySetRunStatus.Finished;
    }

    public void Clear()
    {
        for (int a = 0; a < m_liveEnemies.Count; a++)
        {
            m_liveEnemies[a].KillCharacter();
        }
        m_runStatus = EnemySetRunStatus.Finished;
    }

    public EnemySetRunStatus Update()
    {
        if (m_runStatus == EnemySetRunStatus.Running)
        {
            if (m_enemiesLeft > 0)
            {
                for (int a = 0; a < m_liveEnemies.Count; ++a)
                {
                    m_liveEnemies[a].UpdateEnemy();
                }

                if (m_liveEnemies.Count < m_enemySet.MaxConcurrentEnemies)
                {
                    m_nextSpawnTime -= Time.deltaTime;
                    if (m_nextSpawnTime <= 0)
                    {
                        SpawnNext();

                        m_nextSpawnTime += m_enemySet.SpawnRate;
                    }
                }
            }
            else
            {
                if(m_liveEnemies.Count<=0)
                {
                    Finish();
                }
            }
        }

        return m_runStatus;
    }

    public void NotifyEnemyDeath(EnemyCharacter enemy)
    {
        m_liveEnemies.Remove(enemy);
    }
}

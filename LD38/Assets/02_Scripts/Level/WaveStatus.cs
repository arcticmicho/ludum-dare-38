using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WaveRunStatus
{
    NotStarted = 0,
    Running  = 1,
    Finished = 2,
}

public class WaveStatus
{
    private Wave m_wave;
    private GameSession m_session;

    private WaveRunStatus m_runStatus = WaveRunStatus.NotStarted;

    private List<EnemyWaveSetStatus> m_enemySetStatusList = new List<EnemyWaveSetStatus>();

    private List<EnemyWaveSetStatus> m_runningEnemySets = new List<EnemyWaveSetStatus>();

    private int m_currentSetIndex = 0;

    #region Get/Set
    public WaveRunStatus RunStatus
    {
        get { return m_runStatus; }
    }

    public Wave Wave
    {
        get { return m_wave; }
    }
    #endregion

    public WaveStatus(GameSession session, Wave wave)
    {
        m_wave = wave;
        m_session = session;

        m_currentSetIndex = 0;

        for (int a = 0; a < m_wave.EnemySets.Count; ++a)
        {
            var enemySet = m_wave.EnemySets[a];
            var enemySetStatus = new EnemyWaveSetStatus(session,enemySet);

            m_enemySetStatusList.Add(enemySetStatus);
        }
    }

    private void RunEnemySet(EnemyWaveSetStatus enemySet)
    {
        enemySet.Start();
        m_runningEnemySets.Add(enemySet);
    }



    private bool CanContinue()
    {
        switch(m_wave.Type)
        {
            case WaveType.All:
            case WaveType.Random:
                return false;
            case WaveType.Sequencial:
            case WaveType.Shuffle:
                return m_currentSetIndex < m_enemySetStatusList.Count;
        }
        return false;
    }

    public void StartNext()
    {
        if(m_wave.Type == WaveType.All)
        {
            for (int a = 0; a < m_enemySetStatusList.Count; ++a)
            {
                RunEnemySet(m_enemySetStatusList[a]);
            }
        }
        else if (m_wave.Type == WaveType.Random)
        {
            RunEnemySet(m_enemySetStatusList.RandomElement());
        }
        else if (m_wave.Type == WaveType.Sequencial)
        {
            RunEnemySet(m_enemySetStatusList[m_currentSetIndex]);
            m_currentSetIndex++;
        }
        else if (m_wave.Type == WaveType.Shuffle)
        {
            RunEnemySet(m_enemySetStatusList[m_currentSetIndex]);
            m_currentSetIndex++;
        }

        if (m_runStatus == WaveRunStatus.NotStarted)
        {
            m_runStatus = WaveRunStatus.Running;
        }

        Debug.LogWarning("Wave Status Starting next");
    }

    private void Finish()
    {
        Debug.LogWarning("Wave Status Finish");
        m_runStatus = WaveRunStatus.Finished;
    }

    public void Stop()
    {
        m_runStatus = WaveRunStatus.Finished;
    }

    public void Clear()
    {
        for (int a = 0; a < m_runningEnemySets.Count; ++a)
        {
            m_runningEnemySets[a].Clear();
        }
        m_runningEnemySets.Clear();
        m_enemySetStatusList.Clear();
    }

    public WaveRunStatus Update()
    {
        if (m_runStatus == WaveRunStatus.Running)
        {
            if (m_runningEnemySets.Count > 0)
            {
                for (int a = 0; a < m_runningEnemySets.Count; ++a)
                {
                    var result = m_runningEnemySets[a].Update();
                    if(result == EnemySetRunStatus.Finished)
                    {
                        m_runningEnemySets.RemoveAt(a);
                        a--;
                    }
                }
            }
            else
            {
                if (CanContinue())
                {
                    StartNext();
                }
                else
                {
                    Finish();
                }
            }
        }

        return m_runStatus;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum LevelStatus
{
    NotStarted = 0,
    Running = 1,
    Finished = 2,
}

public class LevelController 
{
    private GameSession m_gameSession;

    private Level m_currentLevel;
    private WaveStatus m_currentWave;

    private int m_waveIndex = 0;

    private LevelStatus m_levelStatus = LevelStatus.NotStarted;

    #region Get/Set
    public LevelStatus LevelStatus
    {
        get { return m_levelStatus; }
    }
    #endregion

    public LevelController(GameSession gameSession)
    {
        m_gameSession = gameSession;
    }

    public void StartLevel(Level level)
    {
        m_currentLevel = level;
        m_waveIndex = 0;
        m_levelStatus = LevelStatus.Running;

        if (!StartNextWave())
        {
            Debug.LogWarning("Failed To Start level: " + level.name);
        }
    }

    private Wave GetNextWave()
    {
        var wave = m_currentLevel.GetWave(m_waveIndex);
        m_waveIndex++;

        return wave;
    }

    private bool StartNextWave()
    {
        Wave wave = GetNextWave();

        if (wave != null)
        {
            m_currentWave = new WaveStatus(m_gameSession, wave);
            m_currentWave.StartNext();
            return true;
        }

        return false;
    }

    private void ClearCurrentWave()
    {
        if (m_currentWave != null)
        {
            m_currentWave.Clear();
            m_currentWave = null;
        }
    }

    private void StopCurrentWave()
    {
        if (m_currentWave != null)
        {
            m_currentWave.Stop();
            m_currentWave = null;
        }
    }

    public LevelStatus Update()
    {
        if (m_levelStatus == LevelStatus.Running)
        {
            if (m_currentWave != null)
            {
                if (m_currentWave.Update() == WaveRunStatus.Finished)
                {
                    m_currentWave = null;
                    if (!StartNextWave())
                    {
                        m_levelStatus = LevelStatus.Finished;
                    }
                }
            }
        }
        return m_levelStatus;
    }
}

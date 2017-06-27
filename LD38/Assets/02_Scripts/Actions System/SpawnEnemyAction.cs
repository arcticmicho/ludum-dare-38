using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyAction : IAction
{
    private DateTime m_spawnDate;
    private CharacterTemplate m_enemyTemplate;

    public SpawnEnemyAction(CharacterTemplate template, DateTime spawnDate)
    {
        m_spawnDate = spawnDate;
        m_enemyTemplate = template;
    }

    public DateTime ActionTime
    {
        get
        {
            return m_spawnDate;
        }
    }

    public void EndAction()
    {

    }

    public bool ProcessAction(float deltaTime)
    {
        GameManager.Instance.ActiveGameSession.SpawnEnemy(m_enemyTemplate);
        return true;
    }

    public void StartAction()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyAction : GameAction
{
    private DateTime m_spawnDate;
    private CharacterTemplate m_enemyTemplate;

    public SpawnEnemyAction(GameSession session, CharacterTemplate template, DateTime spawnDate) : base(session)
    {
        m_spawnDate = spawnDate;
        m_enemyTemplate = template;
    }

    public override DateTime ActionTime
    {
        get
        {
            return m_spawnDate;
        }
    }

    public override void EndAction()
    {

    }

    public override bool ProcessAction(float deltaTime)
    {
        m_contextSession.CreateEnemy(m_enemyTemplate);
        return true;
    }

    public override void StartAction()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    private GenericStateMachine<EnemyCharacter, EnemyTransitionData> m_enemySM;

    public EnemyCharacter(GameSession session, CharacterTemplate template, CharacterEntity entity) : base(session, template, entity)
    {
        m_enemySM = new GenericStateMachine<EnemyCharacter, EnemyTransitionData>(this);                               
    }    

    public override void KillCharacter()
    {
        base.KillCharacter();
        m_contextSession.NotifyEnemyDeath(this);
    }

    public void EnemySpawned()
    {
        m_enemySM.StartGameplayStateMachine<EnemyMoveToCurrentPointState>();
    }    

    public void UpdateEnemy()
    {
        m_enemySM.UpdateSM();
    }
}

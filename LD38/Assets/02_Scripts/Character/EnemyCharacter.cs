using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    private EnemyWaveSetStatus m_currentOwner;

    private GenericStateMachine<EnemyCharacter, EnemyTransitionData> m_enemySM;

    public override Character CurrentTarget
    {
        get { return m_contextSession.MainCharacter; }
    }

    public EnemyCharacter(GameSession session, CharacterTemplate template, CharacterEntity entity) : base(session, template, entity)
    {
        m_enemySM = new GenericStateMachine<EnemyCharacter, EnemyTransitionData>(this);                               
    }    

    public void SetOwner(EnemyWaveSetStatus owner)
    {
        m_currentOwner = owner;
    }

    public override void KillCharacter()
    {
        base.KillCharacter();

        m_currentOwner.NotifyEnemyDeath(this);
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

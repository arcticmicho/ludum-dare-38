using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    private GenericStateMachine<EnemyCharacter, EnemyTransitionData> m_enemySM;

    public EnemyCharacter(CharacterTemplate template) : base(template)
    {
        m_enemySM = new GenericStateMachine<EnemyCharacter, EnemyTransitionData>(this);                               
    }    

    public override void KillCharacter()
    {
        base.KillCharacter();
        GameManager.Instance.ActiveGameSession.NotifyEnemyDeath(this);
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

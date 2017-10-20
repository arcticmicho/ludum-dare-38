using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoSingleton<CharactersManager>
{
    [SerializeField]
    private CharacterTemplate m_mainCharacter;

    [SerializeField]
    private CharacterTemplate m_enemyCharacter;
    
    public Wizard GetSelectedWizard(GameSession currentSession)
    {
        return new Wizard(currentSession, m_mainCharacter);
    }
    	
    public EnemyCharacter GetEnemy(GameSession currentSession)
    {
        return new EnemyCharacter(currentSession, m_enemyCharacter);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoSingleton<CharactersManager>
{
    [SerializeField]
    private CharacterTemplate m_mainCharacter;
    [SerializeField]
    private CharacterEntity m_mainCharacterEntity;

    [SerializeField]
    private CharacterTemplate m_enemyCharacter;
    [SerializeField]
    private CharacterEntity m_enemyEntity;
    
    public Wizard GetSelectedWizard(GameSession currentSession)
    {
        return new Wizard(currentSession, m_mainCharacter, m_mainCharacterEntity);
    }
    	
    public EnemyCharacter GetEnemy(GameSession currentSession)
    {
        return new EnemyCharacter(currentSession, m_enemyCharacter, m_enemyEntity);
    }
}

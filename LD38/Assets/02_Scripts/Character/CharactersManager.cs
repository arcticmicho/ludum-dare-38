using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoSingleton<CharactersManager>
{
    [SerializeField]
    private CharacterTemplate m_mainCharacter;

    [SerializeField]
    private CharacterTemplate m_enemyCharacter;
    
    public Wizard GetSelectedWizard()
    {
        return new Wizard(m_mainCharacter);
    }
    	
    public EnemyCharacter GetEnemy()
    {
        return new EnemyCharacter(m_enemyCharacter);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoSingleton<CharactersManager>
{
    private PlayerData m_playerData;

    [SerializeField]
    private WizardDataTemplate m_mainCharacter;

    [SerializeField]
    private CharacterEntity m_mainCharacterEntity;
    public CharacterEntity MainCharacterEntity
    {
        get { return m_mainCharacterEntity; }
    }

    [SerializeField]
    private CharacterTemplate m_enemyCharacter;
    [SerializeField]
    private CharacterEntity m_enemyEntity;

    public List<WizardData> UnlockedWizards
    {
        get
        {
            return GameManager.Instance.Serializer.PlayerData.WizardsData;
        }
    }
    
    public void Initialize()
    {
        m_playerData = GameManager.Instance.Serializer.PlayerData;
    }
    	
    public EnemyCharacter GetEnemy(GameSession currentSession)
    {
        return new EnemyCharacter(currentSession, m_enemyCharacter, m_enemyEntity);
    }

    public WizardData GetWizardWithId(string wizardId)
    {
        WizardData wData = m_playerData.WizardsData.Find(x => string.Equals(x, wizardId));
        return wData;
    }
}

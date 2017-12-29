using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoSingleton<CharactersManager>
{
    [SerializeField]
    private WizardDataTemplate m_mainCharacter;

    [SerializeField]
    private CharacterEntity m_mainCharacterEntity;

    [SerializeField]
    private CharacterTemplate m_enemyCharacter;

    [SerializeField]
    private CharacterEntity m_enemyEntity;

    private PlayerData m_playerData;

    #region Get/Set
    public CharacterEntity MainCharacterEntity
    {
        get { return m_mainCharacterEntity; }
    }

    public List<WizardData> UnlockedWizards
    {
        get { return m_playerData.WizardsData; }
    }
    #endregion

    public void PostLoad(bool newGame)
    {
        m_playerData = GameManager.Instance.Serializer.PlayerData;
    }
    	
    public EnemyCharacter GetEnemy(GameSession currentSession, CharacterTemplate template)
    {
        return new EnemyCharacter(currentSession, template, template.Prefab);
    }

    public WizardData GetWizardWithId(string wizardId)
    {
        WizardData wData = m_playerData.WizardsData.Find(x => string.Equals(x, wizardId));
        return wData;
    }
}

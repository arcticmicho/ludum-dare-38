using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GamePlay/Level/EnemyWaveSet", fileName = "Enemy Wave Set")]
public class EnemyWaveSet : ScriptableObject
{
    [Header("Configuration")]
    [SerializeField]
    private float m_spawnRate = 3.0f;

    [SerializeField]
    private int m_enemyCount = 5;

    [SerializeField]
    private int m_maxConcurrentEnemies = 3;

    [Header("Sets")]
    [SerializeField]
    private List<CharacterTemplate> m_enemies = new List<CharacterTemplate>();

    #region Get/Set
    public float SpawnRate
    {
        get { return m_spawnRate; }
    }

    public int EnemyCount
    {
        get { return m_enemyCount; }
    }

    public int MaxConcurrentEnemies
    {
        get { return m_maxConcurrentEnemies; }
    }

    public List<CharacterTemplate> Enemies
    {
        get { return m_enemies; }
    }

    #endregion
}

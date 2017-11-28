using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSessionData : ScriptableObject
{
    [SerializeField]
    private string m_roomSessionId;
    public string RoomSessionId
    {
        get { return m_roomSessionId; }
    }

    [SerializeField]
    private string m_roomName;
    public string RoomName
    {
        get { return m_roomName; }
    }

    [SerializeField]
    private List<EnemySpawnData> m_enemiesSpawnData;
    public List<EnemySpawnData> EnemiesSpawnData
    {
        get { return m_enemiesSpawnData; }
    }

    [SerializeField]
    private CharacterTemplate m_bossTemplate;

    [SerializeField]
    private RoomSessionView m_viewTemplate;
    public RoomSessionView RoomViewTemplate
    {
        get
        {
            return m_viewTemplate;
        }
    }   	
}

[Serializable]
public class EnemySpawnData
{
    [SerializeField]
    private CharacterTemplate m_enemyTemplate;
    public CharacterTemplate EnemyTemplate
    {
        get { return m_enemyTemplate; }
    }

    [SerializeField]
    private float m_spawnTime;
    public float SpawnTime
    {
        get { return m_spawnTime; }
    }
}

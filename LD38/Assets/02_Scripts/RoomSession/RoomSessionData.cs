using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomDataTemplate", menuName = "GamePlay/Rooms/RoomData")]
public class RoomSessionData : ScriptableObject
{
    [SerializeField]
    private string m_roomSessionId;

    [SerializeField]
    private string m_roomName;


    [SerializeField]
    private Level m_level;

    [SerializeField]
    private RoomSessionView m_viewTemplate;

    #region Get/Set
    public string RoomSessionId
    {
        get { return m_roomSessionId; }
    }

    public string RoomName
    {
        get { return m_roomName; }
    }

    public RoomSessionView RoomViewTemplate
    {
        get { return m_viewTemplate; }
    }

    public Level Level
    {
        get { return m_level; }
    }
    #endregion
}

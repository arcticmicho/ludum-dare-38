using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateData
{
    private RoomSessionData m_roomData;
    public RoomSessionData SessionData
    {
        get
        {
            return m_roomData;
        }
    }

    public GameStateData(RoomSessionData sessionData)
    {
        m_roomData = sessionData;
    }
    
}

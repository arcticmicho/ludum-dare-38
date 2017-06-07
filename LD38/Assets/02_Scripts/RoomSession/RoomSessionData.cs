using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSessionData : ScriptableObject
{
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

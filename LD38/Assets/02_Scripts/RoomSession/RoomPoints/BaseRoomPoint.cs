using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDirection
{
    Left = 0,
    Right = 1,
}

public class BaseRoomPoint : MonoBehaviour
{
    [SerializeField]
    private EDirection m_direction;
    public EDirection Direction
    {
        get { return m_direction; }
    }

    private Character m_owner;
    
    public bool IsAvailable
    {
        get
        {
            if(m_owner == null)
            {
                return true;
            }else
            {
                return false;
            }
        }
    }

    public void AssignCharacter(Character character)
    {
        if(IsAvailable)
        {
            m_owner = character;
            m_owner.CurrentPoint = this;
        }else
        {
            Debug.LogError("Trying to Assign character to an used RoomPoint");
        }
    }

    public void ReleasePoint()
    {
        if(m_owner != null)
        {
            m_owner = null;
        }
    }
}

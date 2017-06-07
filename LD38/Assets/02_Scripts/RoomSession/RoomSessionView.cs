using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSessionView : MonoBehaviour
{
    [SerializeField]
    private Transform m_characterPosition;
    public Vector3 CharacterPosition
    {
        get { return m_characterPosition.position; }
    }

    [SerializeField]
    private Transform m_enemyPosition;
    public Vector3 EnemyPosition
    {
        get
        {
            return m_enemyPosition.position;
        }
    }

    [SerializeField]
    private Transform m_enemy2Position;
    public Vector3 Enemy2Position
    {
        get { return m_enemy2Position.position; }
    }
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSessionView : MonoBehaviour
{
    [SerializeField]
    private List<BaseRoomPoint> m_enemyPoints;

    [SerializeField]
    private BaseRoomPoint m_mainCharacterPoint;
    public BaseRoomPoint MainCharacterPoint
    {
        get { return m_mainCharacterPoint; }
    }

    public BaseRoomPoint GetRandomAvailableEnemyPoint()
    {
        List<BaseRoomPoint> filteredList = m_enemyPoints.FindAll((x) => x.IsAvailable);
        return filteredList[UnityEngine.Random.Range(0, filteredList.Count)];
    }
}

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

    [SerializeField]
    private List<Transform> m_spawnPoints;

    public BaseRoomPoint GetRandomAvailableEnemyPoint()
    {
        List<BaseRoomPoint> filteredList = m_enemyPoints.FindAll((x) => x.IsAvailable);
        return filteredList[UnityEngine.Random.Range(0, filteredList.Count)];
    }

    public Vector3 GetNearestSpawnPoint(Vector3 reference)
    {
        Vector3 nearest = new Vector3(float.MaxValue, float.MaxValue);
        for(int i=0,count=m_spawnPoints.Count; i<count; i++)
        {
            if(Vector3.Distance(nearest,reference) > Vector3.Distance(m_spawnPoints[i].position, reference))
            {
                nearest = m_spawnPoints[i].position;
            }
        }
        return nearest;
    }
}

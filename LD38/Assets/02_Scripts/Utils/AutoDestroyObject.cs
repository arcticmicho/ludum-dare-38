using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyObject : MonoBehaviour
{
    [SerializeField]
    private float m_timeToDestroy;

    private float m_elapsedTime;

	void Start ()
    {
        m_elapsedTime = 0f;
    }

	void Update ()
    {
        m_elapsedTime += TimeManager.Instance.DeltaTime;
        if(m_elapsedTime >= m_timeToDestroy)
        {
            Destroy(gameObject);
        }
	}
}

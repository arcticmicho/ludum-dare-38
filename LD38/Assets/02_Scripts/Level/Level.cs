using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Definition", menuName = "GamePlay/Level/Level Definition")]
public class Level : ScriptableObject
{
    [SerializeField]
    private Wave[] m_waves;

    public Wave GetWave(int index)
    {
        if (index >= 0 && index < m_waves.Length)
        {
            return m_waves[index];
        }

        return null; 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WaveType
{
    Sequencial = 0,
    Random     = 1,
    Shuffle    = 2,
    All        = 3,
}

[CreateAssetMenu(menuName = "GamePlay/Level/Wave", fileName = "Wave")]
public class Wave : ScriptableObject
{
    [Header("Configuration")]

    [SerializeField]
    private WaveType m_type = WaveType.Sequencial;

    [Header("Steps")]
    [UnityEngine.Serialization.FormerlySerializedAs("_waveSteps")]
    [SerializeField]
    List<EnemyWaveSet> m_enemySets = new List<EnemyWaveSet>();

    #region Get/Set
    public WaveType Type
    {
        get { return m_type; }
    }

    public List<EnemyWaveSet> EnemySets
    {
        get { return m_enemySets; }
    }
    #endregion


}

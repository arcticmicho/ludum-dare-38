using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private GameSerializer m_serializer;
    public GameSerializer Serializer
    {
        get { return m_serializer; }
    }

    [SerializeField]
    private bool m_isFinishPatternActive;
    public bool IsFinishPatternActive { get { return m_isFinishPatternActive; } }

    [SerializeField]
    private SkillPattern m_finishPattern;
    public SkillPattern FinishPattern
    {
        get { return m_finishPattern; }
    }

    public override void Init()
    {
        m_serializer = new GameSerializer();
        m_serializer.ShiftData = true;
        m_serializer.EncrypData = true;
    }

    private void Start()
    {

    }

    public void RequestGameSession()
    {

    }

    private void Update()
    {

    }    
}

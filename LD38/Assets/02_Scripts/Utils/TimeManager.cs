using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoSingleton<TimeManager>
{
    private float m_deltaTime;
    public float DeltaTime
    {
        get { return m_deltaTime; }
    }

    private float m_deltaTimeFactor = 1f;
    public float DeltaTimeFactor
    {
        get { return m_deltaTimeFactor; }
        set
        {
            m_deltaTimeFactor = value;
            if(OnDeltaTimeFactorChanged != null)
            {
                OnDeltaTimeFactorChanged(value);
            }
        }
    }

    private float m_fixedDeltaTime;
    public float FixedDeltaTime
    {
        get { return m_fixedDeltaTime; }
    }

    private float m_elapsedTime = 0f;
    public float ElapsedTime
    {
        get { return m_elapsedTime; }
    }

    private float m_elapsedGameTime = 0f;
    public float ElapsedGameTime
    {
        get { return m_elapsedGameTime; }
    }

    public DateTime CurrentTime
    {
        get { return DateTime.UtcNow; }
    }

    public Action<float> OnDeltaTimeFactorChanged;
    
    private void Update()
    {
        m_deltaTime = Time.deltaTime * m_deltaTimeFactor;
        m_elapsedGameTime += m_deltaTime;
        m_elapsedTime += Time.deltaTime;
    }	

    private void FixedUpdate()
    {
        m_fixedDeltaTime = Time.fixedDeltaTime * m_deltaTimeFactor;
    }

    public void ResetDeltaTimeFactor()
    {
        m_deltaTimeFactor = 1f;
        if(OnDeltaTimeFactorChanged != null)
        {
            OnDeltaTimeFactorChanged(m_deltaTimeFactor);
        }
    }
}

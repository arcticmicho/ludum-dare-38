using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEffectController : MonoBehaviour
{
    [SerializeField]
    protected BaseEffect[] m_effects;

    [SerializeField]
    private bool m_playAtStart = false;

    protected bool m_isStarted;
    public bool IsStarted
    {
        get { return m_isStarted; }
    }

    protected bool m_isFinished;
    public bool IsFinished
    {
        get { return m_isFinished; }
    }

    private float m_delay;
    private float m_elapsedTime;
    private bool m_destroyAtFinish;

    public Action OnEffectFinished;

    private void Start()
    {
        if (m_playAtStart)
        {
            StartEffect(0, true);
        }
    }

    private void Update()
    {
        if (m_isFinished) return;

        m_elapsedTime += TimeManager.Instance.DeltaTime;
        if(!m_isStarted && m_elapsedTime >= m_delay)
        {
            m_isStarted = true;
            StartEffect();
        }

        bool finished = true;
        for(int i=0, count = m_effects.Length; i<count; i++)
        {
            finished &= m_effects[i].IsFinished;
        }

        if(finished)
        {
            m_isFinished = true;
            OnEffectFinished.SafeInvoke();
            if(m_destroyAtFinish)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }

    public void StartEffect(float delay = 0, bool destroyAtFinished = false)
    {
        m_destroyAtFinish = destroyAtFinished;
        m_delay = 0;
    }

    protected abstract void StartEffect();

    public abstract void StopEffect();

    protected abstract void UpateEffect();

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemEffect : BaseEffect
{
    [SerializeField]
    private ParticleSystemInfo[] m_particles;

    private bool m_emitting = false;
    private bool m_started = false;
    private float m_elapsedTime;

    public override void StartEffect()
    {
        m_started = true;
        m_elapsedTime = 0f;
        StartEmitting();
    }

    private void Update()
    {
        if (!m_started) return;

        m_elapsedTime += TimeManager.Instance.DeltaTime;
        if(m_emitting)
        {
            for(int i=0, count=m_particles.Length; i<count; i++)
            {
                if(m_particles[i].EmissionDuration != 0 && m_particles[i].System.isEmitting && m_particles[i].EmissionDuration < m_elapsedTime)
                {
                    m_particles[i].System.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                }
            }
            if(CheckEmissionStatus())
            {
                m_isFinished = true;
            }            
        }
    }

    private bool CheckEmissionStatus()
    {
        bool stopped = true;
        for (int i = 0, count = m_particles.Length; i < count; i++)
        {
            stopped &= !m_particles[i].System.isEmitting && m_particles[i].System.particleCount == 0;
        }
        return stopped;
    }

    private void StartEmitting()
    {
        m_emitting = true;
        for (int i=0, count = m_particles.Length; i<count;i++)
        {
            m_particles[i].System.Play(true);
        }
    }

    public override void StopEffect()
    {
        for (int i = 0, count = m_particles.Length; i < count; i++)
        {
            m_particles[i].System.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
        if(!m_started)
        {
            m_isFinished = true;
        }
    }

    [System.Serializable]
    public class ParticleSystemInfo
    {
        [SerializeField]
        private ParticleSystem m_system;
        public ParticleSystem System
        {
            get { return m_system; }
        }

        [SerializeField]
        private float m_emissionDuration;
        public float EmissionDuration
        {
            get { return m_emissionDuration; }
        }
    }
}

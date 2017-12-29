using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEffect : MonoBehaviour
{
    protected bool m_isFinished = false;
    public bool IsFinished
    {
        get { return m_isFinished; }
    }

    public abstract void StartEffect();

    public abstract void StopEffect();
}

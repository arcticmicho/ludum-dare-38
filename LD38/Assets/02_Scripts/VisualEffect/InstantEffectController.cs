using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantEffectController : BaseEffectController
{


    public override void StopEffect()
    {
        for (int i = 0, count = m_effects.Length; i < count; i++)
        {
            m_effects[i].StopEffect();
        }
    }

    protected override void StartEffect()
    {
        for(int i=0, count=m_effects.Length; i<count; i++)
        {
            m_effects[i].StartEffect();
        }
    }

    protected override void UpateEffect()
    {
        throw new System.NotImplementedException();
    }
}

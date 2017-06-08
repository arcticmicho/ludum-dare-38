using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoSingleton<EffectsManager>
{
    [SerializeField]
    private TrailRenderer m_trailRendererTemplate;

    public TrailRenderer RequestTrailRenderer()
    {
        return Instantiate<TrailRenderer>(m_trailRendererTemplate);
    }
}

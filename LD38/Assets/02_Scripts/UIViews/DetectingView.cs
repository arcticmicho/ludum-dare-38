using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectingView : UIView
{
    [SerializeField]
    private Slider m_timeSlider;

    public void SetTimerValue(float timePercent)
    {
        m_timeSlider.value = timePercent;
    }
	
}

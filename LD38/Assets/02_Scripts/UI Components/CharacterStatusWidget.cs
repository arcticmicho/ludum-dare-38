using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatusWidget : MonoBehaviour
{
    [SerializeField]
    private Text m_durationText;

    [SerializeField]
    private Slider m_durationSlider;

    private float m_statusDuration;

    [SerializeField]
    private Image m_skillImage;
    
    public void SetupCharacterStatusWidget(CharacterStatus status)
    {
        m_statusDuration = status.StatusDuration;
        if(m_durationText != null)
        {
            m_durationText.text = m_statusDuration.ToString();
        }
        if(m_durationSlider != null)
        {
            m_durationSlider.maxValue = m_statusDuration;
            m_durationSlider.value = m_statusDuration;
        }
        
        m_skillImage.sprite = status.StatusImage;
    }	

    private void Update()
    {
        if(m_statusDuration >= 0)
        {
            m_statusDuration = Mathf.Max(m_statusDuration - TimeManager.Instance.DeltaTime, 0f);
            if(m_durationText != null)
            {
                m_durationText.text = m_statusDuration.ToString();
            }            
            if (m_durationSlider != null)
            {
                m_durationSlider.value = m_statusDuration;
            }
        }
    }
}

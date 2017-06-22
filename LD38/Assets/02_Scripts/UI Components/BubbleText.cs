using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleText : MonoBehaviour
{
    [SerializeField]
    private Text m_text;

    [SerializeField]
    private Image m_backgroundImage;

    private bool m_initialized = false;

    private Sprite m_defaultBackgroundSprite;
    public Sprite DefaultBackgroundSprite
    {
        get
        {
            if (!m_initialized) SerDefaultValue();
            return m_defaultBackgroundSprite;
        }
    }
        
    private Color m_defaultBackgroundColor;
    public Color DefaultBackgroundColor
    {
        get
        {
            if (!m_initialized) SerDefaultValue();
            return m_defaultBackgroundColor;
        }
    }

    private float m_showTime;
    private float m_elapsedTime;
    private bool m_hideInTime;



    public void InitBubbleText(string text)
    {
        InitBubbleText(text, DefaultBackgroundSprite, DefaultBackgroundColor);
    }

    public void InitBubbleText(string text, Sprite background, Color backgroundColor)
    {
        InitBubbleText(text, false, 0f, background, backgroundColor);
    }

    public void InitBubbleTextInTime(string text, float showTime)
    {
        InitBubbleText(text, true, showTime, DefaultBackgroundSprite, DefaultBackgroundColor);
    }

    public void InitBubbleTextInTime(string text, float showTime, Sprite backgroundSprite, Color backgroundColor)
    {
        InitBubbleText(text, true, showTime, backgroundSprite, backgroundColor);
    }

    private void InitBubbleText(string text, bool showInTime, float showTime, Sprite backgroundSprite, Color backgroundColor)
    {
        if(!m_initialized)
        {
            m_initialized = true;
            SerDefaultValue();
        }
        m_hideInTime = showInTime;
        m_showTime = showTime;
        m_elapsedTime = 0f;

        ConfigureBubbleText(text, backgroundSprite, backgroundColor);
        SetVisible(true);
    }

    private void SerDefaultValue()
    {
        m_initialized = true;
        m_defaultBackgroundSprite = m_backgroundImage.sprite;
        m_defaultBackgroundColor = m_backgroundImage.color;        
    }

    private void ConfigureBubbleText(string text, Sprite backgroundSprite, Color backgroundColor)
    {
        m_text.text = text;
        m_backgroundImage.sprite = backgroundSprite;
        m_backgroundImage.color = backgroundColor;
    }

    public void SetVisible(bool visible)
    {
        gameObject.SetActive(visible);
        enabled = visible;
    }

    private void Update()
    {
        if(m_hideInTime)
        {
            m_elapsedTime += TimeManager.Instance.DeltaTime;
            if(m_elapsedTime >= m_showTime)
            {
                SetVisible(false);
            }
        }
    }

	
}

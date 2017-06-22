using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    [SerializeField]
    private Text m_text;
    [SerializeField]
    private Sprite m_backgroundSprite;
    [SerializeField]
    private float m_defaultTextDuration;
    [SerializeField]
    private float m_defaultTextSpeed;

    private float m_elapsedTime;
    private float m_textSpeed;
    private float m_textDuration;

    private float m_fadeOutSpeed;
    private float m_fadeOutAceleration;

    public void InitFloatingText(string text)
    {
        InitFloatingText(text, m_defaultTextDuration, m_defaultTextSpeed);
    }

    public void InitFloatingText(string text, float duration, float speed)
    {
        m_text.text = text;
        m_textDuration = duration;
        m_textSpeed = speed;

        m_elapsedTime = 0f;
        m_fadeOutSpeed = 0f;
        m_fadeOutAceleration = 1 / (m_textSpeed * m_textDuration);
    }

    private void Update()
    {
        m_elapsedTime += TimeManager.Instance.DeltaTime;

        if(m_elapsedTime <= m_textDuration)
        {
            transform.position += new Vector3(0f, m_textSpeed * TimeManager.Instance.DeltaTime, 0f);
            m_fadeOutSpeed += m_fadeOutAceleration;
            float newAlpha = Mathf.Lerp(m_text.color.a, 0, m_fadeOutSpeed * TimeManager.Instance.DeltaTime);
            m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b, newAlpha);
        }else
        {
            Destroy(gameObject);   
        }
    }

}

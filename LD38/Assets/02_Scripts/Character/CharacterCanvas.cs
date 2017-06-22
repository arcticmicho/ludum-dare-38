using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CharacterCanvas : MonoBehaviour
{
    [SerializeField]
    private FloatingText m_floatingTextTemplate;
    [SerializeField]
    private BubbleText m_bubbleText;

    public void AddFlotingText(string text)
    {
        FloatingText newFloatingText = Instantiate<FloatingText>(m_floatingTextTemplate);
        newFloatingText.transform.SetParent(transform);
        newFloatingText.transform.localPosition = Vector3.zero;
        newFloatingText.InitFloatingText(text);
    }

    public void ShowBubbleText(string text)
    {
        m_bubbleText.InitBubbleText(text);
    }

    public void ShowBubbleText(string text, Sprite background, Color color)
    {
        m_bubbleText.InitBubbleText(text, background, color);
    }

    public void ShowBubbleText(string text, float timeToShow)
    {
        m_bubbleText.InitBubbleTextInTime(text, timeToShow);
    }

    public void ShowBubbleText(string text, float timeToShow, Sprite background, Color color)
    {
        m_bubbleText.InitBubbleTextInTime(text, timeToShow, background, color);
    }
}

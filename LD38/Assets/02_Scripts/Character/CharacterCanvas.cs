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
    [SerializeField]
    private float m_floatingTextCooldown = 0.2f;
    private float m_elapsedTime;

    private Queue<FloatingTextRequest> m_requests = new Queue<FloatingTextRequest>();

    public void RequestFlotingText(string text)
    {
        m_requests.Enqueue(new FloatingTextRequest(text));
    }

    private void GenerateFloatingText(string text)
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

    private void Update()
    {
        if(m_elapsedTime >= m_floatingTextCooldown)
        {
            if(m_requests.Count > 0)
            {
                GenerateFloatingText(m_requests.Dequeue().RequestedText);
                m_elapsedTime = 0f;
            }
        }else
        {
            m_elapsedTime += TimeManager.Instance.DeltaTime;
        }
    }

    private class FloatingTextRequest
    {
        private string m_requestedText;
        public string RequestedText
        {
            get { return m_requestedText; }
        }

        public FloatingTextRequest(string text)
        {
            m_requestedText = text;
        }
    }
}

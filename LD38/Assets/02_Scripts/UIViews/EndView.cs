using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndView : UIView
{
    [SerializeField]
    private Text m_endText;

    public void SetEndText(string text)
    {
        m_endText.text = text;
    }

    public void OnRestartButtonPressed()
    {
        GameManager.Instance.RequestGameSession();
    }
    	
}

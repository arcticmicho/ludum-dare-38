using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndView : UIView
{
    [SerializeField]
    private Text m_endText;

    public System.Action OnOkPressed;

    public void SetEndText(string text)
    {
        m_endText.text = text;
    }

    public void OnRestartButtonPressed()
    {        
        if(OnOkPressed != null)
        {
            OnOkPressed();
        }
    }

    public static void RequestEndView(System.Action onOkPressed)
    {
      /*  UIPartyManager.Instance.GetView<EndView>().OnOkPressed = null;
        UIPartyManager.Instance.RequestView<EndView>();
        UIPartyManager.Instance.GetView<EndView>().OnOkPressed += onOkPressed;*/
    }
    	
}

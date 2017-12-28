using GameModules;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPopup : UIPanel
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
        //UIPartyManager.Instance.GetView<EndHUD>().OnOkPressed = null;
        //UIPartyManager.Instance.RequestView<EndHUD>();
        //UIPartyManager.Instance.GetView<EndHUD>().OnOkPressed += onOkPressed;
        UIManager.Instance.HideHUD();
        EndPopup hud = UIManager.Instance.RequestPopup<EndPopup>(UIPanelPriority.Medium, UIPanelFlags.HideHUD);
        hud.Show();
        hud.OnOkPressed = onOkPressed;
    }
    	
}

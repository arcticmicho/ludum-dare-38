using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WizardWidget : MonoBehaviour
{
    [SerializeField]
    private Text m_wizardName;

    [SerializeField]
    private Text m_level;

    [SerializeField]
    private Button m_selectButton;


    public void SetupWidget(WizardData wizard)
    {
        m_wizardName.text = wizard.Name;
        m_selectButton.onClick.RemoveAllListeners();
        m_selectButton.onClick.AddListener(() => GameManager.Instance.SetSelectedWizard(wizard));
    }
}

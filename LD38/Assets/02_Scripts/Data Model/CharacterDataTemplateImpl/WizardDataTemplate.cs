using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wizard Data Template", menuName = "GamePlay/Characters/Wizard")]
public class WizardDataTemplate : CharacterTemplate, IWizardData
{
    [SerializeField]
    private int m_level;
    public int Level
    {
        get { return m_level; }
        set { }
    }


    [SerializeField]
    private int m_exp;
    public int Exp
    {
        get { return m_exp; }
        set { }
    }

    [SerializeField]
    private string m_wizardId;
    public string WizardID
    {
        get { return m_wizardId; }
        set { }
    }

}

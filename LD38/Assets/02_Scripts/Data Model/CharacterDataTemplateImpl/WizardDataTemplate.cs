using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wizard Data Template", menuName = "GamePlay/Characters/Wizard")]
public class WizardDataTemplate : CharacterTemplate, IWizardData
{
    [SerializeField]
    private int m_level;

    [SerializeField]
    private int m_exp;

    #region Get/Set
    public int Level
    {
        get { return m_level; }
        set { }
    }

    public int Exp
    {
        get { return m_exp; }
        set { }
    }
    #endregion

    #region Editor Functions and export

    public override void Editor_SetExportData(Dictionary<string, object> exportData)
    {
        base.Editor_SetExportData(exportData);
    }

    #endregion
}

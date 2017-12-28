using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateData
{
    private RoomSessionData m_sessionData;

    private WizardData m_wizardData;

    #region Get/Set
    public RoomSessionData SessionData
    {
        get { return m_sessionData; }
    }

    public WizardData WizardData
    {
        get { return m_wizardData; }
    }
    #endregion

    public GameStateData(RoomSessionData sessionData, WizardData wizardData)
    {
        m_sessionData = sessionData;
        m_wizardData = wizardData;
    }
    
}

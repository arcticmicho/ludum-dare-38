using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateData
{
    private RoomSessionData m_sessionData;
    public RoomSessionData SessionData
    {
        get
        {
            return m_sessionData;
        }
    }

    private WizardData m_wizardData;
    public WizardData WizardData
    {
        get { return m_wizardData; }
    }

    public GameStateData(RoomSessionData sessionData, WizardData wizardData)
    {
        m_sessionData = sessionData;
        m_wizardData = wizardData;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameSerializer
{
    private PlayerData m_playerData;
    public PlayerData PlayerData
    {
        get { return m_playerData; }
    }

    private GameData m_gameData;
    public GameData GameData
    {
        get { return m_gameData; }
    }

    public GameSerializer()
    {
        m_playerData = new PlayerData(this);
    }

    public void SerializeData()
    {
        Dictionary<string, object> gameData = new Dictionary<string, object>();

        gameData.Add("PlayerData", m_playerData.SerializeObject());
        gameData.Add("GameData", m_gameData.SerializeObject());
        SerializeUtils.ESerializeError error;
        if(SaveGame(gameData, "Save1", out error))
        {
            Debug.Log("SerialiazeSuccess");
            m_isDirty = false;
        }else
        {
            Debug.LogError(error.ToString());
        }
    }

    public void DeserializeData()
    {
        Dictionary<string, object> gameData;
        SerializeUtils.ESerializeError error;
        if (LoadGame("Save1", out gameData, out error))
        {
            LoadData(gameData);
        }
        else
        {
            if(error == SerializeUtils.ESerializeError.FileNotFound)
            {
                //Initializating the game for the very first time. We need to create the default data.
                CreateDataFromScratch();
            }
            Debug.LogError(error.ToString());
        }
    }

    private void LoadData(Dictionary<string, object> data)
    {
        m_playerData = new PlayerData(this);
        m_gameData = new GameData(this);
        m_playerData.DeserializeObject(data["PlayerData"] as Dictionary<string,object>);
        m_gameData.DeserializeObject(data["GameData"] as Dictionary<string, object>);
    }

    private void CreateDataFromScratch()
    {
        m_playerData = new PlayerData(this);
        m_playerData.PlayerLevel = 1;
        
        foreach(WizardDataTemplate template in ResourceManager.Instance.CharacterTemplates.InitialWizard)
        {
            m_playerData.WizardsData.Add(new WizardData(this, template));
        }

        m_gameData = new GameData(this);
    }
}

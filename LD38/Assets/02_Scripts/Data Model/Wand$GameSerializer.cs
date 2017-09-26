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

    public GameSerializer()
    {
        m_playerData = new PlayerData(this);
    }

    public void SerializeData()
    {
        Dictionary<string, object> gameData = new Dictionary<string, object>();

        gameData.Add("PlayerData", m_playerData.SerializeObject());
        SerializeUtils.ESerializeError error;
        if(SaveGame(gameData, "Save1", out error))
        {
            Debug.Log("SerialiazeSuccess");
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
            Debug.LogError(error.ToString());
        }
    }

    private void LoadData(Dictionary<string,object> data)
    {

    }
}

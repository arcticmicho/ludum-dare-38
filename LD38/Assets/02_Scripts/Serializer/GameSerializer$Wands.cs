using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameSerializer
{
    private PlayerData m_playerData;
    private GameData m_gameData;

    public PlayerData PlayerData
    {
        get { return m_playerData; }
    }

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
        if(SaveGame(gameData, kDefaultSaveName, out error))
        {
            Debug.Log("SerialiazeSuccess");
            m_isDirty = false;
        }else
        {
            Debug.LogError(error.ToString());
        }
    }

    public IEnumerator DeserializeData()
    {
        Dictionary<string, object> gameData;
        SerializeUtils.ESerializeError error;

        //TODO: Add other kinds of Load Game (maybe cloud save?)
        if (LoadGame(kDefaultSaveName, out gameData, out error))
        {
            LoadData(gameData);
            m_isNewGame = false;
        }
        else
        {
            if(error == SerializeUtils.ESerializeError.FileNotFound)
            {
                //Initializating the game for the very first time. We need to create the default data.
                CreateDataFromScratch();
                m_isNewGame = true;
            }
            Debug.LogError(error.ToString());
        }
        yield return null;
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
            WizardData newWizardData = new WizardData(this, template);
            m_playerData.WizardsData.Add(newWizardData);
            if(template.WeaponTemplate != null)
            {
                EquippableItemInstance newWeapon = new EquippableItemInstance(this, template.WeaponTemplate);
                newWizardData.EquipWeapon(newWeapon);
                newWeapon.Equip(newWizardData);
                m_playerData.EquippableItems.Add(newWeapon);
            }
        }

        foreach(ConsumableItemTemplate template in ResourceManager.Instance.InventoryResources.InitialConsumables)
        {
            m_playerData.ConsumableItems.Add(new ConsumableItemInstance(this, template));
        }

        m_gameData = new GameData(this);
        m_gameData.SelectedWizardId = m_playerData.WizardsData[0].WizardTemplateId;

        m_gameData.SelectRoomId = ResourceManager.Instance.RoomTemplateResources.RoomSessionTemplates[0].RoomSessionId;
    }
}

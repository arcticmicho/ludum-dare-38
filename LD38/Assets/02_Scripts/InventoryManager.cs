using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    private PlayerData m_playerData;

	public void Initialize()
    {
        m_playerData = GameManager.Instance.Serializer.PlayerData;
    }

    public void CreateEquippableItem(EquippableItemTemplate template)
    {
        EquippableItemInstance newInstance = new EquippableItemInstance(GameManager.Instance.Serializer, template);
        m_playerData.EquippableItems.Add(newInstance);
        GameManager.Instance.Serializer.SetDirty();
    }

    public void CreateConsumableItem(ConsumableItemTemplate template)
    {
        ConsumableItemInstance newInstance = new ConsumableItemInstance(GameManager.Instance.Serializer, template);
        m_playerData.ConsumableItems.Add(newInstance);
        GameManager.Instance.Serializer.SetDirty();
    }

    public void RemoveEquippableItem(EquippableItemInstance instance)
    {
        if(m_playerData.EquippableItems.Contains(instance))
        {
            if(instance.IsEquipped)
            {
                WizardData wizard = CharactersManager.Instance.GetWizardWithId(instance.CharacterOwnerId);
                wizard.UnequipWeapon();
            }
            m_playerData.EquippableItems.Remove(instance);
            GameManager.Instance.Serializer.SetDirty();
        }
    }

    public EquippableItemInstance GetEquippableItemInstance(string instanceId)
    {
        EquippableItemInstance instance = m_playerData.EquippableItems.Find(x => string.Equals(x, instanceId));
        return instance;
    }

    public ConsumableItemInstance GetConsumableItemInstance(string instanceId)
    {
        return m_playerData.ConsumableItems.Find(x => string.Equals(x, instanceId));
    }

    public List<EquippableItemInstance> GetAllEquippableItems()
    {
        return m_playerData.EquippableItems;
    }

    public List<ConsumableItemInstance> GetAllConsumableItems()
    {
        return m_playerData.ConsumableItems;
    }
}

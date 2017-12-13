using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryResources
{
    [SerializeField]
    private List<EquippableItemTemplate> m_equippableItems;

    [SerializeField]
    private List<ConsumableItemTemplate> m_consumableItems;


    [SerializeField]
    private List<ConsumableItemTemplate> m_initialConsumableItems;
    public List<ConsumableItemTemplate> InitialConsumables
    {
        get { return m_initialConsumableItems; }
    }

    private Dictionary<string, EquippableItemTemplate> m_equippableItemsDict;
    private Dictionary<string, ConsumableItemTemplate> m_consumableItemsDict;

	public void Initialize()
    {
        m_equippableItemsDict = new Dictionary<string, EquippableItemTemplate>();
        m_consumableItemsDict = new Dictionary<string, ConsumableItemTemplate>();

        for(int i=0, count=m_equippableItems.Count; i<count; i++)
        {
            m_equippableItemsDict.Add(m_equippableItems[i].ItemTemplateId, m_equippableItems[i]);
        }

        for(int i=0, count=m_consumableItems.Count; i<count; i++)
        {
            m_consumableItemsDict.Add(m_consumableItems[i].ItemTemplateId, m_consumableItems[i]);
        }
    }

    public EquippableItemTemplate GetEquippableTemplateWithId(string templateId)
    {
        EquippableItemTemplate template;
        if(m_equippableItemsDict.TryGetValue(templateId, out template))
        {
            return template;
        }else
        {
            Debug.LogError("Equippable Template not found with Id: " + templateId);
            return null;
        }
    }

    public ConsumableItemTemplate GetConsumableTemplateWithId(string templateId)
    {
        ConsumableItemTemplate template;
        if (m_consumableItemsDict.TryGetValue(templateId, out template))
        {
            return template;
        }
        else
        {
            Debug.LogError("Consumable Template not found with Id: " + templateId);
            return null;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : SerializableObject
{
    private SerializableProperty<int> m_playerLevel;
    public int PlayerLevel
    {
        get { return m_playerLevel.Property; }
        set { m_playerLevel.Property = value; }
    }

    private SerializableProperty<PlayerCurrency> m_currency;
    private SerializableProperty<List<WizardData>> m_wizardData;
    public List<WizardData> WizardsData
    {
        get { return m_wizardData; }
    }

    public PlayerData(GameSerializer serializer) : base(serializer)
    {
        m_currency = new SerializableProperty<PlayerCurrency>(serializer, new PlayerCurrency(m_serializer));
        m_playerLevel = new SerializableProperty<int>(serializer, 0);
        m_wizardData = new SerializableProperty<List<WizardData>>(serializer, new List<WizardData>());
    }

    public override void DeserializeObject(Dictionary<string, object> dict)
    {
        if (dict.ContainsKey("PlayerLevel"))
        {
            m_playerLevel = new SerializableProperty<int>(m_serializer, int.Parse(dict["PlayerLevel"].ToString()));
        }
        if(dict.ContainsKey("Currency"))
        {
            m_currency = new SerializableProperty<PlayerCurrency>(m_serializer, new PlayerCurrency(m_serializer));
            m_currency.Property.DeserializeObject(dict["Currency"] as Dictionary<string, object>);
        }

        if(dict.ContainsKey("Wizards"))
        {
            List<object> wizardsData = dict["Wizards"] as List<object>;
            m_wizardData = new SerializableProperty<List<WizardData>>(m_serializer, new List<WizardData>());
            for(int i=0, count = wizardsData.Count; i<count;i++)
            {
                WizardData newWizardData = new WizardData(m_serializer);
                newWizardData.DeserializeObject(wizardsData[i] as Dictionary<string, object>);
                m_wizardData.Property.Add(newWizardData);
            }
        }
    }

    public override Dictionary<string, object> SerializeObject()
    {
        Dictionary<string, object> newDict = new Dictionary<string, object>();
        newDict.Add("PlayerLevel", m_playerLevel.Property);
        newDict.Add("Currency", m_currency.Property.SerializeObject());

        List<Dictionary<string, object>> wizardData = new List<Dictionary<string, object>>();

        for(int i=0,count = m_wizardData.Property.Count; i<count; i++)
        {
            wizardData.Add(m_wizardData.Property[i].SerializeObject());
        }
        newDict.Add("Wizards", wizardData);
        return newDict;
    }
}

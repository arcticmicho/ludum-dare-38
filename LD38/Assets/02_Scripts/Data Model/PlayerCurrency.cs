using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrency : SerializableObject
{
    private SerializableProperty<List<CurrencyData>> m_currenciesData;

    public PlayerCurrency(GameSerializer serializer) : base(serializer)
    {
        m_currenciesData = new SerializableProperty<List<CurrencyData>>(serializer, new List<CurrencyData>());
    }

    public override void DeserializeObject(Dictionary<string, object> dict)
    {
        
    }

    public override Dictionary<string, object> SerializeObject()
    {
        Dictionary<string, object> newDict = new Dictionary<string, object>();
        List<Dictionary<string, object>> currenciesData = new List<Dictionary<string, object>>();
        
        for(int i=0,count = m_currenciesData.Property.Count; i< count; i++)
        {
            currenciesData.Add(m_currenciesData.Property[i].SerializeObject());
        }
        newDict.Add("Currencies", currenciesData);
        return newDict;
    }
}

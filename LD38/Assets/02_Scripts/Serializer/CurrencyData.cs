using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECurrencyType
{
    Soft = 0,
    Hard = 1
}

public class CurrencyData : SerializableObject
{
    private SerializableProperty<int> m_amount;
    private SerializableProperty<ECurrencyType> m_type;

    public CurrencyData(GameSerializer serializer) : base(serializer)
    {

    }

    public override void DeserializeObject(Dictionary<string, object> dict)
    {
        
    }

    public override Dictionary<string, object> SerializeObject()
    {
        Dictionary<string, object> newDict = new Dictionary<string, object>();
        newDict.Add("amount", m_amount.Property);
        newDict.Add("type", m_type.Property.ToString());
        return newDict;
    }

}

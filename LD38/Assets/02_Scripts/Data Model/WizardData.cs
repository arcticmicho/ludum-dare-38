using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardData : SerializableObject
{
    private SerializableProperty<int> m_wizardLevel;
    private SerializableProperty<string> m_wizardTemplateId;

    public int WizardLevel
    {
        get { return m_wizardLevel.Property; }
    }

    public string WizardTemplateId
    {
        get { return m_wizardTemplateId.Property; }
    }

    public WizardData(GameSerializer serializer) : base(serializer)
    {
    }

    public override void DeserializeObject(Dictionary<string, object> dict)
    {
        throw new NotImplementedException();
    }

    public override Dictionary<string, object> SerializeObject()
    {
        Dictionary<string, object> newDict = new Dictionary<string, object>();
        newDict.Add("WizardLevel", m_wizardLevel.Property);
        newDict.Add("WizardTemplateId", m_wizardTemplateId.Property);
        return newDict;
    }
}

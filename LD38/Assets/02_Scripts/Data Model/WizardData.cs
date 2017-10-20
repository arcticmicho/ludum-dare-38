using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardData : SerializableObject
{
    private SerializableProperty<int> m_wizardLevel;
    private SerializableProperty<float> m_wizardExp;
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
        if (dict.ContainsKey("WizardLevel"))
        {
            m_wizardLevel = new SerializableProperty<int>(m_serializer, int.Parse(dict["WizardLevel"] as string));
        }
        if (dict.ContainsKey("WizardTemplateId"))
        {
            m_wizardTemplateId = new SerializableProperty<string>(m_serializer, dict["WizardTemplateId"] as string);
        }
        if (dict.ContainsKey("WizardExp"))
        {
            m_wizardExp = new SerializableProperty<float>(m_serializer, float.Parse(dict["WizardExp"] as string));
        }
    }

    public override Dictionary<string, object> SerializeObject()
    {
        Dictionary<string, object> newDict = new Dictionary<string, object>();
        newDict.Add("WizardLevel", m_wizardLevel.Property);
        newDict.Add("WizardTemplateId", m_wizardTemplateId.Property);
        newDict.Add("WizardExp", m_wizardExp);
        return newDict;
    }
}

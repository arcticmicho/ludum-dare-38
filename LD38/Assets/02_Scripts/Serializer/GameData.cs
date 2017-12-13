using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : SerializableObject
{
    private SerializableProperty<string> m_selectedWizardId;
    public string SelectedWizardId
    {
        get { return m_selectedWizardId; }
        set { m_selectedWizardId.Property = value; }
    }

    private SerializableProperty<string> m_selectedRoomId;
    public string SelectRoomId
    {
        get { return m_selectedRoomId; }
        set
        {
            if (m_selectedRoomId == value) return;
            m_selectedRoomId.Property = value;
        }
    }

    public GameData(GameSerializer serializer) : base(serializer)
    {
        m_selectedRoomId = new SerializableProperty<string>(serializer, string.Empty);
        m_selectedWizardId = new SerializableProperty<string>(serializer, string.Empty);
    }

    public override void DeserializeObject(Dictionary<string, object> dict)
    {
        m_selectedWizardId.Property = DictionaryUtils.TryParseValue(dict, "selectedWizardId", string.Empty);
        m_selectedRoomId.Property = DictionaryUtils.TryParseValue(dict, "selectedRoomId", string.Empty);
    }

    public override Dictionary<string, object> SerializeObject()
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        dict.Add("selectedWizardId", m_selectedWizardId.Property);
        dict.Add("selectedRoomId", m_selectedRoomId.Property);
        return dict;
    }

}

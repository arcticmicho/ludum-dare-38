using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemInstance : SerializableObject
{
    protected Guid m_instanceIdGuid;
    public Guid InstanceGuid
    {
        get
        {
            return m_instanceIdGuid;
        }
    }

    protected SerializableProperty<string> m_instanceId;
    public string InstanceId
    {
        get { return m_instanceId; }
    }

    protected SerializableProperty<string> m_itemTemplateId;
    public string ItemTemplateId
    {
        get { return m_itemTemplateId; }
    }

    public ItemInstance(GameSerializer serializer) : base(serializer)
    {

    }

    public ItemInstance(GameSerializer serializer, string itemDefId) : base(serializer)
    {
        m_instanceIdGuid = Guid.NewGuid();
        m_instanceId = new SerializableProperty<string>(serializer, m_instanceIdGuid.ToString());
        m_itemTemplateId = new SerializableProperty<string>(serializer, itemDefId);
    }

    public override void DeserializeObject(Dictionary<string, object> dict)
    {
        m_instanceId = new SerializableProperty<string>(m_serializer, DictionaryUtils.TryParseValue<string>(dict, "InstanceId", string.Empty));
        m_itemTemplateId = new SerializableProperty<string>(m_serializer, DictionaryUtils.TryParseValue<string>(dict, "ItemDefId", string.Empty));
    }

    public override Dictionary<string, object> SerializeObject()
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();

        dict.Add("InstanceId", m_instanceId);
        dict.Add("ItemDefId", m_itemTemplateId);

        return dict;
    }
}

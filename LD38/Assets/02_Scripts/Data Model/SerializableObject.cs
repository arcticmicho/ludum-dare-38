using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SerializableObject
{
    protected GameSerializer m_serializer;
    public GameSerializer Serializer
    {
        set { m_serializer = value; }
    }

    public SerializableObject(GameSerializer serializer)
    {
        m_serializer = serializer;
    }

    public abstract Dictionary<string, object> SerializeObject();
    public abstract void DeserializeObject(Dictionary<string, object> dict);
}

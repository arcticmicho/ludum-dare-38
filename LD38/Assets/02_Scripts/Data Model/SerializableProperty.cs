using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializableProperty<T>
{
    private T m_property;
    private GameSerializer m_contextSerializer;

    public T Property
    {
        get
        {
            return m_property;
        }
        set
        {
            m_property = value;
            SetDirty();
        }
    }

    public SerializableProperty(GameSerializer serializer, T value)
    {
        m_property = value;
        m_contextSerializer = serializer;
    }

    public void SetDirty()
    {
        if(m_contextSerializer != null)
        {
            m_contextSerializer.SetDirty();
        }
    }

    public static implicit operator T(SerializableProperty<T> property)
    {
        return property.Property;
    }
}

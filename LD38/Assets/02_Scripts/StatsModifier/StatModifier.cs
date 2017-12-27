using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier<T>
{
    public delegate T ModifierAddOperation(T modifier1, T modifier2);

    private ModifierAddOperation m_addOperation;

    private List<Modifier<T>> m_modifiers = new List<Modifier<T>>();

    public bool HasModifiers
    {
        get { return m_modifiers.Count > 0; }
    }

    private T m_totalValue;
    public T TotalModifier
    {
        get { return m_totalValue; }
    }

    public StatModifier(ModifierAddOperation operation, T defaulTotaltValue)
    {
        m_addOperation = operation;
        m_totalValue = defaulTotaltValue;
    }

    public Modifier<T> AddModifier(T value)
    {
        Modifier<T> newMod = new Modifier<T>(value);
        m_modifiers.Add(newMod);
        CalculateNewTotalValue();
        return newMod;
    }

    public bool RemoveModifier(Modifier<T> modifier)
    {
        bool removed = m_modifiers.Remove(modifier);
        CalculateNewTotalValue();
        return removed;
    }

    private void CalculateNewTotalValue()
    {
        m_totalValue = default(T);

        for(int i=0, count=m_modifiers.Count; i<count; i++)
        {
            m_totalValue = m_addOperation(m_totalValue, m_modifiers[i].ModifierValue);
        }
    }
	
}

public class Modifier<T>
{
    private T m_modifierValues;
    public T ModifierValue
    {
        get { return m_modifierValues; }
    }

    public Modifier(T value)
    {
        m_modifierValues = value;
    }
}

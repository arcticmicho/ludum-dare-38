using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DamageTable
{
    [SerializeField]
    private List<DamageTypeValue> m_damage;

    public List<DamageTypeValue> DamageInfo
    {
        get { return m_damage; }
    }

    public DamageTable()
    {
        m_damage = new List<DamageTypeValue>();
    }

    public DamageTable(List<DamageTypeValue> values)
    {
        m_damage = new List<DamageTypeValue>(values);
    }

    public static DamageTable operator +(DamageTable tab1, DamageTable tab2)
    {
        DamageTable newTable = tab1.Clone();
        float temporaryDamage = 0;
        for(int i=0, count= tab2.DamageInfo.Count; i<count; i++)
        {
            newTable.GetDamage(tab2.DamageInfo[i].DamageType, out temporaryDamage);
            newTable.AddOrUpdateDamageType(tab2.DamageInfo[i].DamageType, temporaryDamage + tab2.DamageInfo[i].DamageValue);
        }
        return newTable;
    }

    public static DamageTable operator -(DamageTable tab1, DamageTable tab2)
    {
        DamageTable newTable = tab1.Clone();
        float temporaryDamage = 0;
        for (int i = 0, count = tab2.DamageInfo.Count; i < count; i++)
        {
            newTable.GetDamage(tab2.DamageInfo[i].DamageType, out temporaryDamage);
            newTable.AddOrUpdateDamageType(tab2.DamageInfo[i].DamageType, temporaryDamage - tab2.DamageInfo[i].DamageValue);
        }
        return newTable;
    }

    public static DamageTable operator *(DamageTable tab1, DamageTable tab2)
    {
        DamageTable newTable = new DamageTable();
        float temporaryDamage = 0;
        for (int i = 0, count = tab2.DamageInfo.Count; i < count; i++)
        {
            if(tab1.GetDamage(tab2.DamageInfo[i].DamageType, out temporaryDamage))
            {
                newTable.AddOrUpdateDamageType(tab2.DamageInfo[i].DamageType, temporaryDamage * tab2.DamageInfo[i].DamageValue);
            }            
        }
        return newTable;
    }

    public static DamageTable operator *(DamageTable tab1, float value)
    {
        DamageTable newTable = new DamageTable();
        for (int i = 0, count = tab1.DamageInfo.Count; i < count; i++)
        {
            newTable.AddOrUpdateDamageType(tab1.DamageInfo[i].DamageType, value * tab1.DamageInfo[i].DamageValue);
        }
        return newTable;
    }

    public static DamageTable operator /(DamageTable tab1, DamageTable tab2)
    {
        DamageTable newTable = new DamageTable();
        float temporaryDamage = 0;
        for (int i = 0, count = tab2.DamageInfo.Count; i < count; i++)
        {
            if (tab1.GetDamage(tab2.DamageInfo[i].DamageType, out temporaryDamage))
            {
                newTable.AddOrUpdateDamageType(tab2.DamageInfo[i].DamageType, temporaryDamage / tab2.DamageInfo[i].DamageValue);
            }
        }
        return newTable;
    }

    public void AddOrUpdateDamageType(DamageType type, float damageValue)
    {
        for(int i=0, count=m_damage.Count; i<count; i++)
        {
            if(m_damage[i].DamageType == type)
            {
                m_damage.RemoveAt(i);
                break;
            }
        }
        m_damage.Add(new DamageTypeValue(type, damageValue));
    }

    public bool GetDamage(DamageType type, out float value)
    {
        for (int i = 0, count = m_damage.Count; i < count; i++)
        {
            if (m_damage[i].DamageType == type)
            {
                value = m_damage[i].DamageValue;
                return true;
            }
        }
        value = 0;
        return false;
    }

    public DamageTable Clone()
    {
        return new DamageTable(m_damage);
    }   	
}

[Serializable]
public class DamageTypeValue
{
    [SerializeField]
    private DamageType m_damageType;
    [SerializeField]
    private float m_damageValue;

    public DamageType DamageType
    {
        get { return m_damageType; }
    }

    public float DamageValue
    {
        get { return m_damageValue; }
    }

    public DamageTypeValue(DamageType type, float value)
    {
        m_damageType = type;
        m_damageValue = value;
    }
}

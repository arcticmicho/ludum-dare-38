using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageTypeResources
{

    [SerializeField]
    private List<DamageType> m_damageTypes;

    private Dictionary<string, DamageType> m_damageTypesDict;
    public void Initialize()
    {
        m_damageTypesDict = new Dictionary<string, DamageType>();
        for(int i=0; i<m_damageTypes.Count; i++)
        {
            m_damageTypesDict.Add(m_damageTypes[i].DamageTypeName, m_damageTypes[i]);
        }
    }

    public DamageType GetDamageTypeByName(string name)
    {
        DamageType damageType = null;
        m_damageTypesDict.TryGetValue(name, out damageType);
        return damageType;
    }
}

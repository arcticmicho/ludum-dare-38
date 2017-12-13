using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageType", menuName = "GamePlay/DamageType/DamageType")]
public class DamageType : ScriptableObject
{
    [SerializeField]
    private string m_damageTypeName;

    [SerializeField]
    private string m_damageTypeDescription;

    [SerializeField]
    private Texture m_damageTypeTexture;

    public string DamageTypeName
    {
        get { return m_damageTypeName; }
    }

    public string DamageTypeDescription
    {
        get { return m_damageTypeDescription; }
    }

    public Texture DamageTypeIcon
    {
        get { return m_damageTypeTexture; }
    }
	
}

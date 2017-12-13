using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECharacterStat
{
    HealthPoints = 1,
    SpellDamage = 2,
    SpellResistance = 4,
    CastingTime = 8
}

[CreateAssetMenu(fileName = "EquippableItem", menuName = "GamePlay/Items/ConsumableItem")]
public class ConsumableItemTemplate : ItemTemplate
{
    [SerializeField]
    private ECharacterStat m_statToModify;
    public ECharacterStat StatToModify
    {
        get { return m_statToModify; }
    }

    [SerializeField]
    private float m_modifierValue = 1f;
    public float ModifierValue
    {
        get { return m_modifierValue; }
    }

    [SerializeField]
    private float m_modifierSecondsTicks = 3f;
    public float ModifierSecondsTick
    {
        get { return m_modifierSecondsTicks; }
    }

    [SerializeField]
    private int m_modifierNumberOfTicks = 1;
    public int ModifierNumberOfTicks
    {
        get { return m_modifierNumberOfTicks; }
    }
    	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquippableItem", menuName = "GamePlay/Items/ConsumableItem")]
public class ConsumableItemTemplate : ItemTemplate
{

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

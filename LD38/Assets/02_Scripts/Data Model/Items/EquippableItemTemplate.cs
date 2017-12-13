using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquippableItem", menuName = "GamePlay/Items/EquippableItem")]
public class EquippableItemTemplate : ItemTemplate
{
    [SerializeField]
    private GameObject m_equippablePrefab;

    [SerializeField, Range(0f,1f)]
    private float m_itemDamageSpellIncreasePercent;
}

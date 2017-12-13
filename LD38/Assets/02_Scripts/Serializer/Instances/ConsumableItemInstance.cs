using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItemInstance : ItemInstance
{
    public ConsumableItemInstance(GameSerializer serializer) : base(serializer)
    {

    }

    public ConsumableItemInstance(GameSerializer serializer, ConsumableItemTemplate itemTemplate) : base(serializer, itemTemplate.ItemTemplateId)
    {

    }
}

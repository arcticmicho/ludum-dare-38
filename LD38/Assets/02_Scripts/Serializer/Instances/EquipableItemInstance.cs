using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippableItemInstance : ItemInstance
{
    private SerializableProperty<string> m_characterOwnerId;
    public string CharacterOwnerId
    {
        get { return m_characterOwnerId; }
    }

    public bool IsEquipped
    {
        get { return !string.IsNullOrEmpty(m_characterOwnerId); }
    }


    public EquippableItemTemplate Template
    {
        get
        {
            return ResourceManager.Instance.InventoryResources.GetEquippableTemplateWithId(m_itemTemplateId);
        }
    }

    public EquippableItemInstance(GameSerializer serializer) : base(serializer)
    {

    }

    public EquippableItemInstance(GameSerializer serializer, EquippableItemTemplate template) : base(serializer, template.ItemTemplateId)
    {

    }

    public void Unequip()
    {
        if(!string.IsNullOrEmpty(m_characterOwnerId))
        {
            m_characterOwnerId.Property = string.Empty;
        }
    }

    public void Equip(WizardData wizard)
    {
        m_characterOwnerId.Property = wizard.WizardTemplateId;
    }

    public override Dictionary<string, object> SerializeObject()
    {
        Dictionary<string, object> dict = base.SerializeObject();
        dict.Add("OwnerId", m_characterOwnerId);

        return base.SerializeObject();
    }

    public override void DeserializeObject(Dictionary<string, object> dict)
    {
        base.DeserializeObject(dict);
        m_characterOwnerId = new SerializableProperty<string>(m_serializer, DictionaryUtils.TryParseValue<string>(dict, "OwnerId", string.Empty));
    }

}

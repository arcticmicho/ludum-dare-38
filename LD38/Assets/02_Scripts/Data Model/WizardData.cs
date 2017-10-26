using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardData : SerializableObject, IWizardData
{
    private SerializableProperty<string> m_wizardName;
    private SerializableProperty<string> m_wizardId;
    private SerializableProperty<int> m_wizardLevel;
    private SerializableProperty<int> m_wizardExp;
    private SerializableProperty<string> m_wizardTemplateId;
    private SerializableProperty<float> m_healthPoints;
    private SerializableProperty<float> m_movementSpeed;
    private List<SkillDefinition> m_skills;
    private DamageTable m_resistanceTable; 

    public int WizardLevel
    {
        get { return m_wizardLevel; }
    }

    public string WizardTemplateId
    {
        get { return m_wizardTemplateId; }
    }

    public string WizardID
    {
        get
        {
            return m_wizardId;
        }
    }

    public int Level
    {
        get
        {
            return m_wizardLevel;
        }
    }

    public int Exp
    {
        get
        {
            return m_wizardExp;
        }
    }

    public string Name
    {
        get
        {
            return m_wizardName;
        }
    }

    public float HealthPoints
    {
        get
        {
            return m_healthPoints;
        }
    }

    public DamageTable ResistanceTable
    {
        get
        {
            return m_resistanceTable;
        }
    }

    public List<SkillDefinition> Skills
    {
        get
        {
            return m_skills;
        }
    }

    public float MovementSpeed
    {
        get
        {
            return m_movementSpeed;
        }
    }

    public WizardData(GameSerializer serializer) : base(serializer)
    {
    }

    public override void DeserializeObject(Dictionary<string, object> dict)
    {
        DictionaryUtils.Parser<int> parserInt = (obj) => { return int.Parse(obj as string); };
        DictionaryUtils.Parser<float> parserFloat = (obj) => { return float.Parse(obj as string); };
        m_wizardExp = new SerializableProperty<int>(m_serializer, dict.TryParseValue<int>("WizardLevel", 1, parserInt));
        m_wizardTemplateId = new SerializableProperty<string>(m_serializer, dict.TryParseValue<string>("WizardTemplateId", string.Empty));
        m_wizardExp = new SerializableProperty<int>(m_serializer, dict.TryParseValue<int>("WizardExp", 0, parserInt));
        m_wizardName = new SerializableProperty<string>(m_serializer, dict.TryParseValue<string>("WizardName", string.Empty));
        m_wizardId = new SerializableProperty<string>(m_serializer, dict.TryParseValue<string>("WizardId", string.Empty));
        m_healthPoints = new SerializableProperty<float>(m_serializer, dict.TryParseValue<float>("WizardHP", 1, parserFloat));
        m_movementSpeed = new SerializableProperty<float>(m_serializer, dict.TryParseValue<float>("WizardMovementSpeed", 1f, parserFloat));
        m_skills = DeserializeSkills(dict["WizardSkills"] as List<string>);
        m_resistanceTable = DeserializeResistanceTable(dict["WizardResistanceTable"] as Dictionary<string,object>);
    }

    public override Dictionary<string, object> SerializeObject()
    {
        Dictionary<string, object> newDict = new Dictionary<string, object>();
        newDict.Add("WizardLevel", m_wizardLevel);
        newDict.Add("WizardTemplateId", m_wizardTemplateId);
        newDict.Add("WizardExp", m_wizardExp);
        newDict.Add("WizardName", m_wizardName);
        newDict.Add("WirzardId", m_wizardId);
        newDict.Add("WizardHP", m_healthPoints);
        newDict.Add("WizardMovementSpeed", m_movementSpeed);
        newDict.Add("WizardSkills", m_skills);
        newDict.Add("WizardResistanceTable", SeralizeResistanceTable(m_resistanceTable));
        return newDict;
    }

    private List<SkillDefinition> DeserializeSkills(List<string> skillIDs)
    {
        List<SkillDefinition> skillsDef = new List<SkillDefinition>();
        for(int i=0, count= skillIDs.Count; i<count; i++)
        {
            SkillDefinition skill = ResourceManager.Instance.SkillRecources.GetSkillsById(skillIDs[i]);
            if(skill != null)
            {
                skillsDef.Add(skill);
            }
        }
        return skillsDef;
    }

    private DamageTable DeserializeResistanceTable(Dictionary<string, object> dict)
    {
        List<DamageTypeValue> damageValues = new List<DamageTypeValue>();
        List<string> damageTypes = dict.TryParseValue("DamageName", new List<string>());
        List<float> damageFloat = dict.TryParseValue("Resistance", new List<float>());

        for (int i = 0, count = damageTypes.Count; i < count; i++)
        {
            damageValues.Add(new DamageTypeValue(ResourceManager.Instance.DamageTypeResources.GetDamageTypeByName(damageTypes[i]), damageFloat[i]));
        }
        return new DamageTable(damageValues); ;
    }


    private Dictionary<string, object> SeralizeResistanceTable(DamageTable m_resistanceTable)
    {
        Dictionary<string, object> serTable = new Dictionary<string, object>();

        List<float> damages = new List<float>();
        List<string> damageTypeNames = new List<string>();

        for(int i=0, count = m_resistanceTable.DamageInfo.Count; i<count; i++)
        {
            damages.Add(m_resistanceTable.DamageInfo[i].DamageValue);
            damageTypeNames.Add(m_resistanceTable.DamageInfo[i].DamageType.DamageTypeName);
        }

        serTable.Add("Resistance", damages);
        serTable.Add("DamageName", damageTypeNames);

        return serTable;
    }
}

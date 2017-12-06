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
    private SerializableProperty<string> m_entityTemplateId;
    private List<SkillDefinition> m_skills;
    private DamageTable m_resistanceTable; 

    public string WizardTemplateId
    {
        get { return m_wizardTemplateId; }
        set
        {
            if (m_wizardTemplateId == value) return;
            m_wizardTemplateId.Property = value;
        }
    }

    public string WizardID
    {
        get
        {
            return m_wizardId;
        }
        set
        {
            if (m_wizardId == value) return;
            m_wizardId.Property = value;
        }
    }

    public int Level
    {
        get
        {
            return m_wizardLevel;
        }
        set
        {
            if (m_wizardLevel == value) return;
            m_wizardLevel.Property = value;
        }
    }

    public int Exp
    {
        get
        {
            return m_wizardExp;
        }
        set
        {
            if (m_wizardExp == value) return;
            m_wizardExp.Property = value;
        }
    }

    public string Name
    {
        get
        {
            return m_wizardName;
        }
        set
        {
            if (m_wizardName == value) return;
            m_wizardName.Property = value;
        }
    }

    public float HealthPoints
    {
        get
        {
            return m_healthPoints;
        }
        set
        {
            if (m_healthPoints == value) return;
            m_healthPoints.Property = value;
        }
    }

    public DamageTable ResistanceTable
    {
        get
        {
            return m_resistanceTable;
        }
        set
        {
            if (m_resistanceTable == value) return;
            m_resistanceTable = value;
        }
    }

    public List<SkillDefinition> Skills
    {
        get
        {
            return m_skills;
        }
        set
        {
            if (m_skills == value) return;
            m_skills = value;
        }
    }

    public float MovementSpeed
    {
        get
        {
            return m_movementSpeed;
        }
        set
        {
            if (m_movementSpeed == value) return;
            m_movementSpeed.Property = value;
        }
    }

    public string EntityTemplateId
    {
        get
        {
            return m_entityTemplateId;
        }

        set
        {
            if (m_entityTemplateId == value) return;
            m_entityTemplateId.Property = value;
        }
    }

    public WizardData(GameSerializer serializer) : base(serializer)
    {
    }

    public WizardData(GameSerializer serializer, WizardDataTemplate template) : base(serializer)
    {
        m_wizardLevel = new SerializableProperty<int>(m_serializer, template.Level);
        m_entityTemplateId = new SerializableProperty<string>(m_serializer, template.EntityTemplateId);
        m_wizardTemplateId = new SerializableProperty<string>(m_serializer, template.TemplateId);
        m_wizardExp = new SerializableProperty<int>(m_serializer, template.Exp);
        m_wizardName = new SerializableProperty<string>(m_serializer, template.Name);
        m_wizardId = new SerializableProperty<string>(m_serializer, template.WizardID);
        m_healthPoints = new SerializableProperty<float>(m_serializer, template.HealthPoints);
        m_movementSpeed = new SerializableProperty<float>(m_serializer, template.MovementSpeed);
        m_skills = new List<SkillDefinition>(template.Skills);
        m_resistanceTable = new DamageTable(template.ResistanceTable.DamageInfo);
    }

    public override void DeserializeObject(Dictionary<string, object> dict)
    {
        DictionaryUtils.Parser<int> parserInt = (obj) => { return int.Parse(obj.ToString()); };
        DictionaryUtils.Parser<float> parserFloat = (obj) => { return float.Parse(obj.ToString()); };
        m_wizardLevel = new SerializableProperty<int>(m_serializer, dict.TryParseValue<int>("WizardLevel", 1, parserInt));
        m_wizardTemplateId = new SerializableProperty<string>(m_serializer, dict.TryParseValue<string>("WizardTemplateId", string.Empty));
        m_entityTemplateId = new SerializableProperty<string>(m_serializer, dict.TryParseValue<string>("WizardEntityTemplateId", string.Empty));
        m_wizardExp = new SerializableProperty<int>(m_serializer, dict.TryParseValue<int>("WizardExp", 0, parserInt));
        m_wizardName = new SerializableProperty<string>(m_serializer, dict.TryParseValue<string>("WizardName", string.Empty));
        m_wizardId = new SerializableProperty<string>(m_serializer, dict.TryParseValue<string>("WizardId", string.Empty));
        m_healthPoints = new SerializableProperty<float>(m_serializer, dict.TryParseValue<float>("WizardHP", 1, parserFloat));
        m_movementSpeed = new SerializableProperty<float>(m_serializer, dict.TryParseValue<float>("WizardMovementSpeed", 1f, parserFloat));
        m_skills = DeserializeSkills(dict["WizardSkills"] as List<object>);
        m_resistanceTable = DeserializeResistanceTable(dict["WizardResistanceTable"] as Dictionary<string,object>);
    }

    public override Dictionary<string, object> SerializeObject()
    {
        Dictionary<string, object> newDict = new Dictionary<string, object>();
        newDict.Add("WizardLevel", m_wizardLevel.Property);
        newDict.Add("WizardTemplateId", m_wizardTemplateId.Property);
        newDict.Add("WizardEntityTemplateId", m_entityTemplateId.Property);
        newDict.Add("WizardExp", m_wizardExp.Property);
        newDict.Add("WizardName", m_wizardName.Property);
        newDict.Add("WizardId", m_wizardId.Property);
        newDict.Add("WizardHP", m_healthPoints.Property);
        newDict.Add("WizardMovementSpeed", m_movementSpeed.Property);
        newDict.Add("WizardSkills", SerializeSkills(m_skills));
        newDict.Add("WizardResistanceTable", SeralizeResistanceTable(m_resistanceTable));
        return newDict;
    }

    private List<SkillDefinition> DeserializeSkills(List<object> skillIDs)
    {
        List<SkillDefinition> skillsDef = new List<SkillDefinition>();
        for(int i=0, count= skillIDs.Count; i<count; i++)
        {
            SkillDefinition skill = ResourceManager.Instance.SkillRecources.GetSkillsById(skillIDs[i].ToString());
            if(skill != null)
            {
                skillsDef.Add(skill);
            }
        }
        return skillsDef;
    }

    private DamageTable DeserializeResistanceTable(Dictionary<string, object> dict)
    {
        DictionaryUtils.Parser<List<string>> parserString = (obj) => 
        {
            List<string> parsedList = new List<string>();
            List<object> ids = obj as List<object>;
            for(int i=0, count=ids.Count; i<count; i++)
            {
                parsedList.Add(ids[i].ToString());
            }
            return parsedList;
        };

        DictionaryUtils.Parser<List<float>> parserFloat = (obj) =>
        {
            List<float> parsedList = new List<float>();
            List<object> ids = obj as List<object>;
            for (int i = 0, count = ids.Count; i < count; i++)
            {
                parsedList.Add(float.Parse(ids[i].ToString()));
            }
            return parsedList;
        };

        List<DamageTypeValue> damageValues = new List<DamageTypeValue>();
        List<string> damageTypes = dict.TryParseValue("DamageName", new List<string>(), parserString);
        List<float> damageFloat = dict.TryParseValue("Resistance", new List<float>(), parserFloat);

        for (int i = 0, count = damageTypes.Count; i < count; i++)
        {
            damageValues.Add(new DamageTypeValue(ResourceManager.Instance.DamageTypeResources.GetDamageTypeByName(damageTypes[i]), damageFloat[i]));
        }
        return new DamageTable(damageValues); ;
    }


    private List<string> SerializeSkills(List<SkillDefinition> skills)
    {
        List<string> skillsId = new List<string>();
        for(int i=0, count=skills.Count; i<count; i++)
        {
            skillsId.Add(skills[i].SkillId);
        }
        return skillsId;
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

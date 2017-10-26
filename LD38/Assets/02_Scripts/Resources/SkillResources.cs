using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillResources
{
    [SerializeField]
    private List<SkillDefinition> m_skills;

    private Dictionary<string, SkillDefinition> m_skillsDict;

    public void Initialize()
    {
        m_skillsDict = new Dictionary<string, SkillDefinition>();
        for(int i=0, count = m_skills.Count; i<count; i++)
        {
            m_skillsDict.Add(m_skills[i].SkillId, m_skills[i]);
        }
    }

    public SkillDefinition GetSkillsById(string skillId)
    {
        SkillDefinition skillDef = null;
        m_skillsDict.TryGetValue(skillId, out skillDef);
        return skillDef;
    }
}

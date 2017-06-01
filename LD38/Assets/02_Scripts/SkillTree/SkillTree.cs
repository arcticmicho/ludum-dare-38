using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree
{
    private SkillLeaf m_root;
    private SkillLeaf m_current;

    public SkillTree(List<SkillDefinition> skills)
    {
        m_root = new SkillLeaf(null, null);
    }

    public void InitializeSkillTree(List<SkillDefinition> skills)
    {
        for(int i=0; i<skills.Count; i++)
        {
            AddSkill(skills[i]);
        }
    }

    private void AddSkill(SkillDefinition skillDefinition)
    {
        SkillLeaf currentLeaf = null;
        SkillLeaf lastLeaf = null;

        for(int i=0; i<skillDefinition.SkillPatterns.Length; i++)
        {
            if(currentLeaf == null)
            {
                if(!m_root.TryGetLeaf(skillDefinition.SkillPatterns[i], out currentLeaf))
                {
                    m_root.AddLeaf(new SkillLeaf(null, skillDefinition.SkillPatterns[i]));
                }
            }else
            {
                lastLeaf = currentLeaf;
                if (!currentLeaf.TryGetLeaf(skillDefinition.SkillPatterns[i], out currentLeaf))
                {
                    if (skillDefinition.SkillPatterns.Length - 1 == i)
                    {
                        lastLeaf.AddLeaf(new SkillLeaf(skillDefinition, skillDefinition.SkillPatterns[i]));
                    }
                    else
                    {
                        lastLeaf.AddLeaf(new SkillLeaf(null, skillDefinition.SkillPatterns[i]));
                    }
                }
                else
                {
                    if (skillDefinition.SkillPatterns.Length - 1 == i)
                    {
                        currentLeaf.SetLeaf(skillDefinition, skillDefinition.SkillPatterns[i]);
                    }
                }
            }
        }
    }
}

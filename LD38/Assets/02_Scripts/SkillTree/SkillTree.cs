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
                    if (skillDefinition.SkillPatterns.Length - 1 == i)
                    {
                        m_root.AddLeaf(new SkillLeaf(skillDefinition, skillDefinition.SkillPatterns[i]));
                    }
                    else
                    {
                        m_root.AddLeaf(new SkillLeaf(null, skillDefinition.SkillPatterns[i]));
                    }
                        
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

    internal bool SearchForPattern(Vector2[] patternPoints, out SkillDefinition skillDef, bool moveToNext = false)
    {
        if(m_current == null)
        {
            m_current = m_root;
        }

        SkillLeaf searchLeaf = m_current;
        SkillDefinition selectedSkillDef = null;
        RecognitionResult lastResult = new RecognitionResult(false, 1);

        for(int i=0; i< searchLeaf.LeafConnections.Count; i++)
        {
            if(!searchLeaf.LeafConnections[i].IsEmptyLeaf)
            {
                PRPatternDefinition pattern;
                if(PartyRecognitionManager.Instance.TryGetPatternById(searchLeaf.LeafConnections[i].Pattern.PatternRecognitionId, out pattern))
                {
                    RecognitionResult result = PartyRecognitionManager.Instance.SimpleRecognize(patternPoints, pattern);
                    if (result.Success && result.RecognitionScore < lastResult.RecognitionScore)
                    {
                        lastResult = result;
                        selectedSkillDef = searchLeaf.LeafConnections[i].SkillDef;
                        if (moveToNext)
                        {
                            m_current = searchLeaf.LeafConnections[i];
                        }                        
                    }
                }
            }            
        }

        if(selectedSkillDef != null)
        {
            skillDef = selectedSkillDef;
            return true;
        }
        skillDef = null;
        return false;
    }

    public void ResetSearch()
    {
        m_current = m_root;
    }

    public List<SkillDefinition> GetAllSkillDefinitionsFromTree()
    {
        List<SkillDefinition> skills = new List<SkillDefinition>();
        SearchForSkills(m_root, skills);
        return skills;
    }

    private void SearchForSkills(SkillLeaf leaf, List<SkillDefinition> skills)
    {
        for(int i=0, count=leaf.LeafConnections.Count; i<count; i++)
        {
            if(!leaf.LeafConnections[i].IsEmptyLeaf)
            {
                skills.Add(leaf.LeafConnections[i].SkillDef);
            }
            SearchForSkills(leaf.LeafConnections[i], skills);
        }
    }
}

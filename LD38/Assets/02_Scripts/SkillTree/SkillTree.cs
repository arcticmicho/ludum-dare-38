using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree
{
    private SkillLeaf m_root;
    private SkillLeaf m_current;

    public SkillTree()
    {
        m_root = new SkillLeaf(null, null);
    }

    public void InitializeSkillTree(List<SkillData> skills)
    {
        for(int i=0; i<skills.Count; i++)
        {
            AddSkill(skills[i]);
        }
    }

    private void AddSkill(SkillData skillData)
    {
        SkillLeaf currentLeaf = null;
        SkillLeaf lastLeaf = null;

        for(int i=0; i<skillData.SkillDefinition.SkillPatterns.Length; i++)
        {
            if(currentLeaf == null)
            {
                if(!m_root.TryGetLeaf(skillData.SkillDefinition.SkillPatterns[i], out currentLeaf))
                {
                    if (skillData.SkillDefinition.SkillPatterns.Length - 1 == i)
                    {
                        m_root.AddLeaf(new SkillLeaf(skillData, skillData.SkillDefinition.SkillPatterns[i]));
                    }
                    else
                    {
                        m_root.AddLeaf(new SkillLeaf(null, skillData.SkillDefinition.SkillPatterns[i]));
                    }
                        
                }
            }else
            {
                lastLeaf = currentLeaf;
                if (!currentLeaf.TryGetLeaf(skillData.SkillDefinition.SkillPatterns[i], out currentLeaf))
                {
                    if (skillData.SkillDefinition.SkillPatterns.Length - 1 == i)
                    {
                        lastLeaf.AddLeaf(new SkillLeaf(skillData, skillData.SkillDefinition.SkillPatterns[i]));
                    }
                    else
                    {
                        lastLeaf.AddLeaf(new SkillLeaf(null, skillData.SkillDefinition.SkillPatterns[i]));
                    }
                }
                else
                {
                    if (skillData.SkillDefinition.SkillPatterns.Length - 1 == i)
                    {
                        currentLeaf.SetLeaf(skillData, skillData.SkillDefinition.SkillPatterns[i]);
                    }
                }
            }
        }
    }

    internal bool SearchForPattern(Vector2[] patternPoints, out SkillData skillData, bool moveToNext = false)
    {
        if(m_current == null)
        {
            m_current = m_root;
        }

        SkillLeaf searchLeaf = m_current;
        SkillData selectedSkill = null;
        float lastScore = -1f;
        PropagateResult propagateResult = PartyRecognitionManager.Instance.Recognize(patternPoints);

        for(int i=0; i< searchLeaf.LeafConnections.Count; i++)
        {
            if(!searchLeaf.LeafConnections[i].IsEmptyLeaf)
            {
                float score = propagateResult.GetScore(searchLeaf.LeafConnections[i].Pattern.PatternRecognitionId);
                if (score >= searchLeaf.LeafConnections[i].Pattern.PatternThreshold && score >= lastScore)
                {
                    lastScore = score;
                    selectedSkill = searchLeaf.LeafConnections[i].SkillData;
                    if (moveToNext)
                    {
                        m_current = searchLeaf.LeafConnections[i];
                    }
                }
            }            
        }

        if(selectedSkill != null)
        {
            skillData = selectedSkill;
            return true;
        }
        skillData = null;
        return false;
    }

    public bool CheckFinishPattern(Vector2[] patternPoints)
    {
        //RecognitionResult result = PartyRecognitionManager.Instance.SimpleRecognize(patternPoints, pattern);
        return false;
    }

    public void ResetSearch()
    {
        m_current = m_root;
    }

    public List<SkillData> GetAllSkillFromTree()
    {
        List<SkillData> skills = new List<SkillData>();
        SearchForSkills(m_root, skills);
        return skills;
    }

    private void SearchForSkills(SkillLeaf leaf, List<SkillData> skills)
    {
        for(int i=0, count=leaf.LeafConnections.Count; i<count; i++)
        {
            if(!leaf.LeafConnections[i].IsEmptyLeaf)
            {
                skills.Add(leaf.LeafConnections[i].SkillData);
            }
            SearchForSkills(leaf.LeafConnections[i], skills);
        }
    }
}

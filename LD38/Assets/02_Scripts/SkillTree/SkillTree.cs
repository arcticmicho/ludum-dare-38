﻿using System;
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

        RecognitionResult lastResult = new RecognitionResult(false, 1);

        for(int i=0; i<m_current.LeafConnections.Count; i++)
        {
            if(!m_current.LeafConnections[i].IsEmptyLeaf)
            {
                PRPatternDefinition pattern;
                if(PartyRecognitionManager.Instance.TryGetPatternById(m_current.LeafConnections[i].Pattern.PatternRecognitionId, out pattern))
                {
                    RecognitionResult result = PartyRecognitionManager.Instance.SimpleRecognize(patternPoints, pattern);
                    if (result.Success && result.RecognitionScore < lastResult.RecognitionScore)
                    {
                        lastResult = result;
                        skillDef = m_current.LeafConnections[i].SkillDef;
                        if (moveToNext)
                        {
                            m_current = m_current.LeafConnections[i];
                        }                        
                        return true;
                    }
                }
            }            
        }
        skillDef = null;
        return false;
    }

    public void ResetSearch()
    {
        m_current = m_root;
    }
}
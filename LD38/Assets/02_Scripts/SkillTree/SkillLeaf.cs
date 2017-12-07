using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLeaf
{
    private SkillData m_skill;
    public SkillData SkillData
    {
        get { return m_skill; }
    }

    private SkillPattern m_associatedPattern;
    public SkillPattern Pattern
    {
        get { return m_associatedPattern; }
    }

    private List<SkillLeaf> m_leafConnections;
    public List<SkillLeaf> LeafConnections
    {
        get { return m_leafConnections; }
    }

    public bool IsEmptyLeaf
    {
        get { return m_skill == null; }
    }

    public SkillLeaf(SkillData skillData, SkillPattern pattern)
    {
        m_skill = skillData;
        m_associatedPattern = pattern;
        m_leafConnections = new List<SkillLeaf>();
    }

    public bool TryGetLeaf(SkillData skillDef, out SkillLeaf childLeaf)
    {
        for(int i=0; i<m_leafConnections.Count; i++)
        {
            if(m_leafConnections[i].m_skill == skillDef)
            {
                childLeaf = m_leafConnections[i];
                return true;
            }
        }
        childLeaf = null;
        return false;
    }

    public bool TryGetLeaf(SkillPattern pattern, out SkillLeaf leaf)
    {
        for(int i=0; i<m_leafConnections.Count; i++)
        {
            if(m_leafConnections[i].Pattern == pattern)
            {
                leaf = m_leafConnections[i];
                return true;
            }
        }
        leaf = null;
        return false;
    }

    public bool SkillLeafContains(SkillLeaf leaf)
    {
        return m_leafConnections.Contains(leaf);
    }

    public bool AddLeaf(SkillLeaf leaf)
    {
        for(int i=0; i<m_leafConnections.Count; i++)
        {
            if(m_leafConnections[i] == leaf)
            {
                Debug.LogWarning("Circular reference?");
                return false;
            }
        }
        m_leafConnections.Add(leaf);
        return true;
    }

    public void SetLeaf(SkillData skillData, SkillPattern pattern)
    {
        m_skill = skillData;
        m_associatedPattern = pattern;
    }
}

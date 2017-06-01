using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLeaf
{
    private SkillDefinition m_skill;
    public SkillDefinition SkillDef
    {
        get { return m_skill; }
    }

    private SkillPattern m_associatedPattern;
    public SkillPattern Pattern
    {
        get { return m_associatedPattern; }
    }

    private List<SkillLeaf> m_leafConnections;

    public SkillLeaf(SkillDefinition skillDef, SkillPattern pattern)
    {
        m_skill = skillDef;
        m_associatedPattern = pattern;
        m_leafConnections = new List<SkillLeaf>();
    }

    public bool TryGetLeaf(SkillDefinition skillDef, out SkillLeaf childLeaf)
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

    public void SetLeaf(SkillDefinition skillDef, SkillPattern pattern)
    {
        m_skill = skillDef;
        m_associatedPattern = pattern;
    }
}

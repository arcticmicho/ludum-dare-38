using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsBook : MonoBehaviour
{
    [SerializeField]
    private Transform m_skillsViewGrid;
    [SerializeField]
    private SkillView m_skillViewPrefab;

    private List<SkillView> m_skillsView = new List<SkillView>();

    public void InitializeSkillsBook(SkillTree skillTree)
    {
        m_skillsView.Clear();
        
        for(int i=0, count=m_skillsViewGrid.childCount; i<count; i++)
        {
            Destroy(m_skillsViewGrid.GetChild(i).gameObject);
        }

        foreach (SkillData skillDef in skillTree.GetAllSkillFromTree())
        {
            SkillView newView = GameObject.Instantiate(m_skillViewPrefab);
            newView.InitializeSkillView(skillDef.SkillDefinition);
            newView.transform.SetParent(m_skillsViewGrid, false);
        }
    }
}

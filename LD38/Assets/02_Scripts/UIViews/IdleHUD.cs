using GameModules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleHUD : UIPanel
{
    [SerializeField]
    private SkillsBook m_skillsBook;

    public void CreateSkillsBook(SkillTree skillTree)
    {
        m_skillsBook.InitializeSkillsBook(skillTree);
    }    
}

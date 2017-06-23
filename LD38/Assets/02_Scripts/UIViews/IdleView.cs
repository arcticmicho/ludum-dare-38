using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleView : UIView
{
    [SerializeField]
    private SkillsBook m_skillsBook;

    public void CreateSkillsBook(SkillTree skillTree)
    {
        m_skillsBook.InitializeSkillsBook(skillTree);
    }

    protected override void OnViewOpened()
    {
        base.OnViewOpened();
        Debug.Log("Idle View Opened");
    }
}

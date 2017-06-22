using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastingView : UIView
{
    [SerializeField]
    private Text m_skillName;

    public void SetSkillName(string skillName)
    {
        m_skillName.text = skillName;
    }
	
}

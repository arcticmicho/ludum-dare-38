﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Character
{
    private SkillTree m_skillTree;
    public SkillTree SkillTree
    {
        get { return m_skillTree; }
    }    

    public IWizardData WizardData
    {
        get { return m_data as IWizardData; }
    }

    public Wizard(GameSession session, IWizardData template, CharacterEntity entity) : base(session, template, entity)
    {
        m_skillTree = new SkillTree(template.Skills);
        m_skillTree.InitializeSkillTree(template.Skills);
    }  

    public bool TryProcessPattern(Vector2[] patternPoints, out SkillDefinition skillDef)
    {
        if(m_skillTree != null && m_skillTree.SearchForPattern(patternPoints, out skillDef, true))
        {
            return true;           
        }
        skillDef = null;
        return false;
    }

    public void ResetSkillTree()
    {
        m_skillTree.ResetSearch();
    }

    public override void KillCharacter()
    {
        base.KillCharacter();
        m_contextSession.NotifyMainCharacterDeath(this);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Character
{

    private GenericStateMachine<Wizard, TransitionData> m_stateMachine;

    private SkillTree m_skillTree;

    #region Get/Set
    public SkillTree SkillTree
    {
        get { return m_skillTree; }
    }    

    public IWizardData WizardData
    {
        get { return m_data as IWizardData; }
    }

    public override Character CurrentTarget
    {
        get { return m_contextSession.CurrentTarget; }
    }
    #endregion

    public Wizard(GameSession session, IWizardData template, CharacterEntity entity) : base(session, template, entity)
    {
        m_skillTree = new SkillTree();
        m_skillTree.InitializeSkillTree(template.Skills);

        m_stateMachine = new GenericStateMachine<Wizard, TransitionData>(this);
        m_stateMachine.StartGameplayStateMachine<IdleGameplayState>();
    }  

    public bool TryProcessPattern(Vector2[] patternPoints, out SkillData skillData)
    {
        if(m_skillTree != null && m_skillTree.SearchForPattern(patternPoints, out skillData, true))
        {
            return true;           
        }
        skillData = null;
        return false;
    }

    public void Update()
    {
        m_stateMachine.UpdateSM();
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

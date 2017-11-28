using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterTemplateResources
{
    [SerializeField]
    private List<CharacterTemplate> m_enemyTemplates;

    [SerializeField]
    private List<WizardDataTemplate> m_initialWizards;
    public List<WizardDataTemplate> InitialWizard
    {
        get { return m_initialWizards; }
    }

    private Dictionary<string, CharacterTemplate> m_enemyTemplatesDict;

    public void Initialize()
    {
        m_enemyTemplatesDict = new Dictionary<string, CharacterTemplate>();
        for (int i=0, count=m_enemyTemplates.Count; i<count; i++)
        {
            m_enemyTemplatesDict.Add(m_enemyTemplates[i].TemplateId, m_enemyTemplates[i]);
        }
    }

    public CharacterTemplate GetEnemyTemplate(string id)
    {
        CharacterTemplate template = null;
        m_enemyTemplatesDict.TryGetValue(id, out template);
        return template;
    }
}

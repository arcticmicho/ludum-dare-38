using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillView : MonoBehaviour
{
    [SerializeField]
    private Text m_skillName;

    [SerializeField]
    private Transform m_patternGrid;
    
    public void InitializeSkillView(SkillDefinition def)
    {
        m_skillName.text = def.SkillName;

        for(int i=0, count = def.SkillPatterns.Length; i<count; i++)
        {
            GenerateSkillPatternImage(def.SkillPatterns[i].SkillPatternImage);
        }
    }	

    private void GenerateSkillPatternImage(Sprite sprite)
    {
        GameObject go = new GameObject();
        Image image = go.AddComponent<Image>();
        image.sprite = sprite;
        go.transform.SetParent(m_patternGrid);
    }
}

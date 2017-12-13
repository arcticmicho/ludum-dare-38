using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemTemplate : ScriptableObject
{
    [SerializeField]
    protected string m_itemTemplateId;
    
    public string ItemTemplateId
    {
        get { return m_itemTemplateId; }
    }	

    [SerializeField]
    protected string m_itemTemplateName
    {
        get { return m_itemTemplateName; }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Container", menuName ="UIParty /Container")]
public class ViewContainer : ScriptableObject
{
    [SerializeField]
    private List<UIView> m_viewReferences;
    public List<UIView> Views
    {
        get { return m_viewReferences; }
    }

    [SerializeField]
    private List<ViewContainer> m_viewContainers;
    public List<ViewContainer> Containers
    {
        get { return m_viewContainers; }
    }
	
}

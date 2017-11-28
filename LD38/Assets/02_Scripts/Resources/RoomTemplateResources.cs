using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomTemplateResources
{
    [SerializeField]
    private List<RoomSessionData> m_roomSessionTemplates;
    public List<RoomSessionData> RoomSessionTemplates
    {
        get { return m_roomSessionTemplates; }
    }

    private Dictionary<string, RoomSessionData> m_roomSessionTemplatesDict;
    
    public void Initialize()
    {
        m_roomSessionTemplatesDict = new Dictionary<string, RoomSessionData>();

        for(int i=0, count=m_roomSessionTemplates.Count; i<count; i++)
        {
            m_roomSessionTemplatesDict.Add(m_roomSessionTemplates[i].RoomSessionId, m_roomSessionTemplates[i]);
        }
    }	

    public RoomSessionData GetRoomSessionTemplateById(string id)
    {
        RoomSessionData template = null;
        m_roomSessionTemplatesDict.TryGetValue(id, out template);
        return template;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomTemplateWidget : MonoBehaviour
{

    [SerializeField]
    private Text m_roomTemplateName;

    [SerializeField]
    private Image m_roomImage;

    [SerializeField]
    private Button m_selectTemplate;

    public void SetupWidget(RoomSessionData room)
    {
        m_roomTemplateName.text = room.RoomName;
        m_selectTemplate.onClick.RemoveAllListeners();
        m_selectTemplate.onClick.AddListener(() => GameManager.Instance.SetRoomId(room));
    }
}

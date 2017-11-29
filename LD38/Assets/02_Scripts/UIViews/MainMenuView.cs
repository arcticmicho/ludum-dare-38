using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : UIView
{
    [SerializeField]
    private Button m_playButton;

    [SerializeField]
    private Transform m_wizardListParent;

    [SerializeField]
    private Transform m_roomTemplatesListParent;

    [SerializeField]
    private WizardWidget m_wizardWidgetPrefab;

    [SerializeField]
    private RoomTemplateWidget m_roomTemplateWidget;

    private List<WizardWidget> m_activeWizardWidgets = new List<WizardWidget>();

    private List<RoomTemplateWidget> m_activeRoomWidgets = new List<RoomTemplateWidget>();

    protected override void OnInitialize()
    {
        base.OnInitialize();
        m_playButton.onClick.RemoveAllListeners();
        m_playButton.onClick.AddListener(() => { PlayButtonPressed(); });
    }

    protected override void OnViewOpen()
    {
        base.OnViewOpen();
        SetupWizards();
        SetupRooms();
    }

    private void SetupRooms()
    {
        m_activeRoomWidgets.Clear();
        for (int i=0,count=ResourceManager.Instance.RoomTemplateResources.RoomSessionTemplates.Count; i<count; i++)
        {
            RoomTemplateWidget newWidget = Instantiate<RoomTemplateWidget>(m_roomTemplateWidget);
            newWidget.SetupWidget(ResourceManager.Instance.RoomTemplateResources.RoomSessionTemplates[i]);
            newWidget.transform.SetParent(m_roomTemplatesListParent);
            m_activeRoomWidgets.Add(newWidget);
        }
    }

    private void SetupWizards()
    {
        m_activeWizardWidgets.Clear();
        for (int i = 0, count = CharactersManager.Instance.UnlockedWizards.Count; i < count; i++)
        {
            WizardWidget newWidget = Instantiate<WizardWidget>(m_wizardWidgetPrefab);
            newWidget.SetupWidget(CharactersManager.Instance.UnlockedWizards[i]);
            newWidget.transform.SetParent(m_wizardListParent);
            m_activeWizardWidgets.Add(newWidget);
        }
    }

    protected override void OnViewClosed()
    {
        base.OnViewClosed();
        for(int i=0, count=m_activeRoomWidgets.Count; i<count; i++)
        {
            Destroy(m_activeRoomWidgets[i].gameObject);
        }
        for(int i=0, count=m_activeWizardWidgets.Count; i<count; i++)
        {
            Destroy(m_activeWizardWidgets[i].gameObject);
        }
    }

    public void PlayButtonPressed()
    {
        GameStateManager.Instance.StartGameSession();
    }	
}

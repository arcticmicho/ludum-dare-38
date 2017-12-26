using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GameModules
{

    public sealed class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField]
        private Camera _uiCamera;

        [SerializeField]
        private EventSystem _eventSystem;

        [SerializeField]
        private RectTransform _canvasTransform;

        [SerializeField]
        private RectTransform _worldCanvasTransform;

        [Header("Layer Objects")]
        [SerializeField]
        private RectTransform _popupLayerParent;

        [SerializeField]
        private RectTransform _mainLayerParent;

        [SerializeField]
        private RectTransform _actionLayerParent;

        [SerializeField]
        private RectTransform _worldLayerParent;

        [Header("Utility Objects")]
        [SerializeField]
        private UIBlockPanel _blockingPanel;

        [SerializeField]
        private UIPanelPool _panelPool;


        [Header("Properties")]
        [SerializeField]
        private UIManagerConfiguration _configuration;

        private Dictionary<Type, UIPanel> _panelDatabase = new Dictionary<Type, UIPanel>();

        private Dictionary<Type, UIPanel> _layerHUDDict = new Dictionary<Type, UIPanel>();

        private Dictionary<Type, UIPanelItem> _panelItems = new Dictionary<Type, UIPanelItem>();

        private List<UIPanel> _layerHUDList          = new List<UIPanel>();
        private List<UIPanel> _layerActionHUDList    = new List<UIPanel>();
        private List<UIPanel> _layerPopupList        = new List<UIPanel>();

        private UIPanel _popupOnTop = null;

        private List<UIWorldItem> _worldItemList     = new List<UIWorldItem>();

        public Camera UICamera
        {
            get { return _uiCamera; }
        }

        protected override void OnAwake()
        {
            Debug.LogWarning("[GUIManager] Initializing GUI Manager", this);

            Reset();

            LoadBasePopupList();
        }

        protected override bool DestroyOnLoad()
        {
            return false;
        }

        public void Reset()
        {
            _layerHUDList.Clear();
            _layerActionHUDList.Clear();
            _layerPopupList.Clear();
            _popupOnTop = null;
        }

        #region DATABASE HANDLING

        private void LoadBasePopupList()
        {
            for (int a = 0; a < _configuration.PanelLists.Length; ++a)
            {
                UIPanel[] panelList = _configuration.PanelLists[a].Panels;

                for (int b = 0; b < panelList.Length; ++b)
                {
                    _panelDatabase.Add(panelList[b].GetType(), panelList[b]);
                    _panelPool.PreloadInstance(panelList[b]);
                }
            }

            if (_configuration.PanelItemList != null)
            {
                UIPanelItem[] panelItems = _configuration.PanelItemList.PanelItems;

                for (int a = 0; a < panelItems.Length; ++a)
                {
                    _panelItems.Add(panelItems[a].GetType(), panelItems[a]);
                }
            }
        }

        #endregion

        #region HUD ELEMENTS

        public T AddHUDElement<T>() where T : UIPanel
        {
            Type panelType = typeof(T);
            if (!_layerHUDDict.ContainsKey(panelType))
            {
                UIPanel panelModel = null;

                if (_panelDatabase.TryGetValue(panelType, out panelModel))
                {
                    UIPanel newPanel = _panelPool.GetInstance(panelModel);
                    newPanel.Initialize(UIPanelPriority.Medium, UIPanelFlags.None);
                    newPanel.gameObject.SetActive(true);
                    newPanel.transform.SetParent(_mainLayerParent, false);
                    newPanel.Open();

                    _layerHUDDict.Add(panelType, newPanel);
                    _layerHUDList.Add(newPanel);

                    return newPanel as T;
                }
                else
                {
                    Debug.LogError("The HUD element " + panelType + " does not exist in the database");
                }
            }
            else
            {
                Debug.LogError("The HUD element already exist in the HUD");
            }

            return null;
        }

        public T GetHUDElement<T>() where T : UIPanel
        {
            UIPanel hudPanel = null;
            if (_layerHUDDict.TryGetValue(typeof(T), out hudPanel))
            {
                return hudPanel as T;

            }
            return default(T);
        }

        public void RemoveHUDElement<T>() where T : UIPanel
        {
            UIPanel hudPanel = null;

            if (_layerHUDDict.TryGetValue(typeof(T), out hudPanel))
            {
                if (_layerHUDDict.Remove(typeof(T)))
                {
                    _layerHUDList.Remove(hudPanel);
                    hudPanel.Close();
                }
            }
        }

        public void ClearHUDElements()
        {
            for (int a = 0; a < _layerHUDList.Count; ++a)
            {
                _layerHUDList[a].Close();
            }

            _layerHUDList.Clear();
            _layerHUDDict.Clear();
        }

        public void ShowHUD()
        {
            for (int a = 0; a < _layerHUDList.Count; ++a)
            {
                _layerHUDList[a].Show();
            }
        }

        public void HideHUD()
        {
            for (int a = 0; a < _layerHUDList.Count; ++a)
            {
                _layerHUDList[a].Hide();
            }
        }

        private void SystemShowHUD(bool show)
        {
            for (int a = 0; a < _layerHUDList.Count; ++a)
            {
                _layerHUDList[a].SystemShow(show);
            }
        }

        #endregion

        #region ACTION HUD ELEMENTS

        public T RequestActionHUDElement<T>() where T : UIPanel
        {
            return RequestActionHUDElementInternal<T>();
        }

        private T RequestActionHUDElementInternal<T>(UIPanelPriority priority = UIPanelPriority.Medium, UIPanelFlags flags = UIPanelFlags.None) where T : UIPanel
        {
            UIPanel panelModel = null;

            if (_panelDatabase.TryGetValue(typeof(T), out panelModel))
            {
                UIPanel newPanel = _panelPool.GetInstance(panelModel);
                newPanel.Initialize(priority, flags);
                newPanel.gameObject.SetActive(true);
                newPanel.transform.SetParent(_actionLayerParent, false);
                newPanel.Open();
                _layerActionHUDList.Add(newPanel);
                return (T)newPanel;
            }
            else
            {
                Debug.LogError("The Action HUD element " + typeof(T) + " does not exist in the database");
            }
            return null;
        }

        public void CloseActionHUDElement(string panelName)
        {
            for (int a = 0; a < _layerActionHUDList.Count; ++a)
            {
                if (_layerActionHUDList[a].name == panelName)
                {
                    CloseActionHUDElementInternal(_layerActionHUDList[a]);
                    return;
                }
            }
        }

        public void CloseActionHUDElement(UIPanel panel)
        {
            CloseActionHUDElementInternal(panel);
        }

        private void CloseActionHUDElementInternal(UIPanel panel)
        {
            if (_layerActionHUDList.Remove(panel))
            {
                panel.Close();
            }
        }

        #endregion

        #region POPUP ELEMENTS

        public T RequestPopup<T>(UIPanelPriority priority = UIPanelPriority.Medium, UIPanelFlags flags = UIPanelFlags.None) where T : UIPanel
        {
            return RequestPopupInternal<T>(priority, flags);
        }

        private T RequestPopupInternal<T>(UIPanelPriority priority, UIPanelFlags flags) where T : UIPanel
        {
            UIPanel panelModel = null;

            if (_panelDatabase.TryGetValue(typeof(T), out panelModel))
            {
                UIPanel newPanel = _panelPool.GetInstance(panelModel);

                if (newPanel != null)
                {
                    newPanel.Initialize(priority, flags);
                    InsertPopupPanel(newPanel);
                    newPanel.gameObject.SetActive(true);
                    newPanel.Open();

                    UpdatePanelPropeties();

                    return newPanel as T;
                }
                else
                {
                    Debug.LogWarning("Panel: " + typeof(T) + "exists but is null!");
                }
            }
            else
            {
                Debug.LogError("Panel: " + typeof(T) + " does not exist!");
            }

            return null;
        }

        private void InsertPopupPanel(UIPanel panel)
        {
            for (int a = 0; a < _layerPopupList.Count; a++)
            {
                if ((int)_layerPopupList[a].Priority <= (int)panel.Priority)
                {
                    continue;
                }
                _layerPopupList.Insert(a, panel);
                panel.transform.SetParent(_popupLayerParent, false);
                panel.transform.SetSiblingIndex(a);
                return;
            }

            _layerPopupList.Add(panel);
            panel.transform.SetParent(_popupLayerParent, false);
            panel.transform.SetSiblingIndex(_layerPopupList.Count);
        }

        private void ClosePopupInternal(UIPanel closingPopup)
        {
            if (_layerPopupList.Remove(closingPopup))
            {
                UpdatePanelPropeties();
                closingPopup.Close();
            }
            else
            {
                Debug.LogError("[GUIManager] The popup " + closingPopup.name + " is not in the popup queue.", this);
            }
        }

        public void ClosePopup<T>()
        {
            UIPanel selected = null;
            for (int a = _layerPopupList.Count - 1; a >= 0; a--)
            {
                if (_layerPopupList[a].GetType() == typeof(T))
                {
                    selected = _layerPopupList[a];
                    break;
                }
            }

            if (selected != null)
            {
                ClosePopupInternal(selected);
            }
            else
            {
                Debug.LogError("[GUIManager] The popup " + name + " does not exist", this);
            }
        }

        public void ClosePopupsWithTag(string tag)
        {
            List<UIPanel> closePanels = new List<UIPanel>();

            for (int a = 0; a < _layerPopupList.Count; a++)
            {
                if (_layerPopupList[a].Tag == tag)
                {
                    closePanels.Add(_layerPopupList[a]);
                }
            }

            for (int a = 0; a < closePanels.Count; a++)
            {
                ClosePopupInternal(closePanels[a]);
            }
        }

        public void ClosePopupsOfType<T>()
        {
            List<UIPanel> closePanels = new List<UIPanel>();

            for (int a = 0; a < _layerPopupList.Count; a++)
            {
                if (_layerPopupList[a].GetType() == typeof(T))
                {
                    closePanels.Add(_layerPopupList[a]);
                }
            }

            for (int a = 0; a < closePanels.Count; a++)
            {
                ClosePopupInternal(closePanels[a]);
            }
        }

        public void CloseAllPopups()
        {
            List<UIPanel> closePanels = new List<UIPanel>(_layerPopupList);

            for (int a = 0; a < closePanels.Count; a++)
            {
                ClosePopupInternal(closePanels[a]);
            }
        }

        public void ClosePopup(UIPanel popup)
        {
            ClosePopupInternal(popup);
        }

        public void CloseCurrentPopup()
        {
            if (_popupOnTop != null)
            {
                ClosePopupInternal(_popupOnTop);
            }
        }

        public void PopupOverlayPressed()
        {
            if (_popupOnTop != null)
            {
                _popupOnTop.OverlayPressed();
            }
        }

        private void UpdatePanelPropeties()
        {
            if (_layerPopupList.Count > 0)
            {
                UIPanel lastTopPopup = _popupOnTop;
                _popupOnTop = _layerPopupList[_layerPopupList.Count - 1];

                if (lastTopPopup != _popupOnTop)
                {
                    UpdateBlockingPanel();

                    SystemShowHUD(((_popupOnTop.Flags & UIPanelFlags.HideHUD) != UIPanelFlags.HideHUD));

                    if ((_popupOnTop.Flags & UIPanelFlags.HidePopups) == UIPanelFlags.HidePopups)
                    {
                        ShowPopupsFromTop();
                    }
                    else
                    {
                        ShowAllPopups();
                    }
                }
            }
            else
            {
                _popupOnTop = null;
                _blockingPanel.transform.SetParent(_canvasTransform, false);
                _blockingPanel.Setup(false, false);
                SystemShowHUD(true);
            }

        }

        private void UpdateBlockingPanel()
        {
            bool disableOverlay = ((_popupOnTop.Flags & UIPanelFlags.DisableDarkBackground) == UIPanelFlags.DisableDarkBackground);
            bool allowUIInput = ((_popupOnTop.Flags & UIPanelFlags.AllowUIInput) == UIPanelFlags.AllowUIInput);

            _blockingPanel.transform.SetParent(_popupOnTop.transform, false);
            _blockingPanel.transform.SetSiblingIndex(0);
            _blockingPanel.Setup(!allowUIInput, !disableOverlay);
        }

        private void ShowPopupsFromTop(int showCount=1)
        {
            for (int a = 0; a < _layerPopupList.Count; ++a)
            {
                _layerPopupList[a].SystemShow(a >= (_layerPopupList.Count - showCount));
            }
        }

        private void ShowAllPopups()
        {
            for (int a = 0; a < _layerPopupList.Count; ++a)
            {
                _layerPopupList[a].SystemShow(true);
            }
        }

        #endregion

        /// <summary>
        /// Function to create a panel item for a UI panel.
        /// </summary>
        public T CreatePanelItem<T>() where T : UIPanelItem
        {
            UIPanelItem panelItem = null;

            if (_panelItems.TryGetValue(typeof(T), out panelItem))
            {
                UIPanelItem item = Instantiate(panelItem);
                return item.GetComponent<T>(); ;
            }

            return default(T);
        }

        #region WorldItems
        public void AddWorldItem(UIWorldItem worldItem)
        {
            _worldItemList.Add(worldItem);
        }

        public bool RemoveWorldItem(UIWorldItem worldItem)
        {
            return _worldItemList.Remove(worldItem);
        }

        #endregion

        private void Update()
        {
            _updateDebug();

            if (_configuration.UpdateHudPanels)
            {
                for (int a = 0; a < _layerHUDList.Count; ++a)
                {
                    _layerHUDList[a].CustomUpdate();
                }

                for (int a = 0; a < _layerActionHUDList.Count; ++a)
                {
                    _layerActionHUDList[a].CustomUpdate();
                }
            }

            if (_popupOnTop != null)
            {
                _popupOnTop.CustomUpdate();

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    _popupOnTop.BackPressed();
                }
            }
        }

        /// <summary>
        ///  Disable all the events related to the Unity GUI System.
        /// </summary>
        public void EnableCanvasInput(bool enabled)
        {
            if (_eventSystem == null)
            {
                Debug.LogWarning("Event system is null!. This should not happen, please set the reference.");
                return;
            }

            if (_eventSystem.enabled && enabled)
            {
                Debug.LogWarning("Trying to enable an already enabled Event System");
            }

            if (!_eventSystem.enabled && !enabled)
            {
                Debug.LogWarning("Trying to disable an already enabled Event System");
            }

            _eventSystem.enabled = enabled;
        }

        #region Utility Functions

        public Vector3 UIToViewportPoint(Vector3 uiPosition)
        {
            return _uiCamera.WorldToViewportPoint(uiPosition);
        }

        #endregion

        #region Debug Tools

        [System.Diagnostics.Conditional("DEBUG")]
        private void _updateDebug()
        {
            //     GUIManager.LOG("Debug Update");

            // Check for correct stack positions;

        }
        #endregion

    }
}

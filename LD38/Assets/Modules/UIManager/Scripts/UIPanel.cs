using UnityEngine;
using System.Collections;

namespace GameModules
{
    [System.Flags]
    public enum UIPanelFlags
    {
        None = 0x0,         
        HidePopups = 0x1,            // Hide other popups when this panel appear. This only works form popups.
        HideHUD = 0x2,               // Hide the hud when this popup appears. This only works for popups.
        DisableDarkBackground = 0x4, // Hide the dark background but does not allow ui to pass trough 
        AllowUIInput = 0x8,          // Allow the input to pass trough the popup.

        HideAllUI = HidePopups | HideHUD,
        DisableBackground = DisableDarkBackground| AllowUIInput
    }

    public enum UIPanelPriority
    {
        Lowest = 0,
        Low = 1,
        BelowMedium = 2,
        Medium = 3,
        AboveMedium = 4,
        High = 5,
        Hightest = 6,
    }

    public enum UIPanelState
    {
        Hidden  = 0,
        Hiding  = 1,
        Showed  = 2,
        Showing = 3
    }

    public class UIPanel : MonoBehaviour
    {
        [SerializeField]
        private UIPanelAnimation _animation;

        [SerializeField]
        private RectTransform _mainPanel;

        private UIPanelPool _pool;

        private string _panelTag;

        private UIPanelFlags _flags;
        private UIPanelPriority _priority;
        private UIPanelState _state = UIPanelState.Hidden;

        private bool _open = false;
        private bool _disposed = false;

        private bool _updatingShowStatus = false;

        private bool _shownByUser   = true;
        private bool _shownBySystem = true;

        public event System.Action<UIPanel> OnOpenEvent;
        public event System.Action<UIPanel> OnCloseEvent;

        public event System.Action<UIPanel> OnShowEvent;
        public event System.Action<UIPanel> OnHideEvent;

        #region Get/Set

        public bool IsOpen
        {
            get { return _open; }
        }

        public UIPanelPriority Priority
        {
            get { return _priority; }
        }

        public UIPanelFlags Flags
        {
            get { return _flags; }
        }

        public UIPanelState State
        {
            get { return _state; }
        }

        public RectTransform MainPanel
        {
            get { return _mainPanel; }
        }

        public string Tag
        {
            get { return _panelTag; }
            set { _panelTag = value; }
        }

        #endregion

        /// <summary>
        /// Function called when a panel is created from a pool. This is just called 1 time even if the panel is reused.
        /// </summary>
        internal void PoolCreate(UIPanelPool pool)
        {
            _pool = pool;

            if (_mainPanel == null)
            {
                Debug.LogError("_mainPanel is null ABORT! You need to define a main panel");
            }
        }

        /// <summary>
        /// Function called befor opening the panel.
        /// </summary>
        internal void Initialize(UIPanelPriority priority, UIPanelFlags flags)
        {
            _flags = flags;
            _priority = priority;

            _disposed = false;
            _shownByUser = false;
            _shownBySystem = true;

            _mainPanel.gameObject.SetActive(false);
        }

        /// <summary>
        /// Function called when a panel is created. This begin the life of a panel.
        /// </summary>
        internal void Open()
        {
            if (!_open)
            {
                _open = true;
                OnOpen();

                if (OnOpenEvent != null)
                {
                    OnOpenEvent(this);
                }
            }
            else
            {
                Debug.LogError("Trying to open an already Opened UIPanel");
            }
        }

        /// <summary>
        /// Function to return this panel to the pool. This ends the life of a panel.
        /// </summary>
        internal void Close()
        {
            if (_state != UIPanelState.Hidden)
            {
                _disposed = true;
                UpdateShowStatus();
            }
            else
            {
                if (_open)
                {
                    _open = false;
                    OnClose();

                    if (OnCloseEvent != null)
                    {
                        OnCloseEvent(this);
                    }

                    _pool.ReleaseInstance(this);
                }
                else
                {
                    Debug.LogError("Trying to close an already Closed UIPanel");
                }
            }
        }

        /// <summary>
        /// Function called only by the UIManager To tell this panel to stai shown or hidden. The system has authority
        /// </summary>
        internal void SystemShow(bool show)
        {
            _shownBySystem = show;
            UpdateShowStatus();
        }

        public void Show()
        {
            _shownByUser = true;
            UpdateShowStatus();
        }

        internal void InternalShow()
        {
            if (_state == UIPanelState.Hidden)
            {
                _mainPanel.gameObject.SetActive(true);
                OnShowStart();
                _state = UIPanelState.Showing;

                if (_animation == null)
                {
                    InternalShowEnd();
                }
                else
                {
                    _animation.AnimateShow(InternalShowEnd);
                }
            }
            else
            {
                Debug.LogError("Trying to call InternalShow in a bad state");
            }
        }

        private void InternalShowEnd()
        {
            _state = UIPanelState.Showed;
            OnShowEnd();
            if (OnShowEvent != null)
            {
                OnShowEvent(this);
            }
        }

        public void Hide()
        {
            _shownByUser = false;
            UpdateShowStatus();
        }

        private void InternalHide()
        {
            if (_state == UIPanelState.Showed)
            {
                OnHideStart();
                _state = UIPanelState.Hiding;

                if (_animation == null)
                {
                    InternalHideEnd();
                }
                else
                {
                    _animation.AnimateHide(InternalHideEnd);
                }
            }
            else
            {
                Debug.LogError("Trying to call InternalHide in a bad state");
            }
        }

        private void InternalHideEnd()
        {
            _state = UIPanelState.Hidden;
            OnHideEnd();
            _mainPanel.gameObject.SetActive(false);

            if (OnHideEvent != null)
            {
                OnHideEvent(this);
            }

            if (_disposed)
            {
                Close();
            }
        }


        internal void CustomUpdate()
        {
            OnUpdate();
        }

        internal void BackPressed()
        {
            if(OnBackPressed())
            {
                UIManager.Instance.ClosePopup(this);
            }
        }

        internal void OverlayPressed()
        {
            if (OnOverlayPressed())
            {
                UIManager.Instance.ClosePopup(this);
            }
        }

        private void UpdateShowStatus()
        {
            if (!_updatingShowStatus)
            {
                if (!CheckShowStatus())
                {
                    StartCoroutine(UpdateShowStatusRoutine());
                }
            }
        }

        private IEnumerator UpdateShowStatusRoutine()
        {
            _updatingShowStatus = true;
            while (!CheckShowStatus())
            {
                yield return null;
            }
            _updatingShowStatus = false;
        }

        private bool CheckShowStatus()
        {
            bool shouldBeShown = _shownByUser && _shownBySystem && !_disposed;

            if (_state == UIPanelState.Showed)
            {
                if (!shouldBeShown)
                {
                    InternalHide();
                }
                return true;
            }
            else if (_state == UIPanelState.Hidden)
            {
                if (shouldBeShown)
                {
                    InternalShow();
                }
                return true;
            }

            return false;
        }


        /// <summary>
        /// Function To flag a panel with a tag in a builder patter way
        /// </summary>
        public UIPanel SetTag(string tag)
        {
            _panelTag = tag;
            return this;
        }


        protected virtual void OnUpdate()
        {
            // Overridable
        }

        protected virtual void OnShowStart()
        {
            // Overridable
        }

        protected virtual void OnShowEnd()
        {
            // Overridable
        }

        protected virtual void OnHideStart()
        {
            // Overridable
        }

        protected virtual void OnHideEnd()
        {
            // Overridable
        }

        protected virtual void OnOpen()
        {
            // Overridable
        }

        protected virtual void OnClose()
        {
            // Overridable
        }

        protected virtual bool OnBackPressed()
        {
            return true;
        }

        protected virtual bool OnOverlayPressed()
        {
            return true;
        }
    }
}

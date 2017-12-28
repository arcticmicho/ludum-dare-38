using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text;

namespace GameModules
{
    public sealed class DebugManager : MonoSingleton<DebugManager>
    {
        public const float kFpsInterval = 0.5F;

        private class ButtonInfo
        {
            public GameObject Instance;
            public SectionInfo Section;
        }

        private class ToggleInfo : ButtonInfo
        {
            public bool Value;
        }

        private class SectionInfo
        {
            public string Name;
            public GameObject Instance;
            public SectionInfo PreviousSection;
            public List<GameObject> Buttons = new List<GameObject>();
        }

        private Dictionary<string, SectionInfo> _sectionDictionary = new Dictionary<string, SectionInfo>();
        private Dictionary<string, ToggleInfo> _toggleDictionary = new Dictionary<string, ToggleInfo>();
        private Dictionary<string, ButtonInfo> _buttonDictionary = new Dictionary<string, ButtonInfo>();
        private Dictionary<string, string> _textDictionary = new Dictionary<string, string>();

        [SerializeField]
        private Button _openDebugButton;

        [SerializeField]
        private Text _debugText;

        [SerializeField]
        private GameObject _debugPanel;

        [SerializeField]
        private Button _baseDebugButton;

        [SerializeField]
        private Transform _content;

        [SerializeField]
        private bool _showFPS = true;

        private Text _closeButtonText;
        private SectionInfo _mainSection;

        private SectionInfo _currentSection;
        private bool _showCloseButton;

        private float _accumulatedFps = 0;
        private int _framesCount = 0;
        private float _remainingTime;

        protected override bool DestroyOnLoad()
        {
            return false;
        }

        protected override void OnAwake()
        {
            base.OnAwake();
#if !DEBUG
                Destroy(gameObject);
#else
            Initialize();
#endif
        }

        private void Initialize()
        {
            _openDebugButton.onClick.AddListener(OpenDebug);
            _baseDebugButton.onClick.AddListener(BaseButtonEventListener);
            _closeButtonText = _baseDebugButton.GetComponentInChildren<Text>();

            if (_showFPS)
            {
                _remainingTime = kFpsInterval;
            }

            _mainSection = new SectionInfo();
            ShowSection(_mainSection);

            CloseDebug();
        }

        private void Update()
        {
            if (_showFPS)
            {
                _remainingTime -= Time.deltaTime;
                _accumulatedFps += Time.timeScale / Time.deltaTime;
                ++_framesCount;

                if (_remainingTime <= 0.0)
                {
                    UpdateCloseButtonText();

                    _remainingTime = kFpsInterval;
                    _accumulatedFps = 0.0F;
                    _framesCount = 0;
                }
            }
        }

        private void UpdateCloseButtonText()
        {
            float fps = _framesCount == 0 ? 0 : _accumulatedFps / _framesCount;
            string color = fps < 10 ? "FF0000FF" : fps < 30 ? "FFFF00FF" : "00FF00FF";
            string name = _showCloseButton ? "Close" : "Back";
            _closeButtonText.text = string.Format("{0}\nFPS: <color=#{1}>{2:F0}</color>", name, color, fps);
        }

        [System.Diagnostics.Conditional("DEBUG")]
        public void AddDebugToggleAction(string text, bool initialValue, Action<bool> action, bool closeOnClick)
        {
            if (!_toggleDictionary.ContainsKey(text))
            {
                string[] splittedText = text.Split('/');
                string key = splittedText[splittedText.Length - 1];

                SectionInfo section = AddOrGetSection(splittedText);

                var newButton = Instantiate(_baseDebugButton.gameObject);
                newButton.transform.SetParent(_content, false);

                SetButtonActions(newButton, () =>
                {
                    bool newValue = !_toggleDictionary[text].Value;
                    action(newValue);
                    _toggleDictionary[text].Value = newValue;
                    newButton.GetComponentInChildren<Text>().text = key + (newValue ? ": On" : ": Off");
                }, closeOnClick);

                newButton.GetComponentInChildren<Text>().text = key + (initialValue ? ": On" : ": Off");

                action(initialValue);
                _toggleDictionary.Add(text, new ToggleInfo() { Instance = newButton, Value = initialValue, Section = section });

                section.Buttons.Add(newButton);
                newButton.SetActive(_currentSection == section);
            }
        }

        private SectionInfo AddOrGetSection(string[] splittedText)
        {
            SectionInfo section = _mainSection;
            for (int i = 0; i < splittedText.Length - 1; i++)
            {
                string key = splittedText[i];
                if (_sectionDictionary.ContainsKey(key))
                {
                    section = _sectionDictionary[key];
                }
                else
                {
                    section = AddSection(key, section);
                }
            }
            return section;
        }

        private void ShowSection(SectionInfo section)
        {
            ClearCurrentSection();
            foreach (var button in section.Buttons)
            {
                button.SetActive(true);
            }
            _currentSection = section;
            _showCloseButton = _currentSection == _mainSection;
            UpdateCloseButtonText();
        }

        private void ClearCurrentSection()
        {
            if (_currentSection == null)
                return;

            foreach (var button in _currentSection.Buttons)
            {
                button.SetActive(false);
            }
        }

        [System.Diagnostics.Conditional("DEBUG")]
        public void RemoveDebugToggleAction(string text)
        {
            ToggleInfo obj = null;
            if (_toggleDictionary.TryGetValue(text, out obj))
            {
                obj.Section.Buttons.Remove(obj.Instance);
                Destroy(obj.Instance);
                _toggleDictionary.Remove(text);
            }
        }

        [System.Diagnostics.Conditional("DEBUG")]
        public void AddDebugAction(string text, Action action, bool closeOnClick)
        {
            if (!_buttonDictionary.ContainsKey(text))
            {
                string[] splittedText = text.Split('/');
                string key = splittedText[splittedText.Length - 1];

                SectionInfo section = AddOrGetSection(splittedText);

                var newButton = Instantiate(_baseDebugButton.gameObject);
                newButton.transform.SetParent(_content, false);

                SetButtonActions(newButton, action, closeOnClick);

                newButton.GetComponentInChildren<Text>().text = key;

                _buttonDictionary.Add(text, new ButtonInfo() { Instance = newButton, Section = section });

                section.Buttons.Add(newButton);
                newButton.SetActive(_currentSection == section);
            }
            else
            {
                Debug.LogWarning("Trying to add a new debug button with a name that already exists.");
            }
        }

        private SectionInfo AddSection(string sectionName, SectionInfo lastSection)
        {
            var newSection = new SectionInfo() { Name = sectionName, PreviousSection = lastSection };
            _sectionDictionary.Add(sectionName, newSection);

            if (!_buttonDictionary.ContainsKey(sectionName))
            {
                var newButton = Instantiate(_baseDebugButton.gameObject);
                newButton.transform.SetParent(_content, false);
                newButton.SetActive(_currentSection == lastSection);

                SetButtonActions(newButton, () =>
                {
                    OnSectionButtonClicked(sectionName);
                }, false);

                newButton.GetComponentInChildren<Text>().text = "[" + sectionName + "]";

                _buttonDictionary.Add(sectionName, new ButtonInfo() { Instance = newButton, Section = lastSection });
                lastSection.Buttons.Add(newButton);
            }
            else
            {
                Debug.LogWarning("Trying to add a new section with a name that already exists.");
            }

            return newSection;
        }

        private void OnSectionButtonClicked(string sectionName)
        {
            SectionInfo section = _sectionDictionary[sectionName];
            ShowSection(section);
        }

        private void SetButtonActions(GameObject instance, Action action, bool closeOnClick)
        {
            Button button = instance.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => { action(); });

            if (closeOnClick)
            {
                button.onClick.AddListener(CloseDebug);
            }
        }

        [System.Diagnostics.Conditional("DEBUG")]
        public void RemoveDebugAction(string text)
        {
            ButtonInfo obj = null;
            if (_buttonDictionary.TryGetValue(text, out obj))
            {
                obj.Section.Buttons.Remove(obj.Instance);
                Destroy(obj.Instance);
                _buttonDictionary.Remove(text);
            }
        }

        private void UpdateDebugText()
        {
            StringBuilder text = new StringBuilder();

            foreach (var t in _textDictionary)
            {
                text.Append(t.Value);
                text.Append(" ");
            }

            _debugText.text = text.ToString();
        }

        [System.Diagnostics.Conditional("DEBUG")]
        public void SetDebugText(string key, string text)
        {
            string obj = null;
            if (!_textDictionary.TryGetValue(key, out obj))
            {
                _textDictionary.Add(key, text);
            }
            else
            {
                _textDictionary[key] = text;
            }
            UpdateDebugText();
        }

        [System.Diagnostics.Conditional("DEBUG")]
        public void RemoveDebugText(string key)
        {
            if (_textDictionary.ContainsKey(key))
            {
                _textDictionary.Remove(key);
            }
        }

        private void OpenDebug()
        {
            _debugPanel.SetActive(true);
        }

        private void BaseButtonEventListener()
        {
            if (_showCloseButton)
            {
                CloseDebug();
            }
            else
            {
                ShowSection(_currentSection.PreviousSection);
            }
        }

        private void CloseDebug()
        {
            _debugPanel.SetActive(false);
        }
    }
}
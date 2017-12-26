using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameModules
{
    [CreateAssetMenu(menuName = "Nerdvania/UI/UIManager Configuration", fileName = "UIManagerConfiguration")]
    public class UIManagerConfiguration : ScriptableObject
    {
        [SerializeField]
        private bool _updateHudPanels = true;

        [SerializeField]
        private UIPanelList[] _panelLists;

        [SerializeField]
        private UIPanelItemList _panelItemList;

        #region Get/Set
        public bool UpdateHudPanels
        {
            get { return _updateHudPanels; }
        }

        public UIPanelItemList PanelItemList
        {
            get { return _panelItemList; }
        }

        public UIPanelList[] PanelLists
        {
            get { return _panelLists; }
        }
        #endregion

    }
}

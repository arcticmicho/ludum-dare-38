using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameModules
{
    [CreateAssetMenu(menuName = "Nerdvania/UI/PanelItemList", fileName = "UIPanelItemList")]
    public class UIPanelItemList : ScriptableObject
    {
        [SerializeField]
        private UIPanelItem[] _panelItems;

        #region Get/Set
        public UIPanelItem[] PanelItems
        {
            get { return _panelItems; }
        }
        #endregion
    }
}

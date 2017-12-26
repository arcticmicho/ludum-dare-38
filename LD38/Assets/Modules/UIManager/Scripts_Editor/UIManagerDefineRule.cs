using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace GameModules
{
    [InitializeOnLoad]
    public class UIManagerDefineRule
    {
        private const string kSymbol = "UI_MANAGER";

        static UIManagerDefineRule()
        {
            BuildTools.AddDefineSymbol(kSymbol);
        }
    }
}
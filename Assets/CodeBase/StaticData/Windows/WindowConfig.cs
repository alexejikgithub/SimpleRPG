using System;
using SimpleRPG.UI.Services.Windows;
using SimpleRPG.UI.Windows;

namespace SimpleRPG.StaticData.Windows
{
    [Serializable]
    public class WindowConfig
    {
        public WindowId WindowId;
        public WindowBase Prefab;
    }
}
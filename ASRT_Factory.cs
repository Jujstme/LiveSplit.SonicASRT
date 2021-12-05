using System;
using System.Reflection;
using LiveSplit.Model;
using LiveSplit.UI.Components;
using LiveSplit.SonicASRT;

[assembly: ComponentFactory(typeof(SonicASRTFactory))]

namespace LiveSplit.SonicASRT
{
    public class SonicASRTFactory : IComponentFactory
    {
        public string ComponentName => "Sonic & All-Stars Racing Transformed";
        public string Description => "Automatic splitting and IGT calculation";
        public ComponentCategory Category => ComponentCategory.Control;
        public string UpdateName => this.ComponentName;
        public string UpdateURL => "https://raw.githubusercontent.com/Jujstme/LiveSplit.SonicASRT/master/";
        public Version Version => Assembly.GetExecutingAssembly().GetName().Version;
        public string XMLURL => this.UpdateURL + "Components/update.LiveSplit.SonicASRT.xml";
        public IComponent Create(LiveSplitState state) { return new SonicASRTComponent(state); }
    }
}

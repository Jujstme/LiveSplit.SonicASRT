using LiveSplit.Model;
using LiveSplit.UI.Components;
using LiveSplit.UI;
using System;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using System.Windows.Forms;

namespace LiveSplit.SonicASRT
{
    partial class Component : LogicComponent
    {
        private GameVariables vars = new GameVariables();
        public override string ComponentName => vars.GameName;
        private Process game;
        private TimerModel timer;
        private Timer update_timer;
        private Settings settings { get; set; }

        public Component(LiveSplitState state)
        {
            timer = new TimerModel { CurrentState = state };
            update_timer = new Timer() { Interval = 1000/vars.refreshRate, Enabled = true };
            settings = new Settings();
            update_timer.Tick += updateLogic;
        }

        public override void Dispose()
        {
            settings.Dispose();
            update_timer?.Dispose();
        }

        private void updateLogic(object sender, EventArgs eventArgs)
        {
            if (game == null || game.HasExited)
            {
                if (!HookGameProcess()) return;
            }
            UpdateGameMemory();
            Update();
            if (timer.CurrentState.CurrentPhase == TimerPhase.NotRunning) StartTimer();
            if (timer.CurrentState.CurrentPhase == TimerPhase.Running)
            {
                IsLoading();
                GameTime();
                ResetLogic();
                SplitLogic();
            }
        }

        public override XmlNode GetSettings(XmlDocument document) { return this.settings.GetSettings(document); }

        public override Control GetSettingsControl(LayoutMode mode) { return this.settings; }

        public override void SetSettings(XmlNode settings) { this.settings.SetSettings(settings); }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode) { }

        private bool HookGameProcess()
        {
            foreach (var process in vars.ExeName)
            {
                game = Process.GetProcessesByName(process).OrderByDescending(x => x.StartTime).FirstOrDefault(x => !x.HasExited);
                if (game == null) continue;
                if (Init())
                {
                    return true;
                }
                else
                {
                    game = null;
                    return false;
                }
            }
            return false;
        }
    }
}

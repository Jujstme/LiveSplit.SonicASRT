using System;
using System.Xml;
using System.Windows.Forms;
using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;

namespace LiveSplit.SonicASRT
{
    class SonicASRTComponent : LogicComponent
    {
        public override string ComponentName => "Sonic & All-Stars Racing Transformed";
        private Settings settings { get; set; }
        private readonly TimerModel timer;
        private readonly System.Timers.Timer update_timer;
        private readonly SplittingLogic SplittingLogic;

        public SonicASRTComponent(LiveSplitState state)
        {
            timer = new TimerModel { CurrentState = state };
            settings = new Settings();

            SplittingLogic = new SplittingLogic();
            SplittingLogic.OnTimerCheck += OnTimerCheck;
            SplittingLogic.OnGameTimeTrigger += OnGameTimeTrigger;
            SplittingLogic.OnStartTrigger += OnStartTrigger;
            SplittingLogic.OnSplitTrigger += OnSplitTrigger;
            SplittingLogic.OnSplitTrigger_WorldTour += OnSplitTrigger_WorldTour;

            update_timer = new System.Timers.Timer() { Interval = 15, Enabled = true, AutoReset = false };
            update_timer.Elapsed += delegate { SplittingLogic.Update(); update_timer.Start(); };
        }

        void OnStartTrigger(object sender, EventArgs e)
        {
            if (timer.CurrentState.CurrentPhase == TimerPhase.NotRunning && settings.RunStart) timer.Start();
        }

        void OnSplitTrigger(object sender, Tracks type)
        {
            if (timer.CurrentState.CurrentPhase != TimerPhase.Running) return;
            switch (type)
            {
                case Tracks.OceanView: if (settings.OceanView) timer.Split(); break;
                case Tracks.SambaStudios: if (settings.SambaStudios) timer.Split(); break;
                case Tracks.CarrierZone: if (settings.CarrierZone) timer.Split(); break;
                case Tracks.DragonCanyon: if (settings.DragonCanyon) timer.Split(); break;
                case Tracks.TempleTrouble: if (settings.TempleTrouble) timer.Split(); break;
                case Tracks.GalacticParade: if (settings.GalacticParade) timer.Split(); break;
                case Tracks.SeasonalShrines: if (settings.SeasonalShrines) timer.Split(); break;
                case Tracks.RoguesLanding: if (settings.RoguesLanding) timer.Split(); break;
                case Tracks.DreamValley: if (settings.DreamValley) timer.Split(); break;
                case Tracks.ChillyCastle: if (settings.ChillyCastle) timer.Split(); break;
                case Tracks.GraffitiCity: if (settings.GraffitiCity) timer.Split(); break;
                case Tracks.SanctuaryFalls: if (settings.SanctuaryFalls) timer.Split(); break;
                case Tracks.GraveyardGig: if (settings.GraveyardGig) timer.Split(); break;
                case Tracks.AddersLair: if (settings.AddersLair) timer.Split(); break;
                case Tracks.BurningDepths: if (settings.BurningDepths) timer.Split(); break;
                case Tracks.RaceOfAges: if (settings.RaceOfAges) timer.Split(); break;
                case Tracks.SunshineTour: if (settings.SunshineTour) timer.Split(); break;
                case Tracks.ShibuyaDowntown: if (settings.ShibuyaDowntown) timer.Split(); break;
                case Tracks.RouletteRoad: if (settings.RouletteRoad) timer.Split(); break;
                case Tracks.EggHangar: if (settings.EggHangar) timer.Split(); break;
                case Tracks.OutrunBay: if (settings.OutrunBay) timer.Split(); break;
            }
        }

        void OnSplitTrigger_WorldTour(object sender, WorldTourTrack type)
        {
            if (timer.CurrentState.CurrentPhase != TimerPhase.Running) return;
            switch (type)
            {
                case WorldTourTrack.CoastalCruise: if (settings.CoastalCruise) timer.Split(); break;
                case WorldTourTrack.StudioScrapes: if (settings.StudioScrapes) timer.Split(); break;
                case WorldTourTrack.BattlezoneBlast: if (settings.BattlezoneBlast) timer.Split(); break;
                case WorldTourTrack.DowntownDrift: if (settings.DowntownDrift) timer.Split(); break;
                case WorldTourTrack.MonkeyMayhem: if (settings.MonkeyMayhem) timer.Split(); break;
                case WorldTourTrack.StarrySpeedway: if (settings.StarrySpeedway) timer.Split(); break;
                case WorldTourTrack.RouletteRush: if (settings.RouletteRush) timer.Split(); break;
                case WorldTourTrack.CanyonCarnage: if (settings.CanyonCarnage) timer.Split(); break;
                case WorldTourTrack.SnowballShakedown: if (settings.SnowballShakedown) timer.Split(); break;
                case WorldTourTrack.BananaBoost: if (settings.BananaBoost) timer.Split(); break;
                case WorldTourTrack.ShinobiScramble: if (settings.ShinobiScramble) timer.Split(); break;
                case WorldTourTrack.SeasideScrap: if (settings.SeasideScrap) timer.Split(); break;
                case WorldTourTrack.TrickyTraffic: if (settings.TrickyTraffic) timer.Split(); break;
                case WorldTourTrack.StudioScurry: if (settings.StudioScurry) timer.Split(); break;
                case WorldTourTrack.GraffitiGroove: if (settings.GraffitiGroove) timer.Split(); break;
                case WorldTourTrack.ShakingSkies: if (settings.ShakingSkies) timer.Split(); break;
                case WorldTourTrack.NeonKnockout: if (settings.NeonKnockout) timer.Split(); break;
                case WorldTourTrack.PiratePlunder: if (settings.PiratePlunder) timer.Split(); break;
                case WorldTourTrack.AdderAssault: if (settings.AdderAssault) timer.Split(); break;
                case WorldTourTrack.DreamyDrive: if (settings.DreamyDrive) timer.Split(); break;
                case WorldTourTrack.SanctuarySpeedway: if (settings.SanctuarySpeedway) timer.Split(); break;
                case WorldTourTrack.KeilsCarnage: if (settings.KeilsCarnage) timer.Split(); break;
                case WorldTourTrack.CarrierCrisis: if (settings.CarrierCrisis) timer.Split(); break;
                case WorldTourTrack.SunshineSlide: if (settings.SunshineSlide) timer.Split(); break;
                case WorldTourTrack.RogueRings: if (settings.RogueRings) timer.Split(); break;
                case WorldTourTrack.SeasideSkirmish: if (settings.SeasideSkirmish) timer.Split(); break;
                case WorldTourTrack.ShrineTime: if (settings.ShrineTime) timer.Split(); break;
                case WorldTourTrack.HangarHassle: if (settings.HangarHassle) timer.Split(); break;
                case WorldTourTrack.BootyBoost: if (settings.BootyBoost) timer.Split(); break;
                case WorldTourTrack.RacingRangers: if (settings.RacingRangers) timer.Split(); break;
                case WorldTourTrack.ShinobiShowdown: if (settings.ShinobiShowdown) timer.Split(); break;
                case WorldTourTrack.RuinRun: if (settings.RuinRun) timer.Split(); break;
                case WorldTourTrack.MonkeyBrawl: if (settings.MonkeyBrawl) timer.Split(); break;
                case WorldTourTrack.CrumblingChaos: if (settings.CrumblingChaos) timer.Split(); break;
                case WorldTourTrack.HatcherHustle: if (settings.HatcherHustle) timer.Split(); break;
                case WorldTourTrack.DeathEggDuel: if (settings.DeathEggDuel) timer.Split(); break;
                case WorldTourTrack.UndertakerOvertaker: if (settings.UndertakerOvertaker) timer.Split(); break;
                case WorldTourTrack.GoldenGauntlet: if (settings.GoldenGauntlet) timer.Split(); break;
                case WorldTourTrack.CarnivalClash: if (settings.CarnivalClash) timer.Split(); break;
                case WorldTourTrack.CurienCurves: if (settings.CurienCurves) timer.Split(); break;
                case WorldTourTrack.MoltenMayhem: if (settings.MoltenMayhem) timer.Split(); break;
                case WorldTourTrack.SpeedingSeasons: if (settings.SpeedingSeasons) timer.Split(); break;
                case WorldTourTrack.BurningBoost: if (settings.BurningBoost) timer.Split(); break;
                case WorldTourTrack.OceanOutrun: if (settings.OceanOutrun) timer.Split(); break;
                case WorldTourTrack.BillyBackslide: if (settings.BillyBackslide) timer.Split(); break;
                case WorldTourTrack.CarrierCharge: if (settings.CarrierCharge) timer.Split(); break;
                case WorldTourTrack.JetSetJaunt: if (settings.JetSetJaunt) timer.Split(); break;
                case WorldTourTrack.ArcadeAnnihilation: if (settings.ArcadeAnnihilation) timer.Split(); break;
                case WorldTourTrack.RapidRuins: if (settings.RapidRuins) timer.Split(); break;
                case WorldTourTrack.ZombieZoom: if (settings.ZombieZoom) timer.Split(); break;
                case WorldTourTrack.MaracarMadness: if (settings.MaracarMadness) timer.Split(); break;
                case WorldTourTrack.NightmareMeander: if (settings.NightmareMeander) timer.Split(); break;
                case WorldTourTrack.MaracaMelee: if (settings.MaracaMelee) timer.Split(); break;
                case WorldTourTrack.CastleChaos: if (settings.CastleChaos) timer.Split(); break;
                case WorldTourTrack.VolcanoVelocity: if (settings.VolcanoVelocity) timer.Split(); break;
                case WorldTourTrack.RangerRush: if (settings.RangerRush) timer.Split(); break;
                case WorldTourTrack.TokyoTakeover: if (settings.TokyoTakeover) timer.Split(); break;
                case WorldTourTrack.FatalFinale: if (settings.FatalFinale) timer.Split(); break;
            }
        }

        private bool OnTimerCheck(object sender, EventArgs e) // Returns true if the timer is NOT running
        {
            if (!timer.CurrentState.IsGameTimePaused) timer.CurrentState.IsGameTimePaused = true;
            return timer.CurrentState.CurrentPhase == TimerPhase.NotRunning;
        }

        private void OnGameTimeTrigger(object sender, double value)
        {
            if (timer.CurrentState.CurrentPhase == TimerPhase.Running) timer.CurrentState.SetGameTime(TimeSpan.FromSeconds(value));
        }

        public override void Dispose() { this.settings.Dispose(); this.update_timer?.Dispose(); }

        public override XmlNode GetSettings(XmlDocument document) { return this.settings.GetSettings(document); }

        public override Control GetSettingsControl(LayoutMode mode) { return this.settings; }

        public override void SetSettings(XmlNode settings) { this.settings.SetSettings(settings); }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode) { }
    }
}

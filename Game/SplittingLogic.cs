using System;
using System.Threading;

namespace LiveSplit.SonicASRT
{
    class SplittingLogic
    {
        private Watchers watchers;

        public delegate bool TimerCheckEventHandler(object sender, EventArgs e);
        public event TimerCheckEventHandler OnTimerCheck;
        public event EventHandler<double> OnGameTimeTrigger;
        public event EventHandler OnStartTrigger;
        public event EventHandler<WorldTourTrack> OnSplitTrigger_WorldTour;
        public event EventHandler<Tracks> OnSplitTrigger;

        public void Update()
        {
            if (!VerifyOrHookGameProcess()) return;
            watchers.Update();
            UpdateTimers();
            Start();
            GameTime();
            Split();
        }

        void Start()
        {
            if (!watchers.StartTriggered) return;
            switch (watchers.GameMode)
            {
                case GameMode.GrandPrix:
                case GameMode.SingleRace:
                    this.OnStartTrigger?.Invoke(this, EventArgs.Empty);
                    break;
                case GameMode.WorldTour:
                    if (watchers.CoastalCruise.Current + watchers.CanyonCarnage.Current == 0) this.OnStartTrigger?.Invoke(this, EventArgs.Empty);
                    break;
            }
        }

        private void UpdateTimers()
        {
            if (TimerNotRunning())
            {
                watchers.totalIGT = 0d;
                watchers.progressIGT = 0d;
            }

            if (!watchers.raceCompleted.Current)
            {
                if (watchers.IGT.Old != 0 && watchers.IGT.Current == 0 && watchers.raceStatus.Old == 4)
                {
                    watchers.totalIGT += watchers.IGT.Old;
                    watchers.progressIGT = watchers.totalIGT;

                }
                else
                {
                    watchers.progressIGT = watchers.totalIGT + watchers.IGT.Current;
                }
            }

            if (watchers.raceCompleted.Current && !watchers.raceCompleted.Old)
            {
                watchers.totalIGT += (watchers.GameMode == GameMode.WorldTour && watchers.requiredLaps.Current == 255) || watchers.eventType.Current == 0xE64B5DD8 ? watchers.IGT.Current : watchers.totalRaceTime.Current;
                watchers.progressIGT = watchers.totalIGT;
            }
        }

        private void GameTime()
        {
            this.OnGameTimeTrigger?.Invoke(this, watchers.progressIGT);
        }

        private void Split()
        {
            switch (watchers.GameMode)
            {
                case GameMode.WorldTour:
                    if (watchers.CoastalCruise.Current > watchers.CoastalCruise.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.CoastalCruise);
                    else if (watchers.StudioScrapes.Current > watchers.StudioScrapes.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.StudioScrapes);
                    else if (watchers.BattleZoneBlast.Current > watchers.BattleZoneBlast.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.BattlezoneBlast);
                    else if (watchers.DowntownDrift.Current > watchers.DowntownDrift.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.DowntownDrift);
                    else if (watchers.MonkeyMayhem.Current > watchers.MonkeyMayhem.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.MonkeyMayhem);
                    else if (watchers.StarrySpeedway.Current > watchers.StarrySpeedway.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.StarrySpeedway);
                    else if (watchers.RouletteRush.Current > watchers.RouletteRush.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.RouletteRush);
                    else if (watchers.CanyonCarnage.Current > watchers.CanyonCarnage.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.CanyonCarnage);
                    else if (watchers.SnowballShakedown.Current > watchers.SnowballShakedown.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.SnowballShakedown);
                    else if (watchers.BananaBoost.Current > watchers.BananaBoost.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.BananaBoost);
                    else if (watchers.ShinobiScramble.Current > watchers.ShinobiScramble.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.ShinobiScramble);
                    else if (watchers.SeasideScrap.Current > watchers.SeasideScrap.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.SeasideScrap);
                    else if (watchers.TrickyTraffic.Current > watchers.TrickyTraffic.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.TrickyTraffic);
                    else if (watchers.StudioScurry.Current > watchers.StudioScurry.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.StudioScurry);
                    else if (watchers.GraffitiGroove.Current > watchers.GraffitiGroove.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.GraffitiGroove);
                    else if (watchers.ShakingSkies.Current > watchers.ShakingSkies.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.ShakingSkies);
                    else if (watchers.NeonKnockout.Current > watchers.NeonKnockout.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.NeonKnockout);
                    else if (watchers.PiratePlunder.Current > watchers.PiratePlunder.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.PiratePlunder);
                    else if (watchers.AdderAssault.Current > watchers.AdderAssault.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.AdderAssault);
                    else if (watchers.DreamyDrive.Current > watchers.DreamyDrive.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.DreamyDrive);
                    else if (watchers.SanctuarySpeedway.Current > watchers.SanctuarySpeedway.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.SanctuarySpeedway);
                    else if (watchers.KeilsCarnage.Current > watchers.KeilsCarnage.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.KeilsCarnage);
                    else if (watchers.CarrierCrisis.Current > watchers.CarrierCrisis.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.CarrierCrisis);
                    else if (watchers.SunshineSlide.Current > watchers.SunshineSlide.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.SunshineSlide);
                    else if (watchers.RogueRings.Current > watchers.RogueRings.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.RogueRings);
                    else if (watchers.SeasideSkirmish.Current > watchers.SeasideSkirmish.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.SeasideSkirmish);
                    else if (watchers.ShrineTime.Current > watchers.ShrineTime.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.ShrineTime);
                    else if (watchers.HangarHassle.Current > watchers.HangarHassle.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.HangarHassle);
                    else if (watchers.BootyBoost.Current > watchers.BootyBoost.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.BootyBoost);
                    else if (watchers.RacingRangers.Current > watchers.RacingRangers.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.RacingRangers);
                    else if (watchers.ShinobiShowdown.Current > watchers.ShinobiShowdown.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.ShinobiShowdown);
                    else if (watchers.RuinRun.Current > watchers.RuinRun.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.RuinRun);
                    else if (watchers.MonkeyBrawl.Current > watchers.MonkeyBrawl.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.MonkeyBrawl);
                    else if (watchers.CrumblingChaos.Current > watchers.CrumblingChaos.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.CrumblingChaos);
                    else if (watchers.HatcherHustle.Current > watchers.HatcherHustle.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.HatcherHustle);
                    else if (watchers.DeathEggDuel.Current > watchers.DeathEggDuel.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.DeathEggDuel);
                    else if (watchers.UndertakerOvertaker.Current > watchers.UndertakerOvertaker.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.UndertakerOvertaker);
                    else if (watchers.GoldenGauntlet.Current > watchers.GoldenGauntlet.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.GoldenGauntlet);
                    else if (watchers.CarnivalClash.Current > watchers.CarnivalClash.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.CarnivalClash);
                    else if (watchers.CurienCurves.Current > watchers.CurienCurves.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.CurienCurves);
                    else if (watchers.MoltenMayhem.Current > watchers.MoltenMayhem.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.MoltenMayhem);
                    else if (watchers.SpeedingSeasons.Current > watchers.SpeedingSeasons.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.SpeedingSeasons);
                    else if (watchers.BurningBoost.Current > watchers.BurningBoost.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.BurningBoost);
                    else if (watchers.OceanOutrun.Current > watchers.OceanOutrun.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.OceanOutrun);
                    else if (watchers.BillyBackslide.Current > watchers.BillyBackslide.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.BillyBackslide);
                    else if (watchers.CarrierCharge.Current > watchers.CarrierCharge.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.CarrierCharge);
                    else if (watchers.JetSetJaunt.Current > watchers.JetSetJaunt.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.JetSetJaunt);
                    else if (watchers.ArcadeAnnihilation.Current == 4 && watchers.ArcadeAnnihilation.Changed) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.ArcadeAnnihilation);
                    else if (watchers.endCredits.Current && !watchers.endCredits.Old && watchers.ArcadeAnnihilation.Current != 4) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.ArcadeAnnihilation);
                    else if (watchers.RapidRuins.Current > watchers.RapidRuins.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.RapidRuins);
                    else if (watchers.ZombieZoom.Current > watchers.ZombieZoom.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.ZombieZoom);
                    else if (watchers.MaracarMadness.Current > watchers.MaracarMadness.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.MaracarMadness);
                    else if (watchers.NightmareMeander.Current > watchers.NightmareMeander.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.NightmareMeander);
                    else if (watchers.MaracaMelee.Current > watchers.MaracaMelee.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.MaracaMelee);
                    else if (watchers.CastleChaos.Current > watchers.CastleChaos.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.CastleChaos);
                    else if (watchers.VolcanoVelocity.Current > watchers.VolcanoVelocity.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.VolcanoVelocity);
                    else if (watchers.RangerRush.Current > watchers.RangerRush.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.RangerRush);
                    else if (watchers.TokyoTakeover.Current > watchers.TokyoTakeover.Old) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.TokyoTakeover);
                    else if (watchers.FatalFinale.Current != 4 && watchers.FatalFinale.Changed) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.FatalFinale);
                    else if (watchers.endCredits.Current && !watchers.endCredits.Old && watchers.FatalFinale.Current == 4) this.OnSplitTrigger_WorldTour.Invoke(this, WorldTourTrack.FatalFinale);
                    break;
                case GameMode.GrandPrix:
                case GameMode.SingleRace:
                    if (watchers.raceCompleted.Current && !watchers.raceCompleted.Old) this.OnSplitTrigger?.Invoke(this, watchers.CurrentTrack);
                    break;
            }
        }

        bool TimerNotRunning()
        {
            return this.OnTimerCheck.Invoke(this, EventArgs.Empty);
        }

        bool VerifyOrHookGameProcess()
        {
            if (watchers != null && watchers.IsGameHooked) return true;
            try { watchers = new Watchers(); } catch { Thread.Sleep(500); return false; }
            return true;
        }
    }
}

using System;
using LiveSplit.ComponentUtil;

namespace LiveSplit.SonicASRT
{
    class GameVariables
    {
        internal readonly string GameName = "Sonic & All-Stars Racing Transformed";
        internal readonly string[] ExeName = { "ASN_App_PcDx9_Final" };
        internal readonly byte refreshRate = 60;
        internal MemoryWatcherList watchers;

        // Internal variables
        internal Tracks CurrentTrack;
        internal GameMode GameMode;
        internal float totalIGT = 0, progressIGT = 0;
    }

    partial class Component
    {
        private bool Init()
        {
            var scanner = new SignatureScanner(game, game.MainModule.BaseAddress, game.MainModule.ModuleMemorySize);
            IntPtr ptr;
            vars.watchers = new MemoryWatcherList();

            // Basic checks
            if (game.Is64Bit()) return false;
            ptr = scanner.Scan(new SigScanTarget("53 6F 6E 69 63 20 26 20 41 6C 6C 2D 53 74 61 72 73 20 52 61 63 69 6E 67 20 54 72 61 6E 73 66 6F 72 6D 65 64"));
            if (ptr == IntPtr.Zero) return false;

            ptr = scanner.Scan(new SigScanTarget(2,
                "80 3D ???????? 00", // cmp byte ptr [ASN_App_PcDx9_Final.exe+852918],00  <----
                "0F85 ????????",     // jne ASN_App_PcDx9_Final.exe+467479
                "56"));              // push esi
            if (ptr == IntPtr.Zero) return false;
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr))) { Name = "runStart" }); 
            
            ptr = scanner.Scan(new SigScanTarget(4,
                "74 0E",              // je ASN_App_PcDx9_Final.exe+275701
                "83 3D ???????? 00",  // cmp dword ptr [ASN_App_PcDx9_Final.exe+7C73C0],00  <----
                "74 0E"));            // je ASN_App_PcDx9_Final.exe+27570A
            if (ptr == IntPtr.Zero) return false;
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr))) { Name = "runStart2" });

            ptr = scanner.Scan(new SigScanTarget(3,
                "7E 5C",          // jle ASN_App_PcDx9_Final.exe+51223B
                "A1 ????????"));  // mov eax,[ASN_App_PcDx9_Final.exe+A98669]  <----
            if (ptr == IntPtr.Zero) return false;
            vars.watchers.Add(new MemoryWatcher<bool>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr), 0x8C)) { Name = "endCredits", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });

            ptr = scanner.Scan(new SigScanTarget(1,
                "A1 ????????",   // mov eax,[ASN_App_PcDx9_Final.exe+856890]  <----
                "83 F8 02",      // cmp eax,02
                "74 16"));       // je ASN_App_PcDx9_Final.exe+4C6A32
            if (ptr == IntPtr.Zero) return false;
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr))) { Name = "modeSelect" });

            ptr = scanner.Scan(new SigScanTarget(1,
                "A1 ????????",      // mov eax,[ASN_App_PcDx9_Final.exe+7CE920]  <----
                "85 C0",            // test eax,eax
                "0F84 8D000000"));  // je ASN_App_PcDx9_Final.exe+47C2FD
            if (ptr == IntPtr.Zero) return false;
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr), 0x0, 0xC1B8, 0x4)) { Name = "requiredLaps", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<float>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr), 0x0, 0xC1B8, 0x28)) { Name = "totalRaceTime", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });

            ptr = scanner.Scan(new SigScanTarget(4,
                "8B 04 24",      // mov eax,[esp]
                "A3 ????????",   // mov [ASN_App_PcDx9_Final.exe+7CE930],eax  <----
                "83 C4 08"));    // add esp,08
            if (ptr == IntPtr.Zero) return false;
            vars.watchers.Add(new MemoryWatcher<bool>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr))) { Name = "raceCompleted" });

            ptr = scanner.Scan(new SigScanTarget(4,
                "7C 44",              // jl ASN_App_PcDx9_Final.exe+4D1C5
                "83 3D ???????? 00",  // cmp dword ptr [ASN_App_PcDx9_Final.exe+7CE944],00  <----
                "74 3B"));            // je ASN_App_PcDx9_Final.exe+4D1C5
            if (ptr == IntPtr.Zero) return false;
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr))) { Name = "raceStatus" });

            ptr = scanner.Scan(new SigScanTarget(2,
                "D8 05 ????????",  // fadd dword ptr [ASN_App_PcDx9_Final.exe+7CE980]  <----
                "56"));            // push esi
            if (ptr == IntPtr.Zero) return false;
            vars.watchers.Add(new MemoryWatcher<float>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr))) { Name = "IGT" });

            ptr = scanner.Scan(new SigScanTarget(5,
                "55",               // push ebp
                "8B E9",            // mov ebp,ecx
                "8B 0D ????????",   // mov ecx,[ASN_App_PcDx9_Final.exe+7C7430]  <----
                "57"));             // push edi
            if (ptr == IntPtr.Zero) return false;
            vars.watchers.Add(new MemoryWatcher<uint>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr), 0x0)) { Name = "eventType", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<uint>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 4, 0x0)) { Name = "TrackID", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });

            ptr = scanner.Scan(new SigScanTarget(3,
                "8B 2C 85 ????????",   // mov ebp,[eax*4+ASN_App_PcDx9_Final.exe+7D01F8]  <----
                "89 7C 24 20"));       // mov [esp+20],edi
            if (ptr == IntPtr.Zero) return false;
            // Sunshine Coast
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 0 * 4, 0x7C))  { Name = "CoastalCruise", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 0 * 4, 0x138)) { Name = "StudioScrapes", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 0 * 4, 0x1F4)) { Name = "BattlezoneBlast", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 0 * 4, 0x2B0)) { Name = "DowntownDrift", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 0 * 4, 0x36C)) { Name = "MonkeyMayhem", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 0 * 4, 0x428)) { Name = "StarrySpeedway", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 0 * 4, 0x4E4)) { Name = "RouletteRush", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 0 * 4, 0x5A0)) { Name = "CanyonCarnage", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            // Frozen Valley
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 1 * 4, 0x7C))  { Name = "SnowballShakedown", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 1 * 4, 0x138)) { Name = "BananaBoost", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 1 * 4, 0x1F4)) { Name = "ShinobiScramble", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 1 * 4, 0x2B0)) { Name = "SeasideScrap", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 1 * 4, 0x36C)) { Name = "TrickyTraffic", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 1 * 4, 0x428)) { Name = "StudioScurry", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 1 * 4, 0x4E4)) { Name = "GraffitiGroove", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 1 * 4, 0x5A0)) { Name = "ShakingSkies", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 1 * 4, 0x65C)) { Name = "NeonKnockout", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 1 * 4, 0x718)) { Name = "PiratePlunder", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            // Scorching Skies
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 2 * 4, 0x7C))  { Name = "AdderAssault", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 2 * 4, 0x138)) { Name = "DreamyDrive", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 2 * 4, 0x1F4)) { Name = "SanctuarySpeedway", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 2 * 4, 0x2B0)) { Name = "KeilsCarnage", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 2 * 4, 0x36C)) { Name = "CarrierCrisis", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 2 * 4, 0x428)) { Name = "SunshineSlide", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 2 * 4, 0x4E4)) { Name = "RogueRings", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 2 * 4, 0x5A0)) { Name = "SeasideSkirmish", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 2 * 4, 0x65C)) { Name = "ShrineTime", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 2 * 4, 0x718)) { Name = "HangarHassle", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            // Twilight Engine
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 3 * 4, 0x7C))  { Name = "BootyBoost", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 3 * 4, 0x138)) { Name = "RacingRangers", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 3 * 4, 0x1F4)) { Name = "ShinobiShowdown", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 3 * 4, 0x2B0)) { Name = "RuinRun", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 3 * 4, 0x36C)) { Name = "MonkeyBrawl", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 3 * 4, 0x428)) { Name = "CrumblingChaos", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 3 * 4, 0x4E4)) { Name = "HatcherHustle", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 3 * 4, 0x5A0)) { Name = "DeathEggDuel", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 3 * 4, 0x65C)) { Name = "UndertakerOvertaker", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 3 * 4, 0x718)) { Name = "GoldenGauntlet", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            // Moonlight Park
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 4 * 4, 0x7C))  { Name = "CarnivalClash", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 4 * 4, 0x138)) { Name = "CurienCurves", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 4 * 4, 0x1F4)) { Name = "MoltenMayhem", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 4 * 4, 0x2B0)) { Name = "SpeedingSeasons", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 4 * 4, 0x36C)) { Name = "BurningBoost", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 4 * 4, 0x428)) { Name = "OceanOutrun", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 4 * 4, 0x4E4)) { Name = "BillyBackslide", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 4 * 4, 0x5A0)) { Name = "CarrierCharge", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 4 * 4, 0x65C)) { Name = "JetSetJaunt", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 4 * 4, 0x718)) { Name = "ArcadeAnnihilation", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            // Superstar Showdown
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 5 * 4, 0x7C))  { Name = "RapidRuins", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 5 * 4, 0x138)) { Name = "ZombieZoom", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 5 * 4, 0x1F4)) { Name = "MaracarMadness", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 5 * 4, 0x2B0)) { Name = "NightmareMeander", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 5 * 4, 0x36C)) { Name = "MaracaMelee", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 5 * 4, 0x428)) { Name = "CastleChaos", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 5 * 4, 0x4E4)) { Name = "VolcanoVelocity", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 5 * 4, 0x5A0)) { Name = "RangerRush", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 5 * 4, 0x65C)) { Name = "TokyoTakeover", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });
            vars.watchers.Add(new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 5 * 4, 0x718)) { Name = "FatalFinale", FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull });

            vars.watchers.UpdateAll(game);
            return true;
        }

        private void UpdateGameMemory()
        {
            vars.watchers.UpdateAll(game);
            vars.CurrentTrack = (Tracks)vars.watchers["TrackID"].Current;
            vars.GameMode = (GameMode)vars.watchers["modeSelect"].Current;
        }

        private void Update()
        {
            if (!(bool)vars.watchers["raceCompleted"].Current)
            {
                if ((float)vars.watchers["IGT"].Old != 0 && (float)vars.watchers["IGT"].Current == 0 && (byte)vars.watchers["raceStatus"].Old == 4)
                {
                    vars.totalIGT += (float)vars.watchers["IGT"].Old;
                    vars.progressIGT = vars.totalIGT;
                }
                else
                {
                    vars.progressIGT = vars.totalIGT + (float)vars.watchers["IGT"].Current;
                }
            }
            
            if ((bool)vars.watchers["raceCompleted"].Current && vars.watchers["raceCompleted"].Changed)
            {
                if ((vars.GameMode == GameMode.WorldTour && (byte)vars.watchers["requiredLaps"].Current == 255) || (uint)vars.watchers["eventType"].Current == 0xE64B5DD8)
                {
                    vars.totalIGT += (float)vars.watchers["IGT"].Current;
                } else {
                    vars.totalIGT += (float)vars.watchers["totalRaceTime"].Current;
                }
                vars.progressIGT = vars.totalIGT;
            }
        }

        private void StartTimer()
        {
            vars.totalIGT = vars.progressIGT = 0;
            if (!settings.RunStart) return;
            bool startTrigger = (byte)vars.watchers["runStart"].Current == 1 && (byte)vars.watchers["runStart"].Old == 0 && (byte)vars.watchers["runStart2"].Current == 1;
            if (!startTrigger) return;

            switch (vars.GameMode)
            {
                case GameMode.WorldTour:
                    startTrigger &= (byte)vars.watchers["CoastalCruise"].Current + (byte)vars.watchers["CanyonCarnage"].Current == 0;
                    break;
                case GameMode.SingleRace:
                case GameMode.GrandPrix:
                    break;
                default:
                    startTrigger = false;
                    break;
            }

            if (startTrigger) timer.Start();
        }

        private void IsLoading()
        {
            if (!timer.CurrentState.IsGameTimePaused && settings.UseIGT) timer.CurrentState.IsGameTimePaused = true;
        }

        private void GameTime()
        {
            if (settings.UseIGT) timer.CurrentState.SetGameTime(TimeSpan.FromSeconds(vars.progressIGT));
        }

        private void ResetLogic()
        {
            // Do not implement a reset funcion for this game
            // timer.Reset();
        }

        private void SplitLogic()
        {
            bool shouldSplit = false;

            switch (vars.GameMode)
            {
                case GameMode.WorldTour:
                    bool creditsRolling = (bool)vars.watchers["endCredits"].Current && vars.watchers["endCredits"].Changed;
                    shouldSplit = ((byte)vars.watchers["CoastalCruise"].Current > (byte)vars.watchers["CoastalCruise"].Old && settings.CoastalCruise) ||
                                          ((byte)vars.watchers["StudioScrapes"].Current > (byte)vars.watchers["StudioScrapes"].Old && settings.StudioScrapes) ||
                                          ((byte)vars.watchers["BattlezoneBlast"].Current > (byte)vars.watchers["BattlezoneBlast"].Old && settings.BattlezoneBlast) ||
                                          ((byte)vars.watchers["DowntownDrift"].Current > (byte)vars.watchers["DowntownDrift"].Old && settings.DowntownDrift) ||
                                          ((byte)vars.watchers["MonkeyMayhem"].Current > (byte)vars.watchers["MonkeyMayhem"].Old && settings.MonkeyMayhem) ||
                                          ((byte)vars.watchers["StarrySpeedway"].Current > (byte)vars.watchers["StarrySpeedway"].Old && settings.StarrySpeedway) ||
                                          ((byte)vars.watchers["RouletteRush"].Current > (byte)vars.watchers["RouletteRush"].Old && settings.RouletteRush) ||
                                          ((byte)vars.watchers["CanyonCarnage"].Current > (byte)vars.watchers["CanyonCarnage"].Old && settings.CanyonCarnage) ||
                                          ((byte)vars.watchers["SnowballShakedown"].Current > (byte)vars.watchers["SnowballShakedown"].Old && settings.SnowballShakedown) ||
                                          ((byte)vars.watchers["BananaBoost"].Current > (byte)vars.watchers["BananaBoost"].Old && settings.BananaBoost) ||
                                          ((byte)vars.watchers["ShinobiScramble"].Current > (byte)vars.watchers["ShinobiScramble"].Old && settings.ShinobiScramble) ||
                                          ((byte)vars.watchers["SeasideScrap"].Current > (byte)vars.watchers["SeasideScrap"].Old && settings.SeasideScrap) ||
                                          ((byte)vars.watchers["TrickyTraffic"].Current > (byte)vars.watchers["TrickyTraffic"].Old && settings.TrickyTraffic) ||
                                          ((byte)vars.watchers["StudioScurry"].Current > (byte)vars.watchers["StudioScurry"].Old && settings.StudioScurry) ||
                                          ((byte)vars.watchers["GraffitiGroove"].Current > (byte)vars.watchers["GraffitiGroove"].Old && settings.GraffitiGroove) ||
                                          ((byte)vars.watchers["ShakingSkies"].Current > (byte)vars.watchers["ShakingSkies"].Old && settings.ShakingSkies) ||
                                          ((byte)vars.watchers["NeonKnockout"].Current > (byte)vars.watchers["NeonKnockout"].Old && settings.NeonKnockout) ||
                                          ((byte)vars.watchers["PiratePlunder"].Current > (byte)vars.watchers["PiratePlunder"].Old && settings.PiratePlunder) ||
                                          ((byte)vars.watchers["AdderAssault"].Current > (byte)vars.watchers["AdderAssault"].Old && settings.AdderAssault) ||
                                          ((byte)vars.watchers["DreamyDrive"].Current > (byte)vars.watchers["DreamyDrive"].Old && settings.DreamyDrive) ||
                                          ((byte)vars.watchers["SanctuarySpeedway"].Current > (byte)vars.watchers["SanctuarySpeedway"].Old && settings.SanctuarySpeedway) ||
                                          ((byte)vars.watchers["KeilsCarnage"].Current > (byte)vars.watchers["KeilsCarnage"].Old && settings.KeilsCarnage) ||
                                          ((byte)vars.watchers["CarrierCrisis"].Current > (byte)vars.watchers["CarrierCrisis"].Old && settings.CarrierCrisis) ||
                                          ((byte)vars.watchers["SunshineSlide"].Current > (byte)vars.watchers["SunshineSlide"].Old && settings.SunshineSlide) ||
                                          ((byte)vars.watchers["RogueRings"].Current > (byte)vars.watchers["RogueRings"].Old && settings.RogueRings) ||
                                          ((byte)vars.watchers["SeasideSkirmish"].Current > (byte)vars.watchers["SeasideSkirmish"].Old && settings.SeasideSkirmish) ||
                                          ((byte)vars.watchers["ShrineTime"].Current > (byte)vars.watchers["ShrineTime"].Old && settings.ShrineTime) ||
                                          ((byte)vars.watchers["HangarHassle"].Current > (byte)vars.watchers["HangarHassle"].Old && settings.HangarHassle) ||
                                          ((byte)vars.watchers["BootyBoost"].Current > (byte)vars.watchers["BootyBoost"].Old && settings.BootyBoost) ||
                                          ((byte)vars.watchers["RacingRangers"].Current > (byte)vars.watchers["RacingRangers"].Old && settings.RacingRangers) ||
                                          ((byte)vars.watchers["ShinobiShowdown"].Current > (byte)vars.watchers["ShinobiShowdown"].Old && settings.ShinobiShowdown) ||
                                          ((byte)vars.watchers["RuinRun"].Current > (byte)vars.watchers["RuinRun"].Old && settings.RuinRun) ||
                                          ((byte)vars.watchers["MonkeyBrawl"].Current > (byte)vars.watchers["MonkeyBrawl"].Old && settings.MonkeyBrawl) ||
                                          ((byte)vars.watchers["CrumblingChaos"].Current > (byte)vars.watchers["CrumblingChaos"].Old && settings.CrumblingChaos) ||
                                          ((byte)vars.watchers["HatcherHustle"].Current > (byte)vars.watchers["HatcherHustle"].Old && settings.HatcherHustle) ||
                                          ((byte)vars.watchers["DeathEggDuel"].Current > (byte)vars.watchers["DeathEggDuel"].Old && settings.DeathEggDuel) ||
                                          ((byte)vars.watchers["UndertakerOvertaker"].Current > (byte)vars.watchers["UndertakerOvertaker"].Old && settings.UndertakerOvertaker) ||
                                          ((byte)vars.watchers["GoldenGauntlet"].Current > (byte)vars.watchers["GoldenGauntlet"].Old && settings.GoldenGauntlet) ||
                                          ((byte)vars.watchers["CarnivalClash"].Current > (byte)vars.watchers["CarnivalClash"].Old && settings.CarnivalClash) ||
                                          ((byte)vars.watchers["CurienCurves"].Current > (byte)vars.watchers["CurienCurves"].Old && settings.CurienCurves) ||
                                          ((byte)vars.watchers["MoltenMayhem"].Current > (byte)vars.watchers["MoltenMayhem"].Old && settings.MoltenMayhem) ||
                                          ((byte)vars.watchers["SpeedingSeasons"].Current > (byte)vars.watchers["SpeedingSeasons"].Old && settings.SpeedingSeasons) ||
                                          ((byte)vars.watchers["BurningBoost"].Current > (byte)vars.watchers["BurningBoost"].Old && settings.BurningBoost) ||
                                          ((byte)vars.watchers["OceanOutrun"].Current > (byte)vars.watchers["OceanOutrun"].Old && settings.OceanOutrun) ||
                                          ((byte)vars.watchers["BillyBackslide"].Current > (byte)vars.watchers["BillyBackslide"].Old && settings.BillyBackslide) ||
                                          ((byte)vars.watchers["CarrierCharge"].Current > (byte)vars.watchers["CarrierCharge"].Old && settings.CarrierCharge) ||
                                          ((byte)vars.watchers["JetSetJaunt"].Current > (byte)vars.watchers["JetSetJaunt"].Old && settings.JetSetJaunt) ||
                                          ((byte)vars.watchers["ArcadeAnnihilation"].Current == 4 && vars.watchers["ArcadeAnnihilation"].Changed && settings.ArcadeAnnihilation) ||
                                          (creditsRolling && (byte)vars.watchers["FatalFinale"].Current != 4 && settings.ArcadeAnnihilation) ||
                                          ((byte)vars.watchers["RapidRuins"].Current > (byte)vars.watchers["RapidRuins"].Old && settings.RapidRuins) ||
                                          ((byte)vars.watchers["ZombieZoom"].Current > (byte)vars.watchers["ZombieZoom"].Old && settings.ZombieZoom) ||
                                          ((byte)vars.watchers["MaracarMadness"].Current > (byte)vars.watchers["MaracarMadness"].Old && settings.MaracarMadness) ||
                                          ((byte)vars.watchers["NightmareMeander"].Current > (byte)vars.watchers["NightmareMeander"].Old && settings.NightmareMeander) ||
                                          ((byte)vars.watchers["MaracaMelee"].Current > (byte)vars.watchers["MaracaMelee"].Old && settings.MaracaMelee) ||
                                          ((byte)vars.watchers["CastleChaos"].Current > (byte)vars.watchers["CastleChaos"].Old && settings.CastleChaos) ||
                                          ((byte)vars.watchers["VolcanoVelocity"].Current > (byte)vars.watchers["VolcanoVelocity"].Old && settings.VolcanoVelocity) ||
                                          ((byte)vars.watchers["RangerRush"].Current > (byte)vars.watchers["RangerRush"].Old && settings.RangerRush) ||
                                          ((byte)vars.watchers["TokyoTakeover"].Current > (byte)vars.watchers["TokyoTakeover"].Old && settings.TokyoTakeover) ||
                                          ((byte)vars.watchers["FatalFinale"].Current != 4 && vars.watchers["FatalFinale"].Changed && settings.FatalFinale) ||
                                          (creditsRolling && (byte)vars.watchers["FatalFinale"].Current == 4 && settings.FatalFinale);
                    break;
                case GameMode.GrandPrix:
                case GameMode.SingleRace:
                    shouldSplit = (bool)vars.watchers["raceCompleted"].Current && !(bool)vars.watchers["raceCompleted"].Old;
                    switch (vars.CurrentTrack)
                    {
                        case Tracks.OceanView: shouldSplit &= settings.OceanView; break;
                        case Tracks.SambaStudios: shouldSplit &= settings.SambaStudios; break;
                        case Tracks.CarrierZone: shouldSplit &= settings.CarrierZone; break;
                        case Tracks.DragonCanyon: shouldSplit &= settings.DragonCanyon; break;
                        case Tracks.TempleTrouble: shouldSplit &= settings.TempleTrouble; break;
                        case Tracks.GalacticParade: shouldSplit &= settings.GalacticParade; break;
                        case Tracks.SeasonalShrines: shouldSplit &= settings.SeasonalShrines; break;
                        case Tracks.RoguesLanding: shouldSplit &= settings.RoguesLanding; break;
                        case Tracks.DreamValley: shouldSplit &= settings.DreamValley; break;
                        case Tracks.ChillyCastle: shouldSplit &= settings.ChillyCastle; break;
                        case Tracks.GraffitiCity: shouldSplit &= settings.GraffitiCity; break;
                        case Tracks.SanctuaryFalls: shouldSplit &= settings.SanctuaryFalls; break;
                        case Tracks.GraveyardGig: shouldSplit &= settings.GraveyardGig; break;
                        case Tracks.AddersLair: shouldSplit &= settings.AddersLair; break;
                        case Tracks.BurningDepths: shouldSplit &= settings.BurningDepths; break;
                        case Tracks.RaceOfAges: shouldSplit &= settings.RaceOfAges; break;
                        case Tracks.SunshineTour: shouldSplit &= settings.SunshineTour; break;
                        case Tracks.ShibuyaDowntown: shouldSplit &= settings.ShibuyaDowntown; break;
                        case Tracks.RouletteRoad: shouldSplit &= settings.RouletteRoad; break;
                        case Tracks.EggHangar: shouldSplit &= settings.EggHangar; break;
                        case Tracks.OutrunBay: shouldSplit &= settings.OutrunBay; break;
                        default: shouldSplit = false; break;
                    }
                    break;
            }

            if (shouldSplit) timer.Split();
        }


    }

    internal enum Tracks : uint
    {
        OceanView = 0xD4257EBD,
        SambaStudios = 0x32D305A8,
        CarrierZone = 0xC72B3B98,
        DragonCanyon = 0x03EB7FFF,
        TempleTrouble = 0xE3121777,
        GalacticParade = 0x4E015AB6,
        SeasonalShrines = 0x503C1CBC,
        RoguesLanding = 0x7534B7CA,
        DreamValley = 0x38A394ED,
        ChillyCastle = 0xC5C9DEA1,
        GraffitiCity = 0xD936550C,
        SanctuaryFalls = 0x4A0FF7AE,
        GraveyardGig = 0xCD8017BA,
        AddersLair = 0xDC93F18B,
        BurningDepths = 0x2DB91FC2,
        RaceOfAges = 0x94610644,
        SunshineTour = 0xE6CD97F0,
        ShibuyaDowntown = 0xE87FDF22,
        RouletteRoad = 0x17463C8D,
        EggHangar = 0xFEBC639E,
        OutrunBay = 0x1EF56CE1
    }

    internal enum GameMode : byte
    {
        WorldTour = 0,
        GrandPrix = 1,
        TimeAttack = 2,
        SingleRace = 3
    }
}

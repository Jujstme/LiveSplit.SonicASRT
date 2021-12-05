using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using LiveSplit.ComponentUtil;

namespace LiveSplit.SonicASRT
{
    class Watchers : MemoryWatcherList
    {
        // Game process
        private readonly Process game;
        public bool IsGameHooked => game != null && !game.HasExited;

        // Imported game variables
        private MemoryWatcher<byte> runStart { get; }
        private MemoryWatcher<byte> runStart2 { get;}
        public MemoryWatcher<bool> endCredits { get; }
        private MemoryWatcher<byte> modeSelect { get; }
        public MemoryWatcher<byte> requiredLaps { get; }
        public MemoryWatcher<float> totalRaceTime { get; }
        public MemoryWatcher<bool> raceCompleted { get; }
        public MemoryWatcher<byte> raceStatus { get; }
        public MemoryWatcher<float> IGT { get; }
        public MemoryWatcher<uint> eventType { get; }
        private MemoryWatcher<uint> TrackID { get; }

        // Stars
        public MemoryWatcher<byte> CoastalCruise { get; }
        public MemoryWatcher<byte> StudioScrapes { get; }
        public MemoryWatcher<byte> BattleZoneBlast { get; }
        public MemoryWatcher<byte> DowntownDrift { get; }
        public MemoryWatcher<byte> MonkeyMayhem { get; }
        public MemoryWatcher<byte> StarrySpeedway { get; }
        public MemoryWatcher<byte> RouletteRush { get; }
        public MemoryWatcher<byte> CanyonCarnage { get; }
        public MemoryWatcher<byte> SnowballShakedown { get; }
        public MemoryWatcher<byte> BananaBoost { get; }
        public MemoryWatcher<byte> ShinobiScramble { get; }
        public MemoryWatcher<byte> SeasideScrap { get; }
        public MemoryWatcher<byte> TrickyTraffic { get; }
        public MemoryWatcher<byte> StudioScurry { get; }
        public MemoryWatcher<byte> GraffitiGroove { get; }
        public MemoryWatcher<byte> ShakingSkies { get; }
        public MemoryWatcher<byte> NeonKnockout { get; }
        public MemoryWatcher<byte> PiratePlunder { get; }
        public MemoryWatcher<byte> AdderAssault { get; }
        public MemoryWatcher<byte> DreamyDrive { get; }
        public MemoryWatcher<byte> SanctuarySpeedway { get; }
        public MemoryWatcher<byte> KeilsCarnage { get; }
        public MemoryWatcher<byte> CarrierCrisis { get; }
        public MemoryWatcher<byte> SunshineSlide { get; }
        public MemoryWatcher<byte> RogueRings { get; }
        public MemoryWatcher<byte> SeasideSkirmish { get; }
        public MemoryWatcher<byte> ShrineTime { get; }
        public MemoryWatcher<byte> HangarHassle { get; }
        public MemoryWatcher<byte> BootyBoost { get; }
        public MemoryWatcher<byte> RacingRangers { get; }
        public MemoryWatcher<byte> ShinobiShowdown { get; }
        public MemoryWatcher<byte> RuinRun { get; }
        public MemoryWatcher<byte> MonkeyBrawl { get; }
        public MemoryWatcher<byte> CrumblingChaos { get; }
        public MemoryWatcher<byte> HatcherHustle { get; }
        public MemoryWatcher<byte> DeathEggDuel { get; }
        public MemoryWatcher<byte> UndertakerOvertaker { get; }
        public MemoryWatcher<byte> GoldenGauntlet { get; }
        public MemoryWatcher<byte> CarnivalClash { get; }
        public MemoryWatcher<byte> CurienCurves { get; }
        public MemoryWatcher<byte> MoltenMayhem { get; }
        public MemoryWatcher<byte> SpeedingSeasons { get; }
        public MemoryWatcher<byte> BurningBoost { get; }
        public MemoryWatcher<byte> OceanOutrun { get; }
        public MemoryWatcher<byte> BillyBackslide { get; }
        public MemoryWatcher<byte> CarrierCharge { get; }
        public MemoryWatcher<byte> JetSetJaunt { get; }
        public MemoryWatcher<byte> ArcadeAnnihilation { get; }
        public MemoryWatcher<byte> RapidRuins { get; }
        public MemoryWatcher<byte> ZombieZoom { get; }
        public MemoryWatcher<byte> MaracarMadness { get; }
        public MemoryWatcher<byte> NightmareMeander { get; }
        public MemoryWatcher<byte> MaracaMelee { get; }
        public MemoryWatcher<byte> CastleChaos { get; }
        public MemoryWatcher<byte> VolcanoVelocity { get; }
        public MemoryWatcher<byte> RangerRush { get; }
        public MemoryWatcher<byte> TokyoTakeover { get; }
        public MemoryWatcher<byte> FatalFinale { get; }

        // Fake Memory Watchers
        public bool StartTriggered => this.runStart.Current == 1 && this.runStart.Old == 0 && this.runStart2.Current == 1;

        // Internal variables
        public Tracks CurrentTrack => (Tracks)this.TrackID.Current;
        public GameMode GameMode => (GameMode)this.modeSelect.Current;
        public double totalIGT = 0d;
        public double progressIGT = 0d;

        public Watchers()
        {
            game = Process.GetProcessesByName("ASN_App_PcDx9_Final").OrderByDescending(x => x.StartTime).FirstOrDefault(x => !x.HasExited);
            if (game == null) throw new GameNotfoundException();
            var scanner = new SignatureScanner(game, game.MainModuleWow64Safe().BaseAddress, game.MainModuleWow64Safe().ModuleMemorySize);
            IntPtr ptr;

            // Basic checks
            if (game.Is64Bit()) throw new InvalidHookException("Hooked a 64bit process. This game is not supposed to be 64bit!");
            ptr = scanner.Scan(new SigScanTarget("53 6F 6E 69 63 20 26 20 41 6C 6C 2D 53 74 61 72 73 20 52 61 63 69 6E 67 20 54 72 61 6E 73 66 6F 72 6D 65 64"));
            if (ptr == IntPtr.Zero) throw new InvalidHookException("Hooked the wrong process!");

            ptr = scanner.Scan(new SigScanTarget(2,
                "80 3D ???????? 00", // cmp byte ptr [ASN_App_PcDx9_Final.exe+852918],00  <----
                "0F85 ????????",     // jne ASN_App_PcDx9_Final.exe+467479
                "56"));              // push esi
            if (ptr == IntPtr.Zero) throw new MemoryAddressNotfoundException("runStart");
            this.runStart = new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr)));

            ptr = scanner.Scan(new SigScanTarget(4,
                "74 0E",              // je ASN_App_PcDx9_Final.exe+275701
                "83 3D ???????? 00",  // cmp dword ptr [ASN_App_PcDx9_Final.exe+7C73C0],00  <----
                "74 0E"));            // je ASN_App_PcDx9_Final.exe+27570A
            if (ptr == IntPtr.Zero) throw new MemoryAddressNotfoundException("runStart2");
            this.runStart2 = new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr)));

            ptr = scanner.Scan(new SigScanTarget(3,
                "7E 5C",          // jle ASN_App_PcDx9_Final.exe+51223B
                "A1 ????????"));  // mov eax,[ASN_App_PcDx9_Final.exe+A98669]  <----
            if (ptr == IntPtr.Zero) throw new MemoryAddressNotfoundException("endCredits");
            this.endCredits = new MemoryWatcher<bool>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr), 0x8C)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };

            ptr = scanner.Scan(new SigScanTarget(1,
                "A1 ????????",   // mov eax,[ASN_App_PcDx9_Final.exe+856890]  <----
                "83 F8 02",      // cmp eax,02
                "74 16"));       // je ASN_App_PcDx9_Final.exe+4C6A32
            if (ptr == IntPtr.Zero) throw new MemoryAddressNotfoundException("modeSelect");
            this.modeSelect = new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr)));

            ptr = scanner.Scan(new SigScanTarget(1,
                "A1 ????????",      // mov eax,[ASN_App_PcDx9_Final.exe+7CE920]  <----
                "85 C0",            // test eax,eax
                "0F84 8D000000"));  // je ASN_App_PcDx9_Final.exe+47C2FD
            if (ptr == IntPtr.Zero) throw new MemoryAddressNotfoundException("requiredLaps, totalRaceTime");
            this.requiredLaps =  new MemoryWatcher<byte> (new DeepPointer((IntPtr)game.ReadValue<int>(ptr), 0x0, 0xC1B8, 0x4))  { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.totalRaceTime = new MemoryWatcher<float>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr), 0x0, 0xC1B8, 0x28)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };

            ptr = scanner.Scan(new SigScanTarget(4,
                "8B 04 24",      // mov eax,[esp]
                "A3 ????????",   // mov [ASN_App_PcDx9_Final.exe+7CE930],eax  <----
                "83 C4 08"));    // add esp,08
            if (ptr == IntPtr.Zero) throw new MemoryAddressNotfoundException("raceCompleted");
            this.raceCompleted = new MemoryWatcher<bool>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr)));

            ptr = scanner.Scan(new SigScanTarget(4,
                "7C 44",              // jl ASN_App_PcDx9_Final.exe+4D1C5
                "83 3D ???????? 00",  // cmp dword ptr [ASN_App_PcDx9_Final.exe+7CE944],00  <----
                "74 3B"));            // je ASN_App_PcDx9_Final.exe+4D1C5
            if (ptr == IntPtr.Zero) throw new MemoryAddressNotfoundException("raceStatus");
            this.raceStatus = new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr)));

            ptr = scanner.Scan(new SigScanTarget(2,
                "D8 05 ????????",  // fadd dword ptr [ASN_App_PcDx9_Final.exe+7CE980]  <----
                "56"));            // push esi
            if (ptr == IntPtr.Zero) throw new MemoryAddressNotfoundException("IGT");
            this.IGT = new MemoryWatcher<float>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr)));

            ptr = scanner.Scan(new SigScanTarget(5,
                "55",               // push ebp
                "8B E9",            // mov ebp,ecx
                "8B 0D ????????",   // mov ecx,[ASN_App_PcDx9_Final.exe+7C7430]  <----
                "57"));             // push edi
            if (ptr == IntPtr.Zero) throw new MemoryAddressNotfoundException("eventType, TrackID");
            this.eventType = new MemoryWatcher<uint>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr), 0x0))     { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.TrackID =   new MemoryWatcher<uint>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 4, 0x0)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };

            ptr = scanner.Scan(new SigScanTarget(3,
                "8B 2C 85 ????????",   // mov ebp,[eax*4+ASN_App_PcDx9_Final.exe+7D01F8]  <----
                "89 7C 24 20"));       // mov [esp+20],edi
            if (ptr == IntPtr.Zero) throw new MemoryAddressNotfoundException("Stars");
            // Sunshine Coast
            IntPtr basestarsaddress = (IntPtr)game.ReadValue<int>(ptr);
            int[] offsets = new int[] { 0x7C, 0x138, 0x1F4, 0x2B0, 0x36C, 0x428, 0x4E4, 0x5A0, 0x65C, 0x718 };
            this.CoastalCruise =   new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[0])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.StudioScrapes =   new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[1])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.BattleZoneBlast = new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[2])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.DowntownDrift =   new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[3])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.MonkeyMayhem =    new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[4])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.StarrySpeedway =  new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[5])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.RouletteRush =    new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[6])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.CanyonCarnage =   new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[7])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            // Frozen Valley
            basestarsaddress += 4;
            this.SnowballShakedown = new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[0])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.BananaBoost =       new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[1])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.ShinobiScramble =   new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[2])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.SeasideScrap =      new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[3])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.TrickyTraffic =     new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[4])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.StudioScurry =      new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[5])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.GraffitiGroove =    new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[6])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.ShakingSkies =      new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[7])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.NeonKnockout =      new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[8])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.PiratePlunder =     new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[9])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            // Scorching Skies
            basestarsaddress += 4;
            this.AdderAssault =      new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[0])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.DreamyDrive =       new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[1])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.SanctuarySpeedway = new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[2])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.KeilsCarnage =      new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[3])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.CarrierCrisis =     new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[4])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.SunshineSlide =     new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[5])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.RogueRings =        new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[6])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.SeasideSkirmish =   new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[7])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.ShrineTime =        new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[8])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.HangarHassle =      new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[9])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            // Twilight Engine
            basestarsaddress += 4;
            this.BootyBoost =          new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[0])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.RacingRangers =       new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[1])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.ShinobiShowdown =     new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[2])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.RuinRun =             new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[3])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.MonkeyBrawl =         new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[4])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.CrumblingChaos =      new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[5])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.HatcherHustle =       new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[6])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.DeathEggDuel =        new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[7])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.UndertakerOvertaker = new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[8])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.GoldenGauntlet =      new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[9])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            // Moonlight Park
            basestarsaddress += 4;
            this.CarnivalClash =      new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[0])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.CurienCurves =       new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[1])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.MoltenMayhem =       new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[2])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.SpeedingSeasons =    new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[3])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.BurningBoost =       new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[4])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.OceanOutrun =        new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[5])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.BillyBackslide =     new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[6])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.CarrierCharge =      new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[7])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.JetSetJaunt =        new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[8])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.ArcadeAnnihilation = new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[9])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            // Superstar Showdown
            basestarsaddress += 4;
            this.RapidRuins =       new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[0])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.ZombieZoom =       new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[1])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.MaracarMadness =   new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[2])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.NightmareMeander = new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[3])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.MaracaMelee =      new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[4])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.CastleChaos =      new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[5])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.VolcanoVelocity =  new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[6])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.RangerRush =       new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[7])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.TokyoTakeover =    new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[8])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
            this.FatalFinale =      new MemoryWatcher<byte>(new DeepPointer(basestarsaddress, offsets[9])) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };

            this.AddRange(this.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(p => !p.GetIndexParameters().Any()).Select(p => p.GetValue(this, null) as MemoryWatcher).Where(p => p != null));
        }

        public void Update()
        {
            this.UpdateAll(game);
        }
    }
}
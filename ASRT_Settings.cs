using LiveSplit.Model;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.SonicASRT
{
    public partial class Settings : UserControl
    {
        public bool RunStart { get; set; }
        public bool UseIGT { get; set; }

        // All-Cups
        public bool OceanView { get; set; }
        public bool SambaStudios { get; set; }
        public bool CarrierZone { get; set; }
        public bool DragonCanyon { get; set; }
        public bool TempleTrouble { get; set; }
        public bool GalacticParade { get; set; }
        public bool SeasonalShrines { get; set; }
        public bool RoguesLanding { get; set; }
        public bool DreamValley { get; set; }
        public bool ChillyCastle { get; set; }
        public bool GraffitiCity { get; set; }
        public bool SanctuaryFalls { get; set; }
        public bool GraveyardGig { get; set; }
        public bool AddersLair { get; set; }
        public bool BurningDepths { get; set; }
        public bool RaceOfAges { get; set; }
        public bool SunshineTour { get; set; }
        public bool ShibuyaDowntown { get; set; }
        public bool RouletteRoad { get; set; }
        public bool EggHangar { get; set; }
        public bool OutrunBay { get; set; }

        // World Tour
        public bool CoastalCruise { get; set; }
        public bool StudioScrapes { get; set; }
        public bool BattlezoneBlast { get; set; }
        public bool DowntownDrift { get; set; }
        public bool MonkeyMayhem { get; set; }
        public bool StarrySpeedway { get; set; }
        public bool RouletteRush { get; set; }
        public bool CanyonCarnage { get; set; }
        public bool SnowballShakedown { get; set; }
        public bool BananaBoost { get; set; }
        public bool ShinobiScramble { get; set; }
        public bool SeasideScrap { get; set; }
        public bool TrickyTraffic { get; set; }
        public bool StudioScurry { get; set; }
        public bool GraffitiGroove { get; set; }
        public bool ShakingSkies { get; set; }
        public bool NeonKnockout { get; set; }
        public bool PiratePlunder { get; set; }
        public bool AdderAssault { get; set; }
        public bool DreamyDrive { get; set; }
        public bool SanctuarySpeedway { get; set; }
        public bool KeilsCarnage { get; set; }
        public bool CarrierCrisis { get; set; }
        public bool SunshineSlide { get; set; }
        public bool RogueRings { get; set; }
        public bool SeasideSkirmish { get; set; }
        public bool ShrineTime { get; set; }
        public bool HangarHassle { get; set; }
        public bool BootyBoost { get; set; }
        public bool RacingRangers { get; set; }
        public bool ShinobiShowdown { get; set; }
        public bool RuinRun { get; set; }
        public bool MonkeyBrawl { get; set; }
        public bool CrumblingChaos { get; set; }
        public bool HatcherHustle { get; set; }
        public bool DeathEggDuel { get; set; }
        public bool UndertakerOvertaker { get; set; }
        public bool GoldenGauntlet { get; set; }
        public bool CarnivalClash { get; set; }
        public bool CurienCurves { get; set; }
        public bool MoltenMayhem { get; set; }
        public bool SpeedingSeasons { get; set; }
        public bool BurningBoost { get; set; }
        public bool OceanOutrun { get; set; }
        public bool BillyBackslide { get; set; }
        public bool CarrierCharge { get; set; }
        public bool JetSetJaunt { get; set; }
        public bool ArcadeAnnihilation { get; set; }
        public bool RapidRuins { get; set; }
        public bool ZombieZoom { get; set; }
        public bool MaracarMadness { get; set; }
        public bool NightmareMeander { get; set; }
        public bool MaracaMelee { get; set; }
        public bool CastleChaos { get; set; }
        public bool VolcanoVelocity { get; set; }
        public bool RangerRush { get; set; }
        public bool TokyoTakeover { get; set; }
        public bool FatalFinale { get; set; }

        public Settings()
        {
            InitializeComponent();

            // General settings
            chkrunStart.DataBindings.Add("Checked", this, "RunStart", false, DataSourceUpdateMode.OnPropertyChanged);
            chkIGT.DataBindings.Add("Checked", this, "UseIGT", false, DataSourceUpdateMode.OnPropertyChanged);

            // All Cups
            chkOceanView.DataBindings.Add("Checked", this, "OceanView", false, DataSourceUpdateMode.OnPropertyChanged);
            chkSambaStudios.DataBindings.Add("Checked", this, "SambaStudios", false, DataSourceUpdateMode.OnPropertyChanged);
            chkCarrierZone.DataBindings.Add("Checked", this, "CarrierZone", false, DataSourceUpdateMode.OnPropertyChanged);
            chkDragonCanyon.DataBindings.Add("Checked", this, "DragonCanyon", false, DataSourceUpdateMode.OnPropertyChanged);
            chkTempleTrouble.DataBindings.Add("Checked", this, "TempleTrouble", false, DataSourceUpdateMode.OnPropertyChanged);
            chkGalacticParade.DataBindings.Add("Checked", this, "GalacticParade", false, DataSourceUpdateMode.OnPropertyChanged);
            chkSeasonalShrines.DataBindings.Add("Checked", this, "SeasonalShrines", false, DataSourceUpdateMode.OnPropertyChanged);
            chkRoguesLanding.DataBindings.Add("Checked", this, "RoguesLanding", false, DataSourceUpdateMode.OnPropertyChanged);
            chkDreamValley.DataBindings.Add("Checked", this, "DreamValley", false, DataSourceUpdateMode.OnPropertyChanged);
            chkChillyCastle.DataBindings.Add("Checked", this, "ChillyCastle", false, DataSourceUpdateMode.OnPropertyChanged);
            chkGraffitiCity.DataBindings.Add("Checked", this, "GraffitiCity", false, DataSourceUpdateMode.OnPropertyChanged);
            chkSanctuaryFalls.DataBindings.Add("Checked", this, "SanctuaryFalls", false, DataSourceUpdateMode.OnPropertyChanged);
            chkGraveyardGig.DataBindings.Add("Checked", this, "GraveyardGig", false, DataSourceUpdateMode.OnPropertyChanged);
            chkAddersLair.DataBindings.Add("Checked", this, "AddersLair", false, DataSourceUpdateMode.OnPropertyChanged);
            chkBurningDepths.DataBindings.Add("Checked", this, "BurningDepths", false, DataSourceUpdateMode.OnPropertyChanged);
            chkRaceOfAges.DataBindings.Add("Checked", this, "RaceOfAges", false, DataSourceUpdateMode.OnPropertyChanged);
            chkSunshineTour.DataBindings.Add("Checked", this, "SunshineTour", false, DataSourceUpdateMode.OnPropertyChanged);
            chkShibuyaDowntown.DataBindings.Add("Checked", this, "ShibuyaDowntown", false, DataSourceUpdateMode.OnPropertyChanged);
            chkRouletteRoad.DataBindings.Add("Checked", this, "RouletteRoad", false, DataSourceUpdateMode.OnPropertyChanged);
            chkEggHangar.DataBindings.Add("Checked", this, "EggHangar", false, DataSourceUpdateMode.OnPropertyChanged);
            chkOutrunBay.DataBindings.Add("Checked", this, "OutrunBay", false, DataSourceUpdateMode.OnPropertyChanged);
            // Sunshine Coast
            chkCoastalCruise.DataBindings.Add("Checked", this, "CoastalCruise", false, DataSourceUpdateMode.OnPropertyChanged);
            chkStudioScrapes.DataBindings.Add("Checked", this, "StudioScrapes", false, DataSourceUpdateMode.OnPropertyChanged);
            chkBattlezoneBlast.DataBindings.Add("Checked", this, "BattlezoneBlast", false, DataSourceUpdateMode.OnPropertyChanged);
            chkDowntownDrift.DataBindings.Add("Checked", this, "DowntownDrift", false, DataSourceUpdateMode.OnPropertyChanged);
            chkMonkeyMayhem.DataBindings.Add("Checked", this, "MonkeyMayhem", false, DataSourceUpdateMode.OnPropertyChanged);
            chkStarrySpeedway.DataBindings.Add("Checked", this, "StarrySpeedway", false, DataSourceUpdateMode.OnPropertyChanged);
            chkRouletteRush.DataBindings.Add("Checked", this, "RouletteRush", false, DataSourceUpdateMode.OnPropertyChanged);
            chkCanyonCarnage.DataBindings.Add("Checked", this, "CanyonCarnage", false, DataSourceUpdateMode.OnPropertyChanged);
            // Frozen Valley
            chkSnowballShakedown.DataBindings.Add("Checked", this, "SnowballShakedown", false, DataSourceUpdateMode.OnPropertyChanged);
            chkBananaBoost.DataBindings.Add("Checked", this, "BananaBoost", false, DataSourceUpdateMode.OnPropertyChanged);
            chkShinobiScramble.DataBindings.Add("Checked", this, "ShinobiScramble", false, DataSourceUpdateMode.OnPropertyChanged);
            chkSeasideScrap.DataBindings.Add("Checked", this, "SeasideScrap", false, DataSourceUpdateMode.OnPropertyChanged);
            chkTrickyTraffic.DataBindings.Add("Checked", this, "TrickyTraffic", false, DataSourceUpdateMode.OnPropertyChanged);
            chkStudioScurry.DataBindings.Add("Checked", this, "StudioScurry", false, DataSourceUpdateMode.OnPropertyChanged);
            chkGraffitiGroove.DataBindings.Add("Checked", this, "GraffitiGroove", false, DataSourceUpdateMode.OnPropertyChanged);
            chkShakingSkies.DataBindings.Add("Checked", this, "ShakingSkies", false, DataSourceUpdateMode.OnPropertyChanged);
            chkNeonKnockout.DataBindings.Add("Checked", this, "NeonKnockout", false, DataSourceUpdateMode.OnPropertyChanged);
            chkPiratePlunder.DataBindings.Add("Checked", this, "PiratePlunder", false, DataSourceUpdateMode.OnPropertyChanged);
            // Schorching Skies
            chkAdderAssault.DataBindings.Add("Checked", this, "AdderAssault", false, DataSourceUpdateMode.OnPropertyChanged);
            chkDreamyDrive.DataBindings.Add("Checked", this, "DreamyDrive", false, DataSourceUpdateMode.OnPropertyChanged);
            chkSanctuarySpeedway.DataBindings.Add("Checked", this, "SanctuarySpeedway", false, DataSourceUpdateMode.OnPropertyChanged);
            chkKeilsCarnage.DataBindings.Add("Checked", this, "KeilsCarnage", false, DataSourceUpdateMode.OnPropertyChanged);
            chkCarrierCrisis.DataBindings.Add("Checked", this, "CarrierCrisis", false, DataSourceUpdateMode.OnPropertyChanged);
            chkSunshineSlide.DataBindings.Add("Checked", this, "SunshineSlide", false, DataSourceUpdateMode.OnPropertyChanged);
            chkRogueRings.DataBindings.Add("Checked", this, "RogueRings", false, DataSourceUpdateMode.OnPropertyChanged);
            chkSeasideSkirmish.DataBindings.Add("Checked", this, "SeasideSkirmish", false, DataSourceUpdateMode.OnPropertyChanged);
            chkShrineTime.DataBindings.Add("Checked", this, "ShrineTime", false, DataSourceUpdateMode.OnPropertyChanged);
            chkHangarHassle.DataBindings.Add("Checked", this, "HangarHassle", false, DataSourceUpdateMode.OnPropertyChanged);
            // Twilight Engine
            chkBootyBoost.DataBindings.Add("Checked", this, "BootyBoost", false, DataSourceUpdateMode.OnPropertyChanged);
            chkRacingRangers.DataBindings.Add("Checked", this, "RacingRangers", false, DataSourceUpdateMode.OnPropertyChanged);
            chkShinobiShowdown.DataBindings.Add("Checked", this, "ShinobiShowdown", false, DataSourceUpdateMode.OnPropertyChanged);
            chkRuinRun.DataBindings.Add("Checked", this, "RuinRun", false, DataSourceUpdateMode.OnPropertyChanged);
            chkMonkeyBrawl.DataBindings.Add("Checked", this, "MonkeyBrawl", false, DataSourceUpdateMode.OnPropertyChanged);
            chkCrumblingChaos.DataBindings.Add("Checked", this, "CrumblingChaos", false, DataSourceUpdateMode.OnPropertyChanged);
            chkHatcherHustle.DataBindings.Add("Checked", this, "HatcherHustle", false, DataSourceUpdateMode.OnPropertyChanged);
            chkDeathEggDuel.DataBindings.Add("Checked", this, "DeathEggDuel", false, DataSourceUpdateMode.OnPropertyChanged);
            chkUndertakerOvertaker.DataBindings.Add("Checked", this, "UndertakerOvertaker", false, DataSourceUpdateMode.OnPropertyChanged);
            chkGoldenGauntlet.DataBindings.Add("Checked", this, "GoldenGauntlet", false, DataSourceUpdateMode.OnPropertyChanged);
            // Moonlight Park
            chkCarnivalClash.DataBindings.Add("Checked", this, "CarnivalClash", false, DataSourceUpdateMode.OnPropertyChanged);
            chkCurienCurves.DataBindings.Add("Checked", this, "CurienCurves", false, DataSourceUpdateMode.OnPropertyChanged);
            chkMoltenMayhem.DataBindings.Add("Checked", this, "MoltenMayhem", false, DataSourceUpdateMode.OnPropertyChanged);
            chkSpeedingSeasons.DataBindings.Add("Checked", this, "SpeedingSeasons", false, DataSourceUpdateMode.OnPropertyChanged);
            chkBurningBoost.DataBindings.Add("Checked", this, "BurningBoost", false, DataSourceUpdateMode.OnPropertyChanged);
            chkOceanOutrun.DataBindings.Add("Checked", this, "OceanOutrun", false, DataSourceUpdateMode.OnPropertyChanged);
            chkBillyBackslide.DataBindings.Add("Checked", this, "BillyBackslide", false, DataSourceUpdateMode.OnPropertyChanged);
            chkCarrierCharge.DataBindings.Add("Checked", this, "CarrierCharge", false, DataSourceUpdateMode.OnPropertyChanged);
            chkJetSetJaunt.DataBindings.Add("Checked", this, "JetSetJaunt", false, DataSourceUpdateMode.OnPropertyChanged);
            chkArcadeAnnihilation.DataBindings.Add("Checked", this, "ArcadeAnnihilation", false, DataSourceUpdateMode.OnPropertyChanged);
            // Superstar Showdown
            chkRapidRuins.DataBindings.Add("Checked", this, "RapidRuins", false, DataSourceUpdateMode.OnPropertyChanged);
            chkZombieZoom.DataBindings.Add("Checked", this, "ZombieZoom", false, DataSourceUpdateMode.OnPropertyChanged);
            chkMaracarMadness.DataBindings.Add("Checked", this, "MaracarMadness", false, DataSourceUpdateMode.OnPropertyChanged);
            chkNightmareMeander.DataBindings.Add("Checked", this, "NightmareMeander", false, DataSourceUpdateMode.OnPropertyChanged);
            chkMaracaMelee.DataBindings.Add("Checked", this, "MaracaMelee", false, DataSourceUpdateMode.OnPropertyChanged);
            chkCastleChaos.DataBindings.Add("Checked", this, "CastleChaos", false, DataSourceUpdateMode.OnPropertyChanged);
            chkVolcanoVelocity.DataBindings.Add("Checked", this, "VolcanoVelocity", false, DataSourceUpdateMode.OnPropertyChanged);
            chkRangerRush.DataBindings.Add("Checked", this, "RangerRush", false, DataSourceUpdateMode.OnPropertyChanged);
            chkTokyoTakeover.DataBindings.Add("Checked", this, "TokyoTakeover", false, DataSourceUpdateMode.OnPropertyChanged);
            chkFatalFinale.DataBindings.Add("Checked", this, "FatalFinale", false, DataSourceUpdateMode.OnPropertyChanged);

            // Default Values
            RunStart = true;
            UseIGT = true;
            OceanView = SambaStudios = CarrierZone = DragonCanyon = true;
            TempleTrouble = GalacticParade = SeasonalShrines = RoguesLanding = true;
            DreamValley = ChillyCastle = GraffitiCity = SanctuaryFalls = true;
            GraveyardGig = AddersLair = BurningDepths = RaceOfAges = true;
            SunshineTour = ShibuyaDowntown = RouletteRoad = EggHangar = true;
            OutrunBay = true;
            CoastalCruise = StudioScrapes = BattlezoneBlast = DowntownDrift = MonkeyMayhem = StarrySpeedway = RouletteRush = CanyonCarnage = true;
            SnowballShakedown = BananaBoost = ShinobiScramble = SeasideScrap = TrickyTraffic = StudioScurry = GraffitiGroove = ShakingSkies = NeonKnockout = PiratePlunder = true;
            AdderAssault = DreamyDrive = SanctuarySpeedway = KeilsCarnage = CarrierCrisis = SunshineSlide = RogueRings = SeasideSkirmish = ShrineTime = HangarHassle = true;
            BootyBoost = RacingRangers = ShinobiShowdown = RuinRun = MonkeyBrawl = CrumblingChaos = HatcherHustle = DeathEggDuel = UndertakerOvertaker = GoldenGauntlet = true;
            CarnivalClash = CurienCurves = MoltenMayhem = SpeedingSeasons = BurningBoost = OceanOutrun = BillyBackslide = CarrierCharge = JetSetJaunt = ArcadeAnnihilation = true;
            RapidRuins = ZombieZoom = MaracarMadness = NightmareMeander = MaracaMelee = CastleChaos = VolcanoVelocity = RangerRush = TokyoTakeover = FatalFinale = true;
        }

        public XmlNode GetSettings(XmlDocument doc)
        {
            XmlElement settingsNode = doc.CreateElement("settings");
            settingsNode.AppendChild(ToElement(doc, "RunStart", RunStart));
            settingsNode.AppendChild(ToElement(doc, "UseIGT", UseIGT));
            settingsNode.AppendChild(ToElement(doc, "OceanView", OceanView));
            settingsNode.AppendChild(ToElement(doc, "SambaStudios", SambaStudios));
            settingsNode.AppendChild(ToElement(doc, "CarrierZone", CarrierZone));
            settingsNode.AppendChild(ToElement(doc, "DragonCanyon", DragonCanyon));
            settingsNode.AppendChild(ToElement(doc, "TempleTrouble", TempleTrouble));
            settingsNode.AppendChild(ToElement(doc, "GalacticParade", GalacticParade));
            settingsNode.AppendChild(ToElement(doc, "SeasonalShrines", SeasonalShrines));
            settingsNode.AppendChild(ToElement(doc, "RoguesLanding", RoguesLanding));
            settingsNode.AppendChild(ToElement(doc, "DreamValley", DreamValley));
            settingsNode.AppendChild(ToElement(doc, "ChillyCastle", ChillyCastle));
            settingsNode.AppendChild(ToElement(doc, "GraffitiCity", GraffitiCity));
            settingsNode.AppendChild(ToElement(doc, "SanctuaryFalls", SanctuaryFalls));
            settingsNode.AppendChild(ToElement(doc, "GraveyardGig", GraveyardGig));
            settingsNode.AppendChild(ToElement(doc, "AddersLair", AddersLair));
            settingsNode.AppendChild(ToElement(doc, "BurningDepths", BurningDepths));
            settingsNode.AppendChild(ToElement(doc, "RaceOfAges", RaceOfAges));
            settingsNode.AppendChild(ToElement(doc, "SunshineTour", SunshineTour));
            settingsNode.AppendChild(ToElement(doc, "ShibuyaDowntown", ShibuyaDowntown));
            settingsNode.AppendChild(ToElement(doc, "RouletteRoad", RouletteRoad));
            settingsNode.AppendChild(ToElement(doc, "EggHangar", EggHangar));
            settingsNode.AppendChild(ToElement(doc, "OutrunBay", OutrunBay));
            settingsNode.AppendChild(ToElement(doc, "CoastalCruise", CoastalCruise));
            settingsNode.AppendChild(ToElement(doc, "StudioScrapes", StudioScrapes));
            settingsNode.AppendChild(ToElement(doc, "BattlezoneBlast", BattlezoneBlast));
            settingsNode.AppendChild(ToElement(doc, "DowntownDrift", DowntownDrift));
            settingsNode.AppendChild(ToElement(doc, "MonkeyMayhem", MonkeyMayhem));
            settingsNode.AppendChild(ToElement(doc, "StarrySpeedway", StarrySpeedway));
            settingsNode.AppendChild(ToElement(doc, "RouletteRush", RouletteRush));
            settingsNode.AppendChild(ToElement(doc, "CanyonCarnage", CanyonCarnage));
            settingsNode.AppendChild(ToElement(doc, "SnowballShakedown", SnowballShakedown));
            settingsNode.AppendChild(ToElement(doc, "BananaBoost", BananaBoost));
            settingsNode.AppendChild(ToElement(doc, "ShinobiScramble", ShinobiScramble));
            settingsNode.AppendChild(ToElement(doc, "SeasideScrap", SeasideScrap));
            settingsNode.AppendChild(ToElement(doc, "TrickyTraffic", TrickyTraffic));
            settingsNode.AppendChild(ToElement(doc, "StudioScurry", StudioScurry));
            settingsNode.AppendChild(ToElement(doc, "GraffitiGroove", GraffitiGroove));
            settingsNode.AppendChild(ToElement(doc, "ShakingSkies", ShakingSkies));
            settingsNode.AppendChild(ToElement(doc, "NeonKnockout", NeonKnockout));
            settingsNode.AppendChild(ToElement(doc, "PiratePlunder", PiratePlunder));
            settingsNode.AppendChild(ToElement(doc, "AdderAssault", AdderAssault));
            settingsNode.AppendChild(ToElement(doc, "DreamyDrive", DreamyDrive));
            settingsNode.AppendChild(ToElement(doc, "SanctuarySpeedway", SanctuarySpeedway));
            settingsNode.AppendChild(ToElement(doc, "KeilsCarnage", KeilsCarnage));
            settingsNode.AppendChild(ToElement(doc, "CarrierCrisis", CarrierCrisis));
            settingsNode.AppendChild(ToElement(doc, "SunshineSlide", SunshineSlide));
            settingsNode.AppendChild(ToElement(doc, "RogueRings", RogueRings));
            settingsNode.AppendChild(ToElement(doc, "SeasideSkirmish", SeasideSkirmish));
            settingsNode.AppendChild(ToElement(doc, "ShrineTime", ShrineTime));
            settingsNode.AppendChild(ToElement(doc, "HangarHassle", HangarHassle));
            settingsNode.AppendChild(ToElement(doc, "BootyBoost", BootyBoost));
            settingsNode.AppendChild(ToElement(doc, "RacingRangers", RacingRangers));
            settingsNode.AppendChild(ToElement(doc, "ShinobiShowdown", ShinobiShowdown));
            settingsNode.AppendChild(ToElement(doc, "RuinRun", RuinRun));
            settingsNode.AppendChild(ToElement(doc, "MonkeyBrawl", MonkeyBrawl));
            settingsNode.AppendChild(ToElement(doc, "CrumblingChaos", CrumblingChaos));
            settingsNode.AppendChild(ToElement(doc, "HatcherHustle", HatcherHustle));
            settingsNode.AppendChild(ToElement(doc, "DeathEggDuel", DeathEggDuel));
            settingsNode.AppendChild(ToElement(doc, "UndertakerOvertaker", UndertakerOvertaker));
            settingsNode.AppendChild(ToElement(doc, "GoldenGauntlet", GoldenGauntlet));
            settingsNode.AppendChild(ToElement(doc, "CarnivalClash", CarnivalClash));
            settingsNode.AppendChild(ToElement(doc, "CurienCurves", CurienCurves));
            settingsNode.AppendChild(ToElement(doc, "MoltenMayhem", MoltenMayhem));
            settingsNode.AppendChild(ToElement(doc, "SpeedingSeasons", SpeedingSeasons));
            settingsNode.AppendChild(ToElement(doc, "BurningBoost", BurningBoost));
            settingsNode.AppendChild(ToElement(doc, "OceanOutrun", OceanOutrun));
            settingsNode.AppendChild(ToElement(doc, "BillyBackslide", BillyBackslide));
            settingsNode.AppendChild(ToElement(doc, "CarrierCharge", CarrierCharge));
            settingsNode.AppendChild(ToElement(doc, "JetSetJaunt", JetSetJaunt));
            settingsNode.AppendChild(ToElement(doc, "ArcadeAnnihilation", ArcadeAnnihilation));
            settingsNode.AppendChild(ToElement(doc, "RapidRuins", RapidRuins));
            settingsNode.AppendChild(ToElement(doc, "ZombieZoom", ZombieZoom));
            settingsNode.AppendChild(ToElement(doc, "MaracarMadness", MaracarMadness));
            settingsNode.AppendChild(ToElement(doc, "NightmareMeander", NightmareMeander));
            settingsNode.AppendChild(ToElement(doc, "MaracaMelee", MaracaMelee));
            settingsNode.AppendChild(ToElement(doc, "CastleChaos", CastleChaos));
            settingsNode.AppendChild(ToElement(doc, "VolcanoVelocity", VolcanoVelocity));
            settingsNode.AppendChild(ToElement(doc, "RangerRush", RangerRush));
            settingsNode.AppendChild(ToElement(doc, "TokyoTakeover", TokyoTakeover));
            settingsNode.AppendChild(ToElement(doc, "FatalFinale", FatalFinale));
            return settingsNode;
        }

        public void SetSettings(XmlNode settings)
        {
            RunStart = ParseBool(settings, "RunStart", true);
            UseIGT = ParseBool(settings, "UseIGT", true);
            OceanView = ParseBool(settings, "OceanView", true);
            SambaStudios = ParseBool(settings, "SambaStudios", true);
            CarrierZone = ParseBool(settings, "CarrierZone", true);
            DragonCanyon = ParseBool(settings, "DragonCanyon", true);
            TempleTrouble = ParseBool(settings, "TempleTrouble", true);
            GalacticParade = ParseBool(settings, "GalacticParade", true);
            SeasonalShrines = ParseBool(settings, "SeasonalShrines", true);
            RoguesLanding = ParseBool(settings, "RoguesLanding", true);
            DreamValley = ParseBool(settings, "DreamValley", true);
            ChillyCastle = ParseBool(settings, "ChillyCastle", true);
            GraffitiCity = ParseBool(settings, "GraffitiCity", true);
            SanctuaryFalls = ParseBool(settings, "SanctuaryFalls", true);
            GraveyardGig = ParseBool(settings, "GraveyardGig", true);
            AddersLair = ParseBool(settings, "AddersLair", true);
            BurningDepths = ParseBool(settings, "BurningDepths", true);
            RaceOfAges = ParseBool(settings, "RaceOfAges", true);
            SunshineTour = ParseBool(settings, "SunshineTour", true);
            ShibuyaDowntown = ParseBool(settings, "ShibuyaDowntown", true);
            RouletteRoad = ParseBool(settings, "RouletteRoad", true);
            EggHangar = ParseBool(settings, "EggHangar", true);
            OutrunBay = ParseBool(settings, "OutrunBay", true);
            CoastalCruise = ParseBool(settings, "CoastalCruise", true);
            StudioScrapes = ParseBool(settings, "StudioScrapes", true);
            BattlezoneBlast = ParseBool(settings, "BattlezoneBlast", true);
            DowntownDrift = ParseBool(settings, "DowntownDrift", true);
            MonkeyMayhem = ParseBool(settings, "MonkeyMayhem", true);
            StarrySpeedway = ParseBool(settings, "StarrySpeedway", true);
            RouletteRush = ParseBool(settings, "RouletteRush", true);
            CanyonCarnage = ParseBool(settings, "CanyonCarnage", true);
            SnowballShakedown = ParseBool(settings, "SnowballShakedown", true);
            BananaBoost = ParseBool(settings, "BananaBoost", true);
            ShinobiScramble = ParseBool(settings, "ShinobiScramble", true);
            SeasideScrap = ParseBool(settings, "SeasideScrap", true);
            TrickyTraffic = ParseBool(settings, "TrickyTraffic", true);
            StudioScurry = ParseBool(settings, "StudioScurry", true);
            GraffitiGroove = ParseBool(settings, "GraffitiGroove", true);
            ShakingSkies = ParseBool(settings, "ShakingSkies", true);
            NeonKnockout = ParseBool(settings, "NeonKnockout", true);
            PiratePlunder = ParseBool(settings, "PiratePlunder", true);
            AdderAssault = ParseBool(settings, "AdderAssault", true);
            DreamyDrive = ParseBool(settings, "DreamyDrive", true);
            SanctuarySpeedway = ParseBool(settings, "SanctuarySpeedway", true);
            KeilsCarnage = ParseBool(settings, "KeilsCarnage", true);
            CarrierCrisis = ParseBool(settings, "CarrierCrisis", true);
            SunshineSlide = ParseBool(settings, "SunshineSlide", true);
            RogueRings = ParseBool(settings, "RogueRings", true);
            SeasideSkirmish = ParseBool(settings, "SeasideSkirmish", true);
            ShrineTime = ParseBool(settings, "ShrineTime", true);
            HangarHassle = ParseBool(settings, "HangarHassle", true);
            BootyBoost = ParseBool(settings, "BootyBoost", true);
            RacingRangers = ParseBool(settings, "RacingRangers", true);
            ShinobiShowdown = ParseBool(settings, "ShinobiShowdown", true);
            RuinRun = ParseBool(settings, "RuinRun", true);
            MonkeyBrawl = ParseBool(settings, "MonkeyBrawl", true);
            CrumblingChaos = ParseBool(settings, "CrumblingChaos", true);
            HatcherHustle = ParseBool(settings, "HatcherHustle", true);
            DeathEggDuel = ParseBool(settings, "DeathEggDuel", true);
            UndertakerOvertaker = ParseBool(settings, "UndertakerOvertaker", true);
            GoldenGauntlet = ParseBool(settings, "GoldenGauntlet", true);
            CarnivalClash = ParseBool(settings, "CarnivalClash", true);
            CurienCurves = ParseBool(settings, "CurienCurves", true);
            MoltenMayhem = ParseBool(settings, "MoltenMayhem", true);
            SpeedingSeasons = ParseBool(settings, "SpeedingSeasons", true);
            BurningBoost = ParseBool(settings, "BurningBoost", true);
            OceanOutrun = ParseBool(settings, "OceanOutrun", true);
            BillyBackslide = ParseBool(settings, "BillyBackslide", true);
            CarrierCharge = ParseBool(settings, "CarrierCharge", true);
            JetSetJaunt = ParseBool(settings, "JetSetJaunt", true);
            ArcadeAnnihilation = ParseBool(settings, "ArcadeAnnihilation", true);
            RapidRuins = ParseBool(settings, "RapidRuins", true);
            ZombieZoom = ParseBool(settings, "ZombieZoom", true);
            MaracarMadness = ParseBool(settings, "MaracarMadness", true);
            NightmareMeander = ParseBool(settings, "NightmareMeander", true);
            MaracaMelee = ParseBool(settings, "MaracaMelee", true);
            CastleChaos = ParseBool(settings, "CastleChaos", true);
            VolcanoVelocity = ParseBool(settings, "VolcanoVelocity", true);
            RangerRush = ParseBool(settings, "RangerRush", true);
            TokyoTakeover = ParseBool(settings, "TokyoTakeover", true);
            FatalFinale = ParseBool(settings, "FatalFinale", true);
        }

        static bool ParseBool(XmlNode settings, string setting, bool default_ = false)
        {
            bool val;
            return settings[setting] != null ? (Boolean.TryParse(settings[setting].InnerText, out val) ? val : default_) : default_;
        }

        static XmlElement ToElement<T>(XmlDocument document, string name, T value)
        {
            XmlElement str = document.CreateElement(name);
            str.InnerText = value.ToString();
            return str;
        }
    }
}

using System;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class RenderConfig : IDeserializable, ISerializable
{
    public enum PathConfigEnum
    {
        NONE,
        CUSTOM,
        LEFT,
        RIGHT,
        TOP,
        BOTTOM,
    }

    // it is the responsibility of the renderer to use these
    public TimeSpan Time { get; set; }
    public required double RenderSpeed { get; set; }


    //Which ScoringType to use
    public required ScoringTypeEnum ScoringType { get; set; }
    //Which theme to use
    public required ThemeEnum Theme { get; set; }
    //What keys to render in Tutorial1
    public required HotkeyEnum Hotkey { get; set; }
    //Whether backgrounds are animated.
    public required bool AnimatedBackgrounds { get; set; }
    //Whether to act as if -noskulls is on
    public required bool NoSkulls { get; set; }

    //Disables PickedPlatform display
    public required bool ShowPickedPlatform { get; set; }
    //In platform king, which of the platforms is the red one
    public required int PickedPlatform { get; set; }

    //Scores of team Blue and team Red
    //used for TeamScoreboard
    public required int BlueScore { get; set; }
    public required int RedScore { get; set; }

    //whether to show the brawldown ropes
    public required bool ShowRingRopes { get; set; }
    //whether to show the zombie spawn points
    public required bool ShowZombieSpawns { get; set; }

    //whether to show the bombsketball tagets
    public required bool ShowBombsketballTargets { get; set; }
    //whether to use the bombsketball-specific size for TeamScoreboard digits
    public required bool UseBombsketballDigitSize { get; set; }
    //whether to show each bombsketball bomb timer
    public required bool[] ShowBombsketballBombTimers { get; set; }
    //the time in frames spent on each bombsketball bomb timer
    public required double[] BombsketballBombTimerFrames { get; set; }

    //whether to show the horde doors
    public required bool ShowHordeDoors { get; set; }
    //how many hits each door took. if array is too short, rest of doors will have 0
    public required int[] DamageHordeDoors { get; set; }
    //what horde path type (if any) to show
    public required PathConfigEnum HordePathType { get; set; }
    //the path index
    public required int HordePathIndex { get; set; }
    //the horde wave
    public required int HordeWave { get; set; }
    //the horde random seed
    public required uint HordeRandomSeed { get; set; }

    //whether to show the camera bounds
    public required bool ShowCameraBounds { get; set; }
    //wether to show the kill bounds
    public required bool ShowKillBounds { get; set; }
    //whether to show the sidekick bounds
    public required bool ShowSpawnBotBounds { get; set; }
    //whether to show assets
    public required bool ShowAssets { get; set; }
    //whether to show the background
    public required bool ShowBackground { get; set; }
    //whether to show collision
    public required bool ShowCollision { get; set; }
    //whether to show collision normal overrides
    public required bool ShowCollisionNormalOverride { get; set; }
    //whether to show fire offset of pressure plates
    public required bool ShowFireOffsetLocation { get; set; }
    public required bool ShowFireOffsetLine { get; set; }
    public required bool ShowFireDirection { get; set; }
    //whether to show goals
    public required bool ShowGoal { get; set; }
    //whether to show no dodge zones
    public required bool ShowNoDodgeZone { get; set; }
    //whether to show volumes
    public required bool ShowVolume { get; set; }
    //whether to show respawns
    public required bool ShowRespawn { get; set; }
    //whether to show item spawns
    public required bool ShowItemSpawn { get; set; }
    //whether to show navnodes
    public required bool ShowNavNode { get; set; }

    //circle radius to use for respawns
    public required double RadiusRespawn { get; set; }
    //circle radius to use for zombie spawns
    public required double RadiusZombieSpawn { get; set; }
    //circle radius to use for navnodes
    public required double RadiusNavNode { get; set; }
    //size of points in the horde path
    public required double RadiusHordePathPoint { get; set; }
    //size of radius to indicate pressure plate fire offset
    public required double RadiusFireOffsetLocation { get; set; }

    //length of collision normals
    public required double LengthCollisionNormal { get; set; }
    //when drawing pressure plate fire direction, length of the direction arrow
    public required double LengthFireDirectionArrow { get; set; }

    //when drawing navline arrows, sideways offset of arrow sides
    public required double OffsetNavLineArrowSide { get; set; }
    //when drawing navline arrows, backwards offset of arrow sides
    public required double OffsetNavLineArrowBack { get; set; }
    //when drawing horde path arrows, sideways offset of arrow sides
    public required double OffsetHordePathArrowSide { get; set; }
    //when drawing horde path arrows, backwards offset of arrow sides
    public required double OffsetHordePathArrowBack { get; set; }
    //when drawing pressure plate fire direction, sideways offset of arrow sides
    public required double OffsetFireDirectionArrowSide { get; set; }
    //when drawing pressure plate fire direction, backwards offset of arrow sides
    public required double OffsetFireDirectionArrowBack { get; set; }

    //colors
    public required Color ColorCameraBounds { get; set; }
    public required Color ColorKillBounds { get; set; }
    public required Color ColorSpawnBotBounds { get; set; }

    public required Color ColorHardCollision { get; set; }
    public required Color ColorSoftCollision { get; set; }
    public required Color ColorNoSlideCollision { get; set; }
    public required Color ColorGameModeHardCollision { get; set; }
    public required Color ColorBouncyHardCollision { get; set; }
    public required Color ColorBouncySoftCollision { get; set; }
    public required Color ColorBouncyNoSlideCollision { get; set; }
    public required Color ColorTriggerCollision { get; set; }
    public required Color ColorStickyCollision { get; set; }
    public required Color ColorItemIgnoreCollision { get; set; }
    public required Color ColorPressurePlateCollision { get; set; }
    public required Color ColorSoftPressurePlateCollision { get; set; }
    public required Color ColorLavaCollision { get; set; }

    public required Color ColorCollisionNormal { get; set; }
    public required Color ColorFireOffset { get; set; }
    public required Color ColorFireOffsetLine { get; set; }
    public required Color ColorFireDirection { get; set; }

    public required Color ColorRespawn { get; set; }
    public required Color ColorInitialRespawn { get; set; }
    public required Color ColorExpandedInitRespawn { get; set; }

    public required Color ColorItemSpawn { get; set; }
    public required Color ColorItemInitSpawn { get; set; }
    public required Color ColorItemSet { get; set; }
    public required Color ColorTeamItemInitSpawn { get; set; }

    //1 indexed
    public required Color[] ColorCollisionTeam { get; set; }
    //0 indexed. team 0 is rarely used.
    public required Color[] ColorVolumeTeam { get; set; }

    public required Color ColorZombieSpawns { get; set; }

    public required Color ColorNavNode { get; set; }
    public required Color ColorNavNodeW { get; set; }
    public required Color ColorNavNodeL { get; set; }
    public required Color ColorNavNodeA { get; set; }
    public required Color ColorNavNodeG { get; set; }
    public required Color ColorNavNodeT { get; set; }
    public required Color ColorNavNodeS { get; set; }

    public required Color ColorNavPath { get; set; }

    public required Color ColorHordePath { get; set; }

    public void Deserialize(XElement e)
    {
        RenderConfig @default = Default;
        RenderSpeed = Utils.ParseFloatOrNull(e.GetElementValue(nameof(RenderSpeed))) ?? @default.RenderSpeed;
        ScoringType = Utils.ParseEnumOrNull<ScoringTypeEnum>(e.GetElementValue(nameof(ScoringType))) ?? @default.ScoringType;
        Theme = Utils.ParseEnumOrNull<ThemeEnum>(e.GetElementValue(nameof(Theme))) ?? @default.Theme;
        Hotkey = Utils.ParseEnumOrNull<HotkeyEnum>(e.GetElementValue(nameof(Hotkey))) ?? @default.Hotkey;
        AnimatedBackgrounds = Utils.ParseBoolOrNull(e.GetElementValue(nameof(AnimatedBackgrounds))) ?? @default.AnimatedBackgrounds;
        NoSkulls = Utils.ParseBoolOrNull(e.GetElementValue(nameof(NoSkulls))) ?? @default.NoSkulls;
        ShowPickedPlatform = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowPickedPlatform))) ?? @default.ShowPickedPlatform;
        PickedPlatform = Utils.ParseIntOrNull(e.GetElementValue(nameof(PickedPlatform))) ?? @default.PickedPlatform;
        BlueScore = Utils.ParseIntOrNull(e.GetElementValue(nameof(BlueScore))) ?? @default.BlueScore;
        RedScore = Utils.ParseIntOrNull(e.GetElementValue(nameof(RedScore))) ?? @default.RedScore;
        ShowRingRopes = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowRingRopes))) ?? @default.ShowRingRopes;
        ShowZombieSpawns = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowZombieSpawns))) ?? @default.ShowZombieSpawns;
        ShowBombsketballTargets = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowBombsketballTargets))) ?? @default.ShowBombsketballTargets;
        UseBombsketballDigitSize = Utils.ParseBoolOrNull(e.GetElementValue(nameof(UseBombsketballDigitSize))) ?? @default.UseBombsketballDigitSize;
        ShowBombsketballBombTimers =
        [
            Utils.ParseBoolOrNull(e.Element(nameof(ShowBombsketballBombTimers))?.GetElementValue("Index0")) ?? @default.ShowBombsketballBombTimers[0],
            Utils.ParseBoolOrNull(e.Element(nameof(ShowBombsketballBombTimers))?.GetElementValue("Index1")) ?? @default.ShowBombsketballBombTimers[1],
            Utils.ParseBoolOrNull(e.Element(nameof(ShowBombsketballBombTimers))?.GetElementValue("Index2")) ?? @default.ShowBombsketballBombTimers[2],
        ];
        BombsketballBombTimerFrames =
        [
            Utils.ParseFloatOrNull(e.Element(nameof(BombsketballBombTimerFrames))?.GetElementValue("Index0")) ?? @default.BombsketballBombTimerFrames[0],
            Utils.ParseFloatOrNull(e.Element(nameof(BombsketballBombTimerFrames))?.GetElementValue("Index1")) ?? @default.BombsketballBombTimerFrames[1],
            Utils.ParseFloatOrNull(e.Element(nameof(BombsketballBombTimerFrames))?.GetElementValue("Index2")) ?? @default.BombsketballBombTimerFrames[2],
        ];
        ShowHordeDoors = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowHordeDoors))) ?? @default.ShowHordeDoors;
        DamageHordeDoors =
        [
            Utils.ParseIntOrNull(e.Element(nameof(DamageHordeDoors))?.GetElementValue("Index0")) ?? @default.DamageHordeDoors[0],
            Utils.ParseIntOrNull(e.Element(nameof(DamageHordeDoors))?.GetElementValue("Index1")) ?? @default.DamageHordeDoors[1],
        ];
        HordePathType = Utils.ParseEnumOrNull<PathConfigEnum>(e.GetElementValue(nameof(HordePathType))) ?? @default.HordePathType;
        HordePathIndex = Utils.ParseIntOrNull(e.GetElementValue(nameof(HordePathIndex))) ?? @default.HordePathIndex;
        HordeWave = Utils.ParseIntOrNull(e.GetElementValue(nameof(HordeWave))) ?? @default.HordeWave;
        HordeRandomSeed = Utils.ParseUIntOrNull(e.GetElementValue(nameof(HordeRandomSeed))) ?? @default.HordeRandomSeed;
        ShowCameraBounds = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowCameraBounds))) ?? @default.ShowCameraBounds;
        ShowKillBounds = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowKillBounds))) ?? @default.ShowKillBounds;
        ShowSpawnBotBounds = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowSpawnBotBounds))) ?? @default.ShowSpawnBotBounds;
        ShowAssets = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowAssets))) ?? @default.ShowAssets;
        ShowBackground = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowBackground))) ?? @default.ShowBackground;
        ShowCollision = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowCollision))) ?? @default.ShowCollision;
        ShowCollisionNormalOverride = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowCollisionNormalOverride))) ?? @default.ShowCollisionNormalOverride;
        ShowFireOffsetLocation = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowFireOffsetLocation))) ?? @default.ShowFireOffsetLocation;
        ShowFireOffsetLine = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowFireOffsetLine))) ?? @default.ShowFireOffsetLine;
        ShowFireDirection = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowFireDirection))) ?? @default.ShowFireDirection;
        ShowGoal = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowGoal))) ?? @default.ShowGoal;
        ShowNoDodgeZone = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowNoDodgeZone))) ?? @default.ShowNoDodgeZone;
        ShowVolume = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowVolume))) ?? @default.ShowVolume;
        ShowRespawn = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowRespawn))) ?? @default.ShowRespawn;
        ShowItemSpawn = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowItemSpawn))) ?? @default.ShowItemSpawn;
        ShowNavNode = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowNavNode))) ?? @default.ShowNavNode;
        RadiusRespawn = Utils.ParseFloatOrNull(e.GetElementValue(nameof(RadiusRespawn))) ?? @default.RadiusRespawn;
        RadiusZombieSpawn = Utils.ParseFloatOrNull(e.GetElementValue(nameof(RadiusZombieSpawn))) ?? @default.RadiusZombieSpawn;
        RadiusNavNode = Utils.ParseFloatOrNull(e.GetElementValue(nameof(RadiusNavNode))) ?? @default.RadiusNavNode;
        RadiusHordePathPoint = Utils.ParseFloatOrNull(e.GetElementValue(nameof(RadiusHordePathPoint))) ?? @default.RadiusHordePathPoint;
        RadiusFireOffsetLocation = Utils.ParseFloatOrNull(e.GetElementValue(nameof(RadiusFireOffsetLocation))) ?? @default.RadiusFireOffsetLocation;
        LengthCollisionNormal = Utils.ParseFloatOrNull(e.GetElementValue(nameof(LengthCollisionNormal))) ?? @default.LengthCollisionNormal;
        LengthFireDirectionArrow = Utils.ParseFloatOrNull(e.GetElementValue(nameof(LengthFireDirectionArrow))) ?? @default.LengthFireDirectionArrow;
        OffsetNavLineArrowSide = Utils.ParseFloatOrNull(e.GetElementValue(nameof(OffsetNavLineArrowSide))) ?? @default.OffsetNavLineArrowSide;
        OffsetNavLineArrowBack = Utils.ParseFloatOrNull(e.GetElementValue(nameof(OffsetNavLineArrowBack))) ?? @default.OffsetNavLineArrowBack;
        OffsetHordePathArrowSide = Utils.ParseFloatOrNull(e.GetElementValue(nameof(OffsetHordePathArrowSide))) ?? @default.OffsetHordePathArrowSide;
        OffsetHordePathArrowBack = Utils.ParseFloatOrNull(e.GetElementValue(nameof(OffsetHordePathArrowBack))) ?? @default.OffsetHordePathArrowBack;
        OffsetFireDirectionArrowSide = Utils.ParseFloatOrNull(e.GetElementValue(nameof(OffsetFireDirectionArrowSide))) ?? @default.OffsetFireDirectionArrowSide;
        OffsetFireDirectionArrowBack = Utils.ParseFloatOrNull(e.GetElementValue(nameof(OffsetFireDirectionArrowBack))) ?? @default.OffsetFireDirectionArrowBack;
        ColorCameraBounds = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorCameraBounds))) ?? @default.ColorCameraBounds.ToHex());
        ColorKillBounds = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorKillBounds))) ?? @default.ColorKillBounds.ToHex());
        ColorSpawnBotBounds = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorSpawnBotBounds))) ?? @default.ColorSpawnBotBounds.ToHex());
        ColorHardCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorHardCollision))) ?? @default.ColorHardCollision.ToHex());
        ColorSoftCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorSoftCollision))) ?? @default.ColorSoftCollision.ToHex());
        ColorNoSlideCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorNoSlideCollision))) ?? @default.ColorNoSlideCollision.ToHex());
        ColorGameModeHardCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorGameModeHardCollision))) ?? @default.ColorGameModeHardCollision.ToHex());
        ColorBouncyHardCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorBouncyHardCollision))) ?? @default.ColorBouncyHardCollision.ToHex());
        ColorBouncySoftCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorBouncySoftCollision))) ?? @default.ColorBouncySoftCollision.ToHex());
        ColorBouncyNoSlideCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorBouncyNoSlideCollision))) ?? @default.ColorBouncyNoSlideCollision.ToHex());
        ColorTriggerCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorTriggerCollision))) ?? @default.ColorTriggerCollision.ToHex());
        ColorStickyCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorStickyCollision))) ?? @default.ColorStickyCollision.ToHex());
        ColorItemIgnoreCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorItemIgnoreCollision))) ?? @default.ColorItemIgnoreCollision.ToHex());
        ColorPressurePlateCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorPressurePlateCollision))) ?? @default.ColorPressurePlateCollision.ToHex());
        ColorSoftPressurePlateCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorSoftPressurePlateCollision))) ?? @default.ColorSoftPressurePlateCollision.ToHex());
        ColorLavaCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorLavaCollision))) ?? @default.ColorLavaCollision.ToHex());
        ColorCollisionNormal = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorCollisionNormal))) ?? @default.ColorCollisionNormal.ToHex());
        ColorFireOffset = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorFireOffset))) ?? @default.ColorFireOffset.ToHex());
        ColorFireOffsetLine = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorFireOffsetLine))) ?? @default.ColorFireOffsetLine.ToHex());
        ColorFireDirection = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorFireDirection))) ?? @default.ColorFireDirection.ToHex());
        ColorRespawn = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorRespawn))) ?? @default.ColorRespawn.ToHex());
        ColorInitialRespawn = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorInitialRespawn))) ?? @default.ColorInitialRespawn.ToHex());
        ColorExpandedInitRespawn = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorExpandedInitRespawn))) ?? @default.ColorExpandedInitRespawn.ToHex());
        ColorItemSpawn = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorItemSpawn))) ?? @default.ColorItemSpawn.ToHex());
        ColorItemInitSpawn = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorItemInitSpawn))) ?? @default.ColorItemInitSpawn.ToHex());
        ColorItemSet = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorItemSet))) ?? @default.ColorItemSet.ToHex());
        ColorTeamItemInitSpawn = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorTeamItemInitSpawn))) ?? @default.ColorTeamItemInitSpawn.ToHex());
        ColorCollisionTeam =
        [
            Color.FromHex(Utils.ParseUIntOrNull(e.Element(nameof(ColorCollisionTeam))?.GetElementValue("Index1")) ?? @default.ColorCollisionTeam[0].ToHex()),
            Color.FromHex(Utils.ParseUIntOrNull(e.Element(nameof(ColorCollisionTeam))?.GetElementValue("Index2")) ?? @default.ColorCollisionTeam[1].ToHex()),
            Color.FromHex(Utils.ParseUIntOrNull(e.Element(nameof(ColorCollisionTeam))?.GetElementValue("Index3")) ?? @default.ColorCollisionTeam[2].ToHex()),
            Color.FromHex(Utils.ParseUIntOrNull(e.Element(nameof(ColorCollisionTeam))?.GetElementValue("Index4")) ?? @default.ColorCollisionTeam[3].ToHex()),
            Color.FromHex(Utils.ParseUIntOrNull(e.Element(nameof(ColorCollisionTeam))?.GetElementValue("Index5")) ?? @default.ColorCollisionTeam[4].ToHex()),
        ];
        ColorVolumeTeam =
        [
            Color.FromHex(Utils.ParseUIntOrNull(e.Element(nameof(ColorVolumeTeam))?.GetElementValue("Index0")) ?? @default.ColorVolumeTeam[0].ToHex()),
            Color.FromHex(Utils.ParseUIntOrNull(e.Element(nameof(ColorVolumeTeam))?.GetElementValue("Index1")) ?? @default.ColorVolumeTeam[1].ToHex()),
            Color.FromHex(Utils.ParseUIntOrNull(e.Element(nameof(ColorVolumeTeam))?.GetElementValue("Index2")) ?? @default.ColorVolumeTeam[2].ToHex()),
            Color.FromHex(Utils.ParseUIntOrNull(e.Element(nameof(ColorVolumeTeam))?.GetElementValue("Index3")) ?? @default.ColorVolumeTeam[3].ToHex()),
            Color.FromHex(Utils.ParseUIntOrNull(e.Element(nameof(ColorVolumeTeam))?.GetElementValue("Index4")) ?? @default.ColorVolumeTeam[4].ToHex()),
            Color.FromHex(Utils.ParseUIntOrNull(e.Element(nameof(ColorVolumeTeam))?.GetElementValue("Index5")) ?? @default.ColorVolumeTeam[5].ToHex()),
        ];
        ColorZombieSpawns = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorZombieSpawns))) ?? @default.ColorZombieSpawns.ToHex());
        ColorNavNode = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorNavNode))) ?? @default.ColorNavNode.ToHex());
        ColorNavNodeW = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorNavNodeW))) ?? @default.ColorNavNodeW.ToHex());
        ColorNavNodeL = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorNavNodeL))) ?? @default.ColorNavNodeL.ToHex());
        ColorNavNodeA = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorNavNodeA))) ?? @default.ColorNavNodeA.ToHex());
        ColorNavNodeG = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorNavNodeG))) ?? @default.ColorNavNodeG.ToHex());
        ColorNavNodeT = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorNavNodeT))) ?? @default.ColorNavNodeT.ToHex());
        ColorNavNodeS = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorNavNodeS))) ?? @default.ColorNavNodeS.ToHex());
        ColorNavPath = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorNavPath))) ?? @default.ColorNavPath.ToHex());
        ColorHordePath = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorHordePath))) ?? @default.ColorHordePath.ToHex());
    }

    public void Serialize(XElement e)
    {
        e.AddChild(nameof(RenderSpeed), RenderSpeed);
        e.AddChild(nameof(ScoringType), ScoringType);
        e.AddChild(nameof(Theme), Theme);
        e.AddChild(nameof(Hotkey), Hotkey);
        e.AddChild(nameof(AnimatedBackgrounds), AnimatedBackgrounds);
        e.AddChild(nameof(NoSkulls), NoSkulls);
        e.AddChild(nameof(ShowPickedPlatform), ShowPickedPlatform);
        e.AddChild(nameof(PickedPlatform), PickedPlatform);
        e.AddChild(nameof(BlueScore), BlueScore);
        e.AddChild(nameof(RedScore), RedScore);
        e.AddChild(nameof(ShowRingRopes), ShowRingRopes);
        e.AddChild(nameof(ShowZombieSpawns), ShowZombieSpawns);
        e.AddChild(nameof(ShowBombsketballTargets), ShowBombsketballTargets);
        e.AddChild(nameof(UseBombsketballDigitSize), UseBombsketballDigitSize);
        e.AddChild(nameof(ShowBombsketballBombTimers), ShowBombsketballBombTimers);
        e.AddChild(nameof(ShowHordeDoors), ShowHordeDoors);
        XElement damageHordeDoorsElement = new(nameof(DamageHordeDoors));
        damageHordeDoorsElement.AddChild("Index0", DamageHordeDoors[0]);
        damageHordeDoorsElement.AddChild("Index1", DamageHordeDoors[1]);
        e.Add(damageHordeDoorsElement);
        e.AddChild(nameof(HordePathType), HordePathType);
        e.AddChild(nameof(HordePathIndex), HordePathIndex);
        e.AddChild(nameof(HordeWave), HordeWave);
        e.AddChild(nameof(HordeRandomSeed), HordeRandomSeed);
        e.AddChild(nameof(ShowCameraBounds), ShowCameraBounds);
        e.AddChild(nameof(ShowKillBounds), ShowKillBounds);
        e.AddChild(nameof(ShowSpawnBotBounds), ShowSpawnBotBounds);
        e.AddChild(nameof(ShowAssets), ShowAssets);
        e.AddChild(nameof(ShowBackground), ShowBackground);
        e.AddChild(nameof(ShowCollision), ShowCollision);
        e.AddChild(nameof(ShowCollisionNormalOverride), ShowCollisionNormalOverride);
        e.AddChild(nameof(ShowFireOffsetLocation), ShowFireOffsetLocation);
        e.AddChild(nameof(ShowFireOffsetLine), ShowFireOffsetLine);
        e.AddChild(nameof(ShowFireDirection), ShowFireDirection);
        e.AddChild(nameof(ShowGoal), ShowGoal);
        e.AddChild(nameof(ShowNoDodgeZone), ShowNoDodgeZone);
        e.AddChild(nameof(ShowVolume), ShowVolume);
        e.AddChild(nameof(ShowRespawn), ShowRespawn);
        e.AddChild(nameof(ShowItemSpawn), ShowItemSpawn);
        e.AddChild(nameof(ShowNavNode), ShowNavNode);
        e.AddChild(nameof(RadiusRespawn), RadiusRespawn);
        e.AddChild(nameof(RadiusZombieSpawn), RadiusZombieSpawn);
        e.AddChild(nameof(RadiusNavNode), RadiusNavNode);
        e.AddChild(nameof(RadiusHordePathPoint), RadiusHordePathPoint);
        e.AddChild(nameof(RadiusFireOffsetLocation), RadiusFireOffsetLocation);
        e.AddChild(nameof(LengthCollisionNormal), LengthCollisionNormal);
        e.AddChild(nameof(LengthFireDirectionArrow), LengthFireDirectionArrow);
        e.AddChild(nameof(OffsetNavLineArrowSide), OffsetNavLineArrowSide);
        e.AddChild(nameof(OffsetNavLineArrowBack), OffsetNavLineArrowBack);
        e.AddChild(nameof(OffsetHordePathArrowSide), OffsetHordePathArrowSide);
        e.AddChild(nameof(OffsetHordePathArrowBack), OffsetHordePathArrowBack);
        e.AddChild(nameof(OffsetFireDirectionArrowSide), OffsetFireDirectionArrowSide);
        e.AddChild(nameof(OffsetFireDirectionArrowBack), OffsetFireDirectionArrowBack);
        e.AddChild(nameof(ColorCameraBounds), "0x" + ColorCameraBounds.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorKillBounds), "0x" + ColorKillBounds.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorSpawnBotBounds), "0x" + ColorSpawnBotBounds.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorHardCollision), "0x" + ColorHardCollision.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorSoftCollision), "0x" + ColorSoftCollision.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorNoSlideCollision), "0x" + ColorNoSlideCollision.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorGameModeHardCollision), "0x" + ColorGameModeHardCollision.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorBouncyHardCollision), "0x" + ColorBouncyHardCollision.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorBouncySoftCollision), "0x" + ColorBouncySoftCollision.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorBouncyNoSlideCollision), "0x" + ColorBouncyNoSlideCollision.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorTriggerCollision), "0x" + ColorTriggerCollision.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorStickyCollision), "0x" + ColorStickyCollision.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorItemIgnoreCollision), "0x" + ColorItemIgnoreCollision.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorPressurePlateCollision), "0x" + ColorPressurePlateCollision.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorSoftPressurePlateCollision), "0x" + ColorSoftPressurePlateCollision.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorLavaCollision), "0x" + ColorLavaCollision.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorCollisionNormal), "0x" + ColorCollisionNormal.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorFireOffset), "0x" + ColorFireOffset.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorFireOffsetLine), "0x" + ColorFireOffsetLine.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorFireDirection), "0x" + ColorFireDirection.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorRespawn), "0x" + ColorRespawn.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorInitialRespawn), "0x" + ColorInitialRespawn.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorExpandedInitRespawn), "0x" + ColorExpandedInitRespawn.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorItemSpawn), "0x" + ColorItemSpawn.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorItemInitSpawn), "0x" + ColorItemInitSpawn.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorItemSet), "0x" + ColorItemSet.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorTeamItemInitSpawn), "0x" + ColorTeamItemInitSpawn.ToHex().ToString("X8"));
        XElement colorCollisionTeamElement = new(nameof(ColorCollisionTeam));
        colorCollisionTeamElement.AddChild("Index1", "0x" + ColorCollisionTeam[0].ToHex().ToString("X8"));
        colorCollisionTeamElement.AddChild("Index2", "0x" + ColorCollisionTeam[1].ToHex().ToString("X8"));
        colorCollisionTeamElement.AddChild("Index3", "0x" + ColorCollisionTeam[2].ToHex().ToString("X8"));
        colorCollisionTeamElement.AddChild("Index4", "0x" + ColorCollisionTeam[3].ToHex().ToString("X8"));
        colorCollisionTeamElement.AddChild("Index5", "0x" + ColorCollisionTeam[4].ToHex().ToString("X8"));
        e.Add(colorCollisionTeamElement);
        XElement colorVolumeTeamElement = new(nameof(ColorVolumeTeam));
        colorVolumeTeamElement.AddChild("Index0", "0x" + ColorVolumeTeam[0].ToHex().ToString("X8"));
        colorVolumeTeamElement.AddChild("Index1", "0x" + ColorVolumeTeam[1].ToHex().ToString("X8"));
        colorVolumeTeamElement.AddChild("Index2", "0x" + ColorVolumeTeam[2].ToHex().ToString("X8"));
        colorVolumeTeamElement.AddChild("Index3", "0x" + ColorVolumeTeam[3].ToHex().ToString("X8"));
        colorVolumeTeamElement.AddChild("Index4", "0x" + ColorVolumeTeam[4].ToHex().ToString("X8"));
        colorVolumeTeamElement.AddChild("Index5", "0x" + ColorVolumeTeam[5].ToHex().ToString("X8"));
        e.Add(colorVolumeTeamElement);
        e.AddChild(nameof(ColorZombieSpawns), "0x" + ColorZombieSpawns.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorNavNode), "0x" + ColorNavNode.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorNavNodeW), "0x" + ColorNavNodeW.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorNavNodeL), "0x" + ColorNavNodeL.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorNavNodeA), "0x" + ColorNavNodeA.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorNavNodeG), "0x" + ColorNavNodeG.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorNavNodeT), "0x" + ColorNavNodeT.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorNavNodeS), "0x" + ColorNavNodeS.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorNavPath), "0x" + ColorNavPath.ToHex().ToString("X8"));
        e.AddChild(nameof(ColorHordePath), "0x" + ColorHordePath.ToHex().ToString("X8"));
    }

    public static RenderConfig Default => new()
    {
        RenderSpeed = 1,
        ScoringType = ScoringTypeEnum.None,
        Theme = ThemeEnum.None,
        Hotkey = HotkeyEnum.Keyboard,
        AnimatedBackgrounds = false,
        NoSkulls = false,
        ShowPickedPlatform = false,
        PickedPlatform = 0,
        BlueScore = 0,
        RedScore = 0,
        ShowRingRopes = false,
        ShowZombieSpawns = false,
        ShowBombsketballTargets = false,
        UseBombsketballDigitSize = false,
        ShowBombsketballBombTimers = [false, false, false],
        BombsketballBombTimerFrames = [0, 0, 0],
        ShowHordeDoors = false,
        DamageHordeDoors = [0, 0],
        HordePathType = PathConfigEnum.NONE,
        HordePathIndex = 0,
        HordeWave = 0,
        HordeRandomSeed = 0,
        ShowCameraBounds = true,
        ShowKillBounds = true,
        ShowSpawnBotBounds = false,
        ShowAssets = true,
        ShowBackground = true,
        ShowCollision = true,
        ShowCollisionNormalOverride = true,
        ShowFireOffsetLocation = false,
        ShowFireOffsetLine = false,
        ShowFireDirection = false,
        ShowGoal = true,
        ShowNoDodgeZone = true,
        ShowVolume = true,
        ShowRespawn = true,
        ShowItemSpawn = true,
        ShowNavNode = false,
        RadiusRespawn = 10,
        RadiusZombieSpawn = 20,
        RadiusNavNode = 5,
        RadiusHordePathPoint = 15,
        RadiusFireOffsetLocation = 15,
        LengthCollisionNormal = 50,
        LengthFireDirectionArrow = 50,
        OffsetNavLineArrowSide = 7,
        OffsetNavLineArrowBack = 7,
        OffsetHordePathArrowSide = 18,
        OffsetHordePathArrowBack = 28,
        OffsetFireDirectionArrowSide = 18,
        OffsetFireDirectionArrowBack = 28,
        ColorCameraBounds = Color.FromHex(0xFF00007F),
        ColorKillBounds = Color.FromHex(0x5F00007F),
        ColorSpawnBotBounds = Color.FromHex(0xFFFFCC7F),
        ColorHardCollision = Color.FromHex(0x00FF00FF),
        ColorSoftCollision = Color.FromHex(0xFFFF00FF),
        ColorNoSlideCollision = Color.FromHex(0x00FFFFFF),
        ColorGameModeHardCollision = Color.FromHex(0xCCFFCCFF),
        ColorBouncyHardCollision = Color.FromHex(0x339933FF),
        ColorBouncySoftCollision = Color.FromHex(0x999933FF),
        ColorBouncyNoSlideCollision = Color.FromHex(0x339999FF),
        ColorTriggerCollision = Color.FromHex(0x0000FFFF),
        ColorStickyCollision = Color.FromHex(0x66FF66FF),
        ColorItemIgnoreCollision = Color.FromHex(0xFF00FFFF),
        ColorPressurePlateCollision = Color.FromHex(0xCC6619FF),
        ColorSoftPressurePlateCollision = Color.FromHex(0x7F1900FF),
        ColorLavaCollision = Color.FromHex(0xFF8000FF),
        ColorCollisionNormal = Color.FromHex(0xFFFFFF7F),
        ColorFireOffset = Color.FromHex(0x7000AA70),
        ColorFireOffsetLine = Color.FromHex(0xBB7000D0),
        ColorFireDirection = Color.FromHex(0xCC5000BE),
        ColorRespawn = Color.FromHex(0xFF7F0060),
        ColorInitialRespawn = Color.FromHex(0xFF000060),
        ColorExpandedInitRespawn = Color.FromHex(0xFF00FF60),
        ColorItemSpawn = Color.FromHex(0x007FFF60),
        ColorItemInitSpawn = Color.FromHex(0x7F007F60),
        ColorItemSet = Color.FromHex(0x007F7F60),
        ColorTeamItemInitSpawn = Color.FromHex(0x7F7F0060),
        ColorCollisionTeam =
        [
            Color.FromHex(0xFF00007F),
            Color.FromHex(0x0000FF7F),
            Color.FromHex(0x00FF007F),
            Color.FromHex(0xFFFF007F),
            Color.FromHex(0xFF00FF7F)
        ],
        ColorVolumeTeam =
        [
            Color.FromHex(0xFFA50033),
            Color.FromHex(0xFF000033),
            Color.FromHex(0x0000FF33),
            Color.FromHex(0x00FF0033),
            Color.FromHex(0xFFFF0033),
            Color.FromHex(0xFF00FF33)
        ],
        ColorZombieSpawns = Color.FromHex(0xFFFFFF7F),
        ColorNavNode = Color.FromHex(0x7F7F7F90),
        ColorNavNodeW = Color.FromHex(0x7F700F90),
        ColorNavNodeL = Color.FromHex(0x7F007F90),
        ColorNavNodeA = Color.FromHex(0x007F7F90),
        ColorNavNodeG = Color.FromHex(0x007F0090),
        ColorNavNodeT = Color.FromHex(0x00007F90),
        ColorNavNodeS = Color.FromHex(0x7F000090),
        ColorNavPath = Color.FromHex(0x8060607F),
        ColorHordePath = Color.FromHex(0x93529960),
    };
}
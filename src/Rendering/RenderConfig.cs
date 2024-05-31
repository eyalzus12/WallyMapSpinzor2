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
    public double RenderSpeed { get; set; } = 1;


    //Which ScoringType to use
    public ScoringTypeEnum ScoringType { get; set; } = ScoringTypeEnum.None;
    //Which theme to use
    public ThemeEnum Theme { get; set; } = ThemeEnum.None;
    //What keys to render in Tutorial1
    public HotkeyEnum Hotkey { get; set; } = HotkeyEnum.Keyboard;
    //Whether backgrounds are animated.
    public bool AnimatedBackgrounds { get; set; } = false;
    //Whether to act as if -noskulls is on
    public bool NoSkulls { get; set; } = false;

    //Disables PickedPlatform display
    public bool ShowPickedPlatform { get; set; } = false;
    //In platform king, which of the platforms is the red one
    public int PickedPlatform { get; set; } = 0;

    //Scores of team Blue and team Red
    //used for TeamScoreboard
    public int BlueScore { get; set; } = 0;
    public int RedScore { get; set; } = 0;

    //whether to show the brawldown ropes
    public bool ShowRingRopes { get; set; } = false;
    //whether to show the zombie spawn points
    public bool ShowZombieSpawns { get; set; } = false;
    //whether to show the bombsketball tagets
    public bool ShowBombsketballTargets { get; set; } = false;
    //whether to use the bombsketball-specific size for TeamScoreboard digits
    public bool UseBombsketballDigitSize { get; set; } = false;

    //whether to show the horde doors
    public bool ShowHordeDoors { get; set; } = false;
    //how many hits each door took. if array is too short, rest of doors will have 0
    public int[] DamageHordeDoors { get; set; } = [0, 0];
    //what horde path type (if any) to show
    public PathConfigEnum HordePathType { get; set; } = PathConfigEnum.NONE;
    //the path index
    public int HordePathIndex { get; set; } = 0;
    //the horde wave
    public int HordeWave { get; set; } = 0;
    //the horde random seed
    public uint HordeRandomSeed { get; set; } = 0;

    //whether to show the camera bounds
    public bool ShowCameraBounds { get; set; } = true;
    //wether to show the kill bounds
    public bool ShowKillBounds { get; set; } = true;
    //whether to show the sidekick bounds
    public bool ShowSpawnBotBounds { get; set; } = false;
    //whether to show assets
    public bool ShowAssets { get; set; } = true;
    //whether to show the background
    public bool ShowBackground { get; set; } = true;
    //whether to show collision
    public bool ShowCollision { get; set; } = true;
    //whether to show collision normal overrides
    public bool ShowCollisionNormalOverride { get; set; } = true;
    //whether to show fire offset of pressure plates
    public bool ShowFireOffsetLocation { get; set; } = false;
    public bool ShowFireOffsetLine { get; set; } = false;
    public bool ShowFireDirection { get; set; } = false;
    //whether to show goals
    public bool ShowGoal { get; set; } = true;
    //whether to show no dodge zones
    public bool ShowNoDodgeZone { get; set; } = true;
    //whether to show volumes
    public bool ShowVolume { get; set; } = true;
    //whether to show respawns
    public bool ShowRespawn { get; set; } = true;
    //whether to show item spawns
    public bool ShowItemSpawn { get; set; } = true;
    //whether to show navnodes
    public bool ShowNavNode { get; set; } = false;

    //circle radius to use for respawns
    public double RadiusRespawn { get; set; } = 10;
    //circle radius to use for zombie spawns
    public double RadiusZombieSpawn { get; set; } = 20;
    //circle radius to use for navnodes
    public double RadiusNavNode { get; set; } = 5;
    //size of points in the horde path
    public double RadiusHordePathPoint { get; set; } = 15;
    //size of radius to indicate pressure plate fire offset
    public double RadiusFireOffsetLocation { get; set; } = 15;

    //length of collision normals
    public double LengthCollisionNormal { get; set; } = 50;
    //when drawing pressure plate fire direction, length of the direction arrow
    public double LengthFireDirectionArrow { get; set; } = 50;

    //when drawing navline arrows, sideways offset of arrow sides
    public double OffsetNavLineArrowSide { get; set; } = 7;
    //when drawing navline arrows, backwards offset of arrow sides
    public double OffsetNavLineArrowBack { get; set; } = 7;
    //when drawing horde path arrows, sideways offset of arrow sides
    public double OffsetHordePathArrowSide { get; set; } = 18;
    //when drawing horde path arrows, backwards offset of arrow sides
    public double OffsetHordePathArrowBack { get; set; } = 28;
    //when drawing pressure plate fire direction, sideways offset of arrow sides
    public double OffsetFireDirectionArrowSide { get; set; } = 18;
    //when drawing pressure plate fire direction, backwards offset of arrow sides
    public double OffsetFireDirectionArrowBack { get; set; } = 28;

    //colors
    public Color ColorCameraBounds { get; set; } = Color.FromHex(0xFF00007F);
    public Color ColorKillBounds { get; set; } = Color.FromHex(0x5F00007F);
    public Color ColorSpawnBotBounds { get; set; } = Color.FromHex(0xFFFFCC7F);

    public Color ColorHardCollision { get; set; } = Color.FromHex(0x00FF00FF);
    public Color ColorSoftCollision { get; set; } = Color.FromHex(0xFFFF00FF);
    public Color ColorNoSlideCollision { get; set; } = Color.FromHex(0x00FFFFFF);
    public Color ColorGameModeHardCollision { get; set; } = Color.FromHex(0xCCFFCCFF);
    public Color ColorBouncyHardCollision { get; set; } = Color.FromHex(0x339933FF);
    public Color ColorBouncySoftCollision { get; set; } = Color.FromHex(0x999933FF);
    public Color ColorBouncyNoSlideCollision { get; set; } = Color.FromHex(0x339999FF);
    public Color ColorTriggerCollision { get; set; } = Color.FromHex(0x0000FFFF);
    public Color ColorStickyCollision { get; set; } = Color.FromHex(0x66FF66FF);
    public Color ColorItemIgnoreCollision { get; set; } = Color.FromHex(0xFF00FFFF);
    public Color ColorPressurePlateCollision { get; set; } = Color.FromHex(0xCC6619FF);
    public Color ColorSoftPressurePlateCollision { get; set; } = Color.FromHex(0x7F1900FF);
    public Color ColorLavaCollision { get; set; } = Color.FromHex(0xFF8000FF);

    public Color ColorCollisionNormal { get; set; } = Color.FromHex(0xFFFFFF7F);
    public Color ColorFireOffset { get; set; } = Color.FromHex(0x7000AA70);
    public Color ColorFireOffsetLine { get; set; } = Color.FromHex(0xBB7000D0);
    public Color ColorFireDirection { get; set; } = Color.FromHex(0xCC5000BE);

    public Color ColorRespawn { get; set; } = Color.FromHex(0xFF7F0060);
    public Color ColorInitialRespawn { get; set; } = Color.FromHex(0xFF000060);
    public Color ColorExpandedInitRespawn { get; set; } = Color.FromHex(0xFF00FF60);

    public Color ColorItemSpawn { get; set; } = Color.FromHex(0x007FFF60);
    public Color ColorItemInitSpawn { get; set; } = Color.FromHex(0x7F007F60);
    public Color ColorItemSet { get; set; } = Color.FromHex(0x007F7F60);
    public Color ColorTeamItemInitSpawn { get; set; } = Color.FromHex(0x7F7F0060);

    //1 indexed
    public Color[] ColorCollisionTeam { get; set; } =
    [
        Color.FromHex(0xFF00007F), //1
        Color.FromHex(0x0000FF7F), //2
        Color.FromHex(0x00FF007F), //3
        Color.FromHex(0xFFFF007F), //4
        Color.FromHex(0xFF00FF7F)  //5
    ];

    //0 indexed. team 0 is rarely used.
    public Color[] ColorVolumeTeam { get; set; } =
    [
        Color.FromHex(0xFFA50033), //0
        Color.FromHex(0xFF000033), //1
        Color.FromHex(0x0000FF33), //2
        Color.FromHex(0x00FF0033), //3
        Color.FromHex(0xFFFF0033), //4
        Color.FromHex(0xFF00FF33)  //5
    ];

    public Color ColorZombieSpawns { get; set; } = Color.FromHex(0xFFFFFF7F);

    public Color ColorNavNode { get; set; } = Color.FromHex(0x7F7F7F90);
    public Color ColorNavNodeW { get; set; } = Color.FromHex(0x7F700F90);
    public Color ColorNavNodeL { get; set; } = Color.FromHex(0x7F007F90);
    public Color ColorNavNodeA { get; set; } = Color.FromHex(0x007F7F90);
    public Color ColorNavNodeG { get; set; } = Color.FromHex(0x007F0090);
    public Color ColorNavNodeT { get; set; } = Color.FromHex(0x00007F90);
    public Color ColorNavNodeS { get; set; } = Color.FromHex(0x7F000090);

    public Color ColorNavPath { get; set; } = Color.FromHex(0x8060607F);

    public Color ColorHordePath { get; set; } = Color.FromHex(0x93529960);

    public void Deserialize(XElement e)
    {
        // these need updating if defaults change
        // TODO: define a const default for each prop so there's no duplication
        RenderSpeed = Utils.ParseFloatOrNull(e.GetElementValue(nameof(RenderSpeed))) ?? 1;
        ScoringType = Utils.ParseEnumOrNull<ScoringTypeEnum>(e.GetElementValue(nameof(ScoringType))) ?? ScoringTypeEnum.None;
        Theme = Utils.ParseEnumOrNull<ThemeEnum>(e.GetElementValue(nameof(Theme))) ?? ThemeEnum.None;
        Hotkey = Utils.ParseEnumOrNull<HotkeyEnum>(e.GetElementValue(nameof(Hotkey))) ?? HotkeyEnum.Keyboard;
        AnimatedBackgrounds = Utils.ParseBoolOrNull(e.GetElementValue(nameof(AnimatedBackgrounds))) ?? false;
        NoSkulls = Utils.ParseBoolOrNull(e.GetElementValue(nameof(NoSkulls))) ?? false;
        ShowPickedPlatform = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowPickedPlatform))) ?? false;
        PickedPlatform = Utils.ParseIntOrNull(e.GetElementValue(nameof(PickedPlatform))) ?? 0;
        BlueScore = Utils.ParseIntOrNull(e.GetElementValue(nameof(BlueScore))) ?? 0;
        RedScore = Utils.ParseIntOrNull(e.GetElementValue(nameof(RedScore))) ?? 0;
        ShowRingRopes = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowRingRopes))) ?? false;
        ShowZombieSpawns = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowZombieSpawns))) ?? false;
        ShowBombsketballTargets = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowBombsketballTargets))) ?? false;
        UseBombsketballDigitSize = Utils.ParseBoolOrNull(e.GetElementValue(nameof(UseBombsketballDigitSize))) ?? false;
        ShowHordeDoors = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowHordeDoors))) ?? false;
        DamageHordeDoors =
        [
            Utils.ParseIntOrNull(e.Element(nameof(DamageHordeDoors))?.GetElementValue("Index0")) ?? 0,
            Utils.ParseIntOrNull(e.Element(nameof(DamageHordeDoors))?.GetElementValue("Index1")) ?? 0,
        ];
        HordePathType = Utils.ParseEnumOrNull<PathConfigEnum>(e.GetElementValue(nameof(HordePathType))) ?? PathConfigEnum.NONE;
        HordePathIndex = Utils.ParseIntOrNull(e.GetElementValue(nameof(HordePathIndex))) ?? 0;
        HordeWave = Utils.ParseIntOrNull(e.GetElementValue(nameof(HordeWave))) ?? 0;
        HordeRandomSeed = Utils.ParseUIntOrNull(e.GetElementValue(nameof(HordeRandomSeed))) ?? 0;
        ShowCameraBounds = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowCameraBounds))) ?? true;
        ShowKillBounds = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowKillBounds))) ?? true;
        ShowSpawnBotBounds = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowSpawnBotBounds))) ?? false;
        ShowAssets = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowAssets))) ?? true;
        ShowBackground = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowBackground))) ?? true;
        ShowCollision = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowCollision))) ?? true;
        ShowCollisionNormalOverride = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowCollisionNormalOverride))) ?? true;
        ShowFireOffsetLocation = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowFireOffsetLocation))) ?? false;
        ShowFireOffsetLine = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowFireOffsetLine))) ?? false;
        ShowFireDirection = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowFireDirection))) ?? false;
        ShowGoal = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowGoal))) ?? true;
        ShowNoDodgeZone = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowNoDodgeZone))) ?? true;
        ShowVolume = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowVolume))) ?? true;
        ShowRespawn = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowRespawn))) ?? true;
        ShowItemSpawn = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowItemSpawn))) ?? true;
        ShowNavNode = Utils.ParseBoolOrNull(e.GetElementValue(nameof(ShowNavNode))) ?? false;
        RadiusRespawn = Utils.ParseFloatOrNull(e.GetElementValue(nameof(RadiusRespawn))) ?? 10;
        RadiusZombieSpawn = Utils.ParseFloatOrNull(e.GetElementValue(nameof(RadiusZombieSpawn))) ?? 20;
        RadiusNavNode = Utils.ParseFloatOrNull(e.GetElementValue(nameof(RadiusNavNode))) ?? 5;
        RadiusHordePathPoint = Utils.ParseFloatOrNull(e.GetElementValue(nameof(RadiusHordePathPoint))) ?? 15;
        RadiusFireOffsetLocation = Utils.ParseFloatOrNull(e.GetElementValue(nameof(RadiusFireOffsetLocation))) ?? 15;
        LengthCollisionNormal = Utils.ParseFloatOrNull(e.GetElementValue(nameof(LengthCollisionNormal))) ?? 50;
        LengthFireDirectionArrow = Utils.ParseFloatOrNull(e.GetElementValue(nameof(LengthFireDirectionArrow))) ?? 50;
        OffsetNavLineArrowSide = Utils.ParseFloatOrNull(e.GetElementValue(nameof(OffsetNavLineArrowSide))) ?? 7;
        OffsetNavLineArrowBack = Utils.ParseFloatOrNull(e.GetElementValue(nameof(OffsetNavLineArrowBack))) ?? 7;
        OffsetHordePathArrowSide = Utils.ParseFloatOrNull(e.GetElementValue(nameof(OffsetHordePathArrowSide))) ?? 18;
        OffsetHordePathArrowBack = Utils.ParseFloatOrNull(e.GetElementValue(nameof(OffsetHordePathArrowBack))) ?? 28;
        OffsetFireDirectionArrowSide = Utils.ParseFloatOrNull(e.GetElementValue(nameof(OffsetFireDirectionArrowSide))) ?? 18;
        OffsetFireDirectionArrowBack = Utils.ParseFloatOrNull(e.GetElementValue(nameof(OffsetFireDirectionArrowBack))) ?? 28;
        ColorCameraBounds = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorCameraBounds))) ?? 0xFF00007F);
        ColorKillBounds = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorKillBounds))) ?? 0x5F00007F);
        ColorSpawnBotBounds = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorSpawnBotBounds))) ?? 0xFFFFCC7F);
        ColorHardCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorHardCollision))) ?? 0x00FF00FF);
        ColorSoftCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorSoftCollision))) ?? 0xFFFF00FF);
        ColorNoSlideCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorNoSlideCollision))) ?? 0x00FFFFFF);
        ColorGameModeHardCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorGameModeHardCollision))) ?? 0xCCFFCCFF);
        ColorBouncyHardCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorBouncyHardCollision))) ?? 0x339933FF);
        ColorBouncySoftCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorBouncySoftCollision))) ?? 0x999933FF);
        ColorBouncyNoSlideCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorBouncyNoSlideCollision))) ?? 0x339999FF);
        ColorTriggerCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorTriggerCollision))) ?? 0x0000FFFF);
        ColorStickyCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorStickyCollision))) ?? 0x66FF66FF);
        ColorItemIgnoreCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorItemIgnoreCollision))) ?? 0xFF00FFFF);
        ColorPressurePlateCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorPressurePlateCollision))) ?? 0xCC6619FF);
        ColorSoftPressurePlateCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorSoftPressurePlateCollision))) ?? 0x7F1900FF);
        ColorLavaCollision = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorLavaCollision))) ?? 0xFF8000FF);
        ColorCollisionNormal = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorCollisionNormal))) ?? 0xFFFFFF7F);
        ColorFireOffset = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorFireOffset))) ?? 0x7000AA70);
        ColorFireOffsetLine = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorFireOffsetLine))) ?? 0xBB7000D0);
        ColorFireDirection = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorFireDirection))) ?? 0xCC5000BE);
        ColorRespawn = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorRespawn))) ?? 0xFF7F0060);
        ColorInitialRespawn = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorInitialRespawn))) ?? 0xFF000060);
        ColorExpandedInitRespawn = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorExpandedInitRespawn))) ?? 0xFF00FF60);
        ColorItemSpawn = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorItemSpawn))) ?? 0x007FFF60);
        ColorItemInitSpawn = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorItemInitSpawn))) ?? 0x7F007F60);
        ColorItemSet = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorItemSet))) ?? 0x007F7F60);
        ColorTeamItemInitSpawn = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorTeamItemInitSpawn))) ?? 0x7F7F0060);
        ColorCollisionTeam =
        [
            Color.FromHex(Utils.ParseUIntOrNull(e.Element(nameof(ColorCollisionTeam))?.GetElementValue("Index1")) ?? 0xFF00007F),
            Color.FromHex(Utils.ParseUIntOrNull(e.Element(nameof(ColorCollisionTeam))?.GetElementValue("Index2")) ?? 0x0000FF7F),
            Color.FromHex(Utils.ParseUIntOrNull(e.Element(nameof(ColorCollisionTeam))?.GetElementValue("Index3")) ?? 0x00FF007F),
            Color.FromHex(Utils.ParseUIntOrNull(e.Element(nameof(ColorCollisionTeam))?.GetElementValue("Index4")) ?? 0xFFFF007F),
            Color.FromHex(Utils.ParseUIntOrNull(e.Element(nameof(ColorCollisionTeam))?.GetElementValue("Index5")) ?? 0xFF00FF7F),
        ];
        ColorVolumeTeam =
        [
            Color.FromHex(Utils.ParseUIntOrNull(e.Element(nameof(ColorVolumeTeam))?.GetElementValue("Index0")) ?? 0xFFA50033),
            Color.FromHex(Utils.ParseUIntOrNull(e.Element(nameof(ColorVolumeTeam))?.GetElementValue("Index1")) ?? 0xFF000033),
            Color.FromHex(Utils.ParseUIntOrNull(e.Element(nameof(ColorVolumeTeam))?.GetElementValue("Index2")) ?? 0x0000FF33),
            Color.FromHex(Utils.ParseUIntOrNull(e.Element(nameof(ColorVolumeTeam))?.GetElementValue("Index3")) ?? 0x00FF0033),
            Color.FromHex(Utils.ParseUIntOrNull(e.Element(nameof(ColorVolumeTeam))?.GetElementValue("Index4")) ?? 0xFFFF0033),
            Color.FromHex(Utils.ParseUIntOrNull(e.Element(nameof(ColorVolumeTeam))?.GetElementValue("Index5")) ?? 0xFF00FF33),
        ];
        ColorZombieSpawns = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorZombieSpawns))) ?? 0xFFFFFF7F);
        ColorNavNode = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorNavNode))) ?? 0x7F7F7F90);
        ColorNavNodeW = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorNavNodeW))) ?? 0x7F700F90);
        ColorNavNodeL = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorNavNodeL))) ?? 0x7F007F90);
        ColorNavNodeA = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorNavNodeA))) ?? 0x007F7F90);
        ColorNavNodeG = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorNavNodeG))) ?? 0x007F0090);
        ColorNavNodeT = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorNavNodeT))) ?? 0x00007F90);
        ColorNavNodeS = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorNavNodeS))) ?? 0x7F000090);
        ColorNavPath = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorNavPath))) ?? 0x8060607F);
        ColorHordePath = Color.FromHex(Utils.ParseUIntOrNull(e.GetElementValue(nameof(ColorHordePath))) ?? 0x93529960);
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
}
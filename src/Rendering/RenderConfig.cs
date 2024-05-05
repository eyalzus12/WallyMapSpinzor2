namespace WallyMapSpinzor2;

public class RenderConfig
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
}
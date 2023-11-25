namespace WallyMapSpinzor2;

public class RenderSettings
{
    //Which ScoringType to use. Possible values are any scoring type name.
    //Case Sensitive. Empty string to not have a scoring type.
    public string ScoringType{get; set;} = "";
    //Which theme to use. Possible values:
    //Christmas, Halloween, TWDHalloween
    //Keep as empty string to not have a theme.
    public string Theme{get; set;} = "";
    //Whether backgrounds are animated.
    public bool AnimatedBackgrounds{get; set;} = false;
    //What keys to render in Tutorial1. Possible values:
    //Keyboard, PS4, Mouse, Switch, SwitchJoyConSolo, Controller, Mobile
    public string Hotkey{get; set;} = "Keyboard";
    //Whether to act as if -noskulls is on
    public bool NoSkulls{get; set;} = false;
    //In platform king, which of the platforms is the red one
    //Set to -1 to make them all blue
    public int PickedPlatform{get; set;} = -1;
    //Scores of team Blue and team Red
    //used for TeamScoreboard
    public int BlueScore{get; set;} = 0;
    public int RedScore{get; set;} = 0;


    //whether to show the camera bounds
    public bool ShowCameraBounds{get; set;} = true;
    //wether to show the kill bounds
    public bool ShowKillBounds{get; set;} = true;
    //whether to show the sidekick bounds
    public bool ShowSpawnBotBounds{get; set;} = false;

    //whether to show assets
    public bool ShowAssets{get; set;} = true;
    //whether to show the background
    public bool ShowBackground{get; set;} = true;

    //whether to show collision
    public bool ShowCollision{get; set;} = true;
    //whether to show collision normals
    public bool ShowCollisionNormal{get; set;} = false;

    //whether to show goals
    public bool ShowGoal{get; set;} = true;
    //whether to show no dodge zones
    public bool ShowNoDodgeZone{get; set;} = true;
    //whether to show volumes
    public bool ShowVolume{get; set;} = true;

    //whether to show respawns
    public bool ShowRespawn{get; set;} = true;

    //whether to show item spawns
    public bool ShowItemSpawn{get; set;} = true;

    //whether to show navnodes
    public bool ShowNavNode{get; set;} = false;

    //circle radius to use for drawing points
    public double RadiusPointDraw{get; set;} = 10;
    //circle radius to use for respawns
    public double RadiusRespawn{get; set;} = 30;
    //length of collision normals
    public double LengthCollisionNormal{get; set;} = 10;

    //colors
    public Color ColorCameraBounds{get; set;} = Color.FromHex(0xFF00007F);
    public Color ColorKillBounds{get; set;} = Color.FromHex(0x5F00007F);
    public Color ColorSpawnBotBounds{get ;set;} = Color.FromHex(0xFFFFCC7F);

    public Color ColorHardCollision{get; set;} = Color.FromHex(0x00FF00FF);
    public Color ColorSoftCollision{get; set;} = Color.FromHex(0xFFFF00FF);
    public Color ColorNoSlideCollision{get; set;} = Color.FromHex(0x00FFFFFF);
    public Color ColorGameModeHardCollision{get; set;} = Color.FromHex(0xCCFFCCFF);
    public Color ColorBouncyHardCollision{get; set;} = Color.FromHex(0x339933FF);
    public Color ColorBouncySoftCollision{get; set;} = Color.FromHex(0x999933FF);
    public Color ColorBouncyNoSlideCollision{get; set;} = Color.FromHex(0x339999FF);
    public Color ColorTriggerCollision{get; set;} = Color.FromHex(0x0000FFFF);
    public Color ColorStickyCollision{get; set;} = Color.FromHex(0xFF0000FF);
    public Color ColorItemIgnoreCollision{get; set;} = Color.FromHex(0xFF00FFFF);
    public Color ColorPressurePlateCollision{get; set;} = Color.FromHex(0xCC6619FF);
    public Color ColorSoftPressurePlateCollision{get; set;} = Color.FromHex(0x7F1900FF);

    public Color ColorCollisionNormal{get; set;} = Color.FromHex(0xFFFFFF7F);

    public Color ColorRespawn{get; set;} = Color.FromHex(0xFF7F007F);
    public Color ColorInitialRespawn{get; set;} = Color.FromHex(0xFF00007F);
    public Color ColorExpandedInitRespawn{get; set;} = Color.FromHex(0xFF00FF7F);

    public Color ColorItemSpawn{get; set;} = Color.FromHex(0x007FFF7F);
    public Color ColorItemInitSpawn{get; set;} = Color.FromHex(0x7F007F7F);
    public Color ColorItemSet{get; set;} = Color.FromHex(0x007F7F7F);
    public Color ColorTeamItemInitSpawn{get; set;} = Color.FromHex(0x007F007F);

    //1 indexed
    public Color[] ColorCollisionTeam{get; set;} = new[]
    {
        Color.FromHex(0xFF00007F), //1
        Color.FromHex(0x0000FF7F), //2
        Color.FromHex(0x00FF007F), //3
        Color.FromHex(0xFFFF007F), //4
        Color.FromHex(0xFF00FF7F)  //5
    };

    //0 indexed. team 0 is rarely used.
    public Color[] ColorVolumeTeam{get; set;} = new[]
    {
        Color.FromHex(0xFFA50033), //0
        Color.FromHex(0xFF000033), //1
        Color.FromHex(0x0000FF33), //2
        Color.FromHex(0x00FF0033), //3
        Color.FromHex(0xFFFF0033), //4
        Color.FromHex(0xFF00FF33)  //5
    };
}
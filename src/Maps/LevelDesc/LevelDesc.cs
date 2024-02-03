using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelDesc : IDeserializable, ISerializable, IDrawable
{
    private const int LEFT_ROPE_X = 521;
    private const int LEFT_ROPE_Y = 1293;
    private const int RIGHT_ROPE_X = 2934;
    private const int RIGHT_ROPE_Y = 1293;
    private static readonly (int, int)[] ZOMBIE_SPAWNS = new[] { (230, 390), (180, 900), (-1160, 900), (-1990, 390) };

    private const string ROPE_SPRITE = "a_DefaultRopes";
    private const string RED_TARGET_SPRITE = "a_TargetAnchoredRed";
    private const string BLUE_TARGET_SPRITE = "a_TargetAnchoredBlue";
    private const string HORDE_DOOR_UNDAMAGED = "a_ValhallaDoor_000";
    private const string HORDE_DOOR_DAMAGED = "a_ValhallaDoor_025";
    private const string HORDE_DOOR_CRITICAL = "a_ValhallaDoor_050";

    public const string GAMEMODE_BONES = "bones/Bones_GameModes.swf";

    public string AssetDir { get; set; } = null!;
    public string LevelName { get; set; } = null!;
    public int NumFrames { get; set; }
    public double SlowMult { get; set; }

    public CameraBounds CameraBounds { get; set; } = null!;
    public SpawnBotBounds SpawnBotBounds { get; set; } = null!;
    public List<Background> Backgrounds { get; set; } = null!;
    public List<LevelSound> LevelSounds { get; set; } = null!;
    public TeamScoreboard? TeamScoreboard { get; set; }
    public List<AbstractAsset> Assets { get; set; } = null!;
    public List<LevelAnim> LevelAnims { get; set; } = null!;
    public List<AbstractVolume> Volumes { get; set; } = null!;
    public List<AbstractCollision> Collisions { get; set; } = null!;
    public List<DynamicCollision> DynamicCollisions { get; set; } = null!;
    public List<Respawn> Respawns { get; set; } = null!;
    public List<DynamicRespawn> DynamicRespawns { get; set; } = null!;
    public List<AbstractItemSpawn> ItemSpawns { get; set; } = null!;
    public List<DynamicItemSpawn> DynamicItemSpawns { get; set; } = null!;
    public List<NavNode> NavNodes { get; set; } = null!;
    public List<DynamicNavNode> DynamicNavNodes { get; set; } = null!;
    public List<WaveData> WaveDatas { get; set; } = null!;
    public List<AnimatedBackground> AnimatedBackgrounds { get; set; } = null!;

    public virtual void Deserialize(XElement e)
    {
        AssetDir = e.GetAttribute("AssetDir");
        LevelName = e.GetAttribute("LevelName");
        NumFrames = e.GetIntAttribute("NumFrames", 0);
        SlowMult = e.GetFloatAttribute("SlowMult", 1);

        CameraBounds = e.DeserializeChildOfType<CameraBounds>()!;
        SpawnBotBounds = e.DeserializeChildOfType<SpawnBotBounds>()!;
        Backgrounds = e.DeserializeChildrenOfType<Background>();
        LevelSounds = e.DeserializeChildrenOfType<LevelSound>();
        TeamScoreboard = e.DeserializeChildOfType<TeamScoreboard>();
        Assets = e.DeserializeAssetChildren();
        LevelAnims = e.DeserializeChildrenOfType<LevelAnim>();
        Volumes = e.DeserializeVolumeChildren();
        Collisions = e.DeserializeCollisionChildren();
        DynamicCollisions = e.DeserializeChildrenOfType<DynamicCollision>();
        Respawns = e.DeserializeChildrenOfType<Respawn>();
        DynamicRespawns = e.DeserializeChildrenOfType<DynamicRespawn>();
        ItemSpawns = e.DeserializeItemSpawnChildren();
        DynamicItemSpawns = e.DeserializeChildrenOfType<DynamicItemSpawn>();
        WaveDatas = e.DeserializeChildrenOfType<WaveData>();
        AnimatedBackgrounds = e.DeserializeChildrenOfType<AnimatedBackground>();
        NavNodes = e.DeserializeChildrenOfType<NavNode>();
        DynamicNavNodes = e.DeserializeChildrenOfType<DynamicNavNode>();
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("AssetDir", AssetDir);
        e.SetAttributeValue("LevelName", LevelName);
        if (NumFrames != 0)
            e.SetAttributeValue("NumFrames", NumFrames.ToString());
        if (SlowMult != 1)
            e.SetAttributeValue("SlowMult", SlowMult.ToString());

        e.AddSerialized(CameraBounds);
        e.AddSerialized(SpawnBotBounds);
        e.AddManySerialized(Backgrounds);
        e.AddManySerialized(LevelSounds);
        e.AddSerializedIfNotNull(TeamScoreboard);
        e.AddManySerialized(Assets);
        e.AddManySerialized(LevelAnims);
        e.AddManySerialized(Volumes);
        e.AddManySerialized(Collisions);
        e.AddManySerialized(DynamicCollisions);
        e.AddManySerialized(Respawns);
        e.AddManySerialized(DynamicRespawns);
        e.AddManySerialized(ItemSpawns);
        e.AddManySerialized(DynamicItemSpawns);
        e.AddManySerialized(WaveDatas);
        e.AddManySerialized(AnimatedBackgrounds);
        e.AddManySerialized(NavNodes);
        e.AddManySerialized(DynamicNavNodes);
    }


    public void DrawOn<T>(ICanvas<T> canvas, RenderConfig config, Transform trans, TimeSpan time, RenderData data)
        where T : ITexture
    {
        if(trans != Transform.IDENTITY)
            throw new ArgumentException("Initial non-camera transform must be the identity transform");
        // setup
        data.AssetDir = AssetDir;
        data.DefaultNumFrames = NumFrames;
        data.DefaultSlowMult = SlowMult;
        foreach (Background b in Backgrounds)
            b.UpdateBackground(data, config);
        data.PlatIDDynamicOffset.Clear();
        data.PlatIDMovingPlatformOffset.Clear();
        foreach (AbstractAsset a in Assets) if (a is MovingPlatform mp)
                mp.StoreMovingPlatformOffset(data, time);
        foreach (AbstractCollision c in Collisions)
            c.CalculateCurve(0, 0);
        data.NavIDDictionary.Clear();
        foreach (NavNode n in NavNodes)
            n.RegisterNavNode(data);
        foreach (DynamicNavNode dn in DynamicNavNodes)
            dn.RegisterNavNodes(data);

        // drawing
        CameraBounds.DrawOn(canvas, config, trans, time, data);
        SpawnBotBounds.DrawOn(canvas, config, trans, time, data);
        foreach (Background b in Backgrounds)
            b.DrawOn(canvas, config, trans, time, data);
        //foreach(LevelSound ls in LevelSounds)
        TeamScoreboard?.DrawOn(canvas, config, trans, time, data);
        foreach (AbstractAsset a in Assets)
            a.DrawOn(canvas, config, trans, time, data);
        //foreach (LevelAnim la in LevelAnims)
        foreach (AbstractVolume v in Volumes)
            v.DrawOn(canvas, config, trans, time, data);
        foreach (AbstractCollision c in Collisions)
            c.DrawOn(canvas, config, trans, time, data);
        foreach (DynamicCollision dc in DynamicCollisions)
            dc.DrawOn(canvas, config, trans, time, data);
        foreach (Respawn r in Respawns)
            r.DrawOn(canvas, config, trans, time, data);
        foreach (DynamicRespawn dr in DynamicRespawns)
            dr.DrawOn(canvas, config, trans, time, data);
        foreach (AbstractItemSpawn i in ItemSpawns)
            i.DrawOn(canvas, config, trans, time, data);
        foreach (DynamicItemSpawn di in DynamicItemSpawns)
            di.DrawOn(canvas, config, trans, time, data);
        //foreach(WaveData wd in WaveDatas)
        //foreach(AnimatedBackground ab in AnimatedBackgrounds)
        foreach (NavNode n in NavNodes)
            n.DrawOn(canvas, config, trans, time, data);
        foreach (DynamicNavNode dn in DynamicNavNodes)
            dn.DrawOn(canvas, config, trans, time, data);


        //Gamemode stuff
        if (config.ScoringType == Enum.GetName(ScoringTypeEnum.RING))
        {
            if (config.ShowRingRopes)
            {
                T rope = canvas.LoadTextureFromSWF(GAMEMODE_BONES, ROPE_SPRITE);
                canvas.DrawTexture(LEFT_ROPE_X, LEFT_ROPE_Y, rope, trans, DrawPriorityEnum.FOREGROUND);
                canvas.DrawTexture(RIGHT_ROPE_X, RIGHT_ROPE_Y, rope, trans * Transform.CreateScale(-1, 1), DrawPriorityEnum.FOREGROUND);
            }
        }
        if (config.ScoringType == Enum.GetName(ScoringTypeEnum.ZOMBIE))
        {
            if (config.ShowZombieSpawns)
            {
                foreach ((int x, int y) in ZOMBIE_SPAWNS)
                    canvas.DrawCircle(x, y, config.RadiusZombieSpawn, config.ColorZombieSpawns, trans, DrawPriorityEnum.DATA);
            }
        }
        if (config.ScoringType == Enum.GetName(ScoringTypeEnum.BOMBSKETBALL))
        {
            if (config.ShowBombsketballTargets)
            {
                T red = canvas.LoadTextureFromSWF(GAMEMODE_BONES, RED_TARGET_SPRITE);
                T blue = canvas.LoadTextureFromSWF(GAMEMODE_BONES, BLUE_TARGET_SPRITE);
                Goal? goalred = Volumes.OfType<Goal>().Where(g => g.Team == 2).FirstOrDefault();
                Goal? goalblue = Volumes.OfType<Goal>().Where(g => g.Team == 1).FirstOrDefault();
                if (goalred is not null)
                    canvas.DrawTexture((goalred.X + goalred.W) / 2, (goalred.Y + goalred.H) / 2, red, trans, DrawPriorityEnum.FOREGROUND);
                if (goalblue is not null)
                    canvas.DrawTexture((goalblue.X + goalblue.W) / 2, (goalblue.Y + goalblue.H) / 2, blue, trans, DrawPriorityEnum.FOREGROUND);
            }
        }
        if (config.ScoringType == Enum.GetName(ScoringTypeEnum.HORDE))
        {
            if (config.ShowHordeDoors)
            {
                int i = 0;
                foreach (Goal g in Volumes.OfType<Goal>())
                {
                    int hits = (i >= config.DamageHordeDoors.Length) ? 0 : config.DamageHordeDoors[i];

                    if (hits < 24)
                    {
                        T door = hits switch
                        {
                            <= 6 => canvas.LoadTextureFromSWF(GAMEMODE_BONES, HORDE_DOOR_UNDAMAGED),
                            <= 12 => canvas.LoadTextureFromSWF(GAMEMODE_BONES, HORDE_DOOR_DAMAGED),
                            // <=23
                            _ => canvas.LoadTextureFromSWF(GAMEMODE_BONES, HORDE_DOOR_CRITICAL),
                        };

                        canvas.DrawTexture((g.X + g.W) / 2, (g.Y + g.H) / 2, door, trans, DrawPriorityEnum.FOREGROUND);
                    }

                    ++i;
                }
            }
        }
    }
}

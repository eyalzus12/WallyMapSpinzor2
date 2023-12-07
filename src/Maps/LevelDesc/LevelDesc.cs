using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelDesc : IDeserializable, ISerializable, IDrawable
{
    public string AssetDir{get; set;} = null!;
    public string LevelName{get; set;} = null!;
    public int NumFrames{get; set;}
    public double SlowMult{get; set;}

    public CameraBounds CameraBounds{get; set;} = null!;
    public SpawnBotBounds SpawnBotBounds{get; set;} = null!;
    public List<Background> Backgrounds{get; set;} = null!;
    public List<LevelSound> LevelSounds{get; set;} = null!;
    public TeamScoreboard? TeamScoreboard{get; set;}
    public List<AbstractAsset> Assets{get; set;} = null!;
    public List<LevelAnim> LevelAnims{get; set;} = null!;
    public List<AbstractVolume> Volumes{get; set;} = null!;
    public List<AbstractCollision> Collisions{get; set;} = null!;
    public List<DynamicCollision> DynamicCollisions{get; set;} = null!;
    public List<Respawn> Respawns{get; set;} = null!;
    public List<DynamicRespawn> DynamicRespawns{get; set;} = null!;
    public List<AbstractItemSpawn> ItemSpawns{get; set;} = null!;
    public List<DynamicItemSpawn> DynamicItemSpawns{get; set;} = null!;
    public List<NavNode> NavNodes{get; set;} = null!;
    public List<DynamicNavNode> DynamicNavNodes{get; set;} = null!;
    public List<WaveData> WaveDatas{get; set;} = null!;
    public List<AnimatedBackground> AnimatedBackgrounds{get; set;} = null!;

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
        NavNodes = e.DeserializeChildrenOfType<NavNode>();
        DynamicNavNodes = e.DeserializeChildrenOfType<DynamicNavNode>();
        WaveDatas = e.DeserializeChildrenOfType<WaveData>();
        AnimatedBackgrounds = e.DeserializeChildrenOfType<AnimatedBackground>();
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("AssetDir", AssetDir);
        e.SetAttributeValue("LevelName", LevelName);
        if(NumFrames != 0)
            e.SetAttributeValue("NumFrames", NumFrames.ToString());
        if(SlowMult != 1)
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
        e.AddManySerialized(NavNodes);
        e.AddManySerialized(DynamicNavNodes);
        e.AddManySerialized(WaveDatas);
        e.AddManySerialized(AnimatedBackgrounds);
    }

    
    public void DrawOn<T>(ICanvas<T> canvas, RenderConfig config, Transform trans, TimeSpan time, RenderData data)
        where T : ITexture
    {
        data.AssetDir = AssetDir;
        data.DefaultNumFrames = NumFrames;
        data.DefaultSlowMult = SlowMult;
        foreach(Background b in Backgrounds)
            b.ChallengeCurrentBackground(data, config);
        data.PlatIDDynamicOffset = new();
        data.PlatIDMovingPlatformOffset = new();
        foreach(AbstractAsset a in Assets) if(a is MovingPlatform mp)
            mp.StoreOffset(data, time);
        foreach(AbstractCollision c in Collisions)
            c.CalculateCurve(0, 0);

        CameraBounds.DrawOn(canvas, config, trans, time, data);
        SpawnBotBounds.DrawOn(canvas, config, trans, time, data);
        foreach(Background b in Backgrounds)
            b.DrawOn(canvas, config, trans, time, data);
        //foreach(LevelSound ls in LevelSounds)
        TeamScoreboard?.DrawOn(canvas, config, trans, time, data);
        foreach(AbstractAsset a in Assets)
            a.DrawOn(canvas, config, trans, time, data);
        foreach(LevelAnim la in LevelAnims)
            la.DrawOn(canvas, config, trans, time, data);
        foreach(AbstractVolume v in Volumes)
            v.DrawOn(canvas, config, trans, time, data);
        foreach(AbstractCollision c in Collisions)
            c.DrawOn(canvas, config, trans, time, data);
        foreach(DynamicCollision dc in DynamicCollisions)
            dc.DrawOn(canvas, config, trans, time, data);
        foreach(Respawn r in Respawns)
            r.DrawOn(canvas, config, trans, time, data);
        foreach(DynamicRespawn dr in DynamicRespawns)
            dr.DrawOn(canvas, config, trans, time, data);
        foreach(AbstractItemSpawn i in ItemSpawns)
            i.DrawOn(canvas, config, trans, time, data);
        foreach(DynamicItemSpawn di in DynamicItemSpawns)
            di.DrawOn(canvas, config, trans, time, data);
        foreach(NavNode n in NavNodes)
            n.DrawOn(canvas, config, trans, time, data);
        foreach(DynamicNavNode dn in DynamicNavNodes)
            dn.DrawOn(canvas, config, trans, time, data);
        //foreach(WaveData wd in WaveDatas)
        //foreach(AnimatedBackground ab in AnimatedBackgrounds)


        //Gamemode stuff
        if(config.ScoringType == Enum.GetName(ScoringTypeEnum.RING))
        {
            if(config.ShowRingRopes)
            {
                T rope = canvas.LoadTextureFromSWF("bones/Bones_GameModes.swf", "a_DefaultRopes");
                canvas.DrawTexture(521, 1293, rope, trans, DrawPriorityEnum.FOREGROUND);
                canvas.DrawTexture(2934, 1293, rope, trans * Transform.CreateScale(-1, 1), DrawPriorityEnum.FOREGROUND);
            }
        }
        if(config.ScoringType == Enum.GetName(ScoringTypeEnum.ZOMBIE))
        {
            if(config.ShowZombieSpawns)
            {
                canvas.DrawCircle(230, 390, config.RadiusZombieSpawn, config.ColorZombieSpawns, trans, DrawPriorityEnum.DATA);
                canvas.DrawCircle(180, 900, config.RadiusZombieSpawn, config.ColorZombieSpawns, trans, DrawPriorityEnum.DATA);
                canvas.DrawCircle(-1160, 900, config.RadiusZombieSpawn, config.ColorZombieSpawns, trans, DrawPriorityEnum.DATA);
                canvas.DrawCircle(-1990, 390, config.RadiusZombieSpawn, config.ColorZombieSpawns, trans, DrawPriorityEnum.DATA);
            }
        }
        if(config.ScoringType == Enum.GetName(ScoringTypeEnum.BOMBSKETBALL))
        {
            if(config.ShowBombsketballTargets)
            {
                T red = canvas.LoadTextureFromSWF("bones/Bones_GameModes.swf", "a_TargetAnchoredRed");
                T blue = canvas.LoadTextureFromSWF("bones/Bones_GameModes.swf", "a_TargetAnchoredBlue");
                Goal? goalred = Volumes.OfType<Goal>().Where(g => g.Team == 2).FirstOrDefault();
                Goal? goalblue = Volumes.OfType<Goal>().Where(g => g.Team == 1).FirstOrDefault();
                if(goalred is not null)
                    canvas.DrawTexture((goalred.X+goalred.W)/2, (goalred.Y+goalred.H)/2, red, trans, DrawPriorityEnum.FOREGROUND);
                if(goalblue is not null)
                    canvas.DrawTexture((goalblue.X+goalblue.W)/2, (goalblue.Y+goalblue.H)/2, blue, trans, DrawPriorityEnum.FOREGROUND);
            }
        }
        if(config.ScoringType == Enum.GetName(ScoringTypeEnum.HORDE))
        {
            if(config.ShowHordeDoors)
            {
                int i = 0;
                foreach(Goal g in Volumes.OfType<Goal>())
                {
                    int hits = (i >= config.DamageHordeDoors.Length)?0:config.DamageHordeDoors[i];

                    if(hits < 24)
                    {
                        T door = hits switch
                        {
                            <= 6 => canvas.LoadTextureFromSWF("bones/Bones_GameModes.swf", "a_ValhallaDoor_000"),
                            <= 12 => canvas.LoadTextureFromSWF("bones/Bones_GameModes.swf", "a_ValhallaDoor_025"),
                            // <=23
                            _ => canvas.LoadTextureFromSWF("bones/Bones_GameModes.swf", "a_ValhallaDoor_050"),
                        };

                        canvas.DrawTexture((g.X+g.W)/2, (g.Y+g.H)/2, door, trans, DrawPriorityEnum.FOREGROUND);
                    }

                    ++i;
                }
            }
        }
    }
}
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
    public List<MovingPlatform> MovingPlatforms{get; set;} = null!;
    public List<Platform> Platforms{get; set;} = null!;
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

    public virtual void Deserialize(XElement element)
    {
        AssetDir = element.GetAttribute("AssetDir");
        LevelName = element.GetAttribute("LevelName");
        NumFrames = element.GetIntAttribute("NumFrames", 0);
        SlowMult = element.GetFloatAttribute("SlowMult", 1);
        CameraBounds = element.DeserializeChildOfType<CameraBounds>()!;
        SpawnBotBounds = element.DeserializeChildOfType<SpawnBotBounds>()!;
        Backgrounds = element.DeserializeChildrenOfType<Background>();
        LevelSounds = element.DeserializeChildrenOfType<LevelSound>();
        TeamScoreboard = element.DeserializeChildOfType<TeamScoreboard>();
        MovingPlatforms = element.DeserializeChildrenOfType<MovingPlatform>();
        Platforms = element.DeserializeChildrenOfType<Platform>();
        LevelAnims = element.DeserializeChildrenOfType<LevelAnim>();
        Volumes = element.DeserializeVolumeChildren();
        Collisions = element.DeserializeCollisionChildren();
        DynamicCollisions = element.DeserializeChildrenOfType<DynamicCollision>();
        Respawns = element.DeserializeChildrenOfType<Respawn>();
        DynamicRespawns = element.DeserializeChildrenOfType<DynamicRespawn>();
        ItemSpawns = element.DeserializeItemSpawnChildren();
        DynamicItemSpawns = element.DeserializeChildrenOfType<DynamicItemSpawn>();
        NavNodes = element.DeserializeChildrenOfType<NavNode>();
        DynamicNavNodes = element.DeserializeChildrenOfType<DynamicNavNode>();
        WaveDatas = element.DeserializeChildrenOfType<WaveData>();
        AnimatedBackgrounds = element.DeserializeChildrenOfType<AnimatedBackground>();
    }

    public XElement Serialize()
    {
        XElement e = new("LevelDesc");

        e.SetAttributeValue("AssetDir", AssetDir);
        e.SetAttributeValue("LevelName", LevelName);
        if(NumFrames != 0)
            e.SetAttributeValue("NumFrames", NumFrames.ToString());
        if(SlowMult != 1)
            e.SetAttributeValue("SlowMult", SlowMult.ToString());
        e.Add(CameraBounds.Serialize());
        e.Add(SpawnBotBounds.Serialize());
        foreach(Background b in Backgrounds)
            e.Add(b.Serialize());
        foreach(LevelSound ls in LevelSounds)
            e.Add(ls.Serialize());
        if(TeamScoreboard is not null)
            e.Add(TeamScoreboard.Serialize());
        foreach(MovingPlatform mp in MovingPlatforms)
            e.Add(mp.Serialize());
        foreach(Platform p in Platforms)
            e.Add(p.Serialize());
        foreach(LevelAnim la in LevelAnims)
            e.Add(la.Serialize());
        foreach(AbstractVolume v in Volumes)
            e.Add(v.Serialize());
        foreach(AbstractCollision c in Collisions)
            e.Add(c.Serialize());
        foreach(DynamicCollision dc in DynamicCollisions)
            e.Add(dc.Serialize());
        foreach(Respawn r in Respawns)
            e.Add(r.Serialize());
        foreach(DynamicRespawn dr in DynamicRespawns)
            e.Add(dr.Serialize());
        foreach(AbstractItemSpawn i in ItemSpawns)
            e.Add(i.Serialize());
        foreach(DynamicItemSpawn di in DynamicItemSpawns)
            e.Add(di.Serialize());
        foreach(NavNode n in NavNodes)
            e.Add(n.Serialize());
        foreach(DynamicNavNode dn in DynamicNavNodes)
            e.Add(dn.Serialize());
        foreach(WaveData wd in WaveDatas)
            e.Add(wd.Serialize());
        foreach(AnimatedBackground ab in AnimatedBackgrounds)
            e.Add(ab.Serialize());

        return e;
    }

    
    public void DrawOn<TTexture>
    (ICanvas<TTexture> canvas, GlobalRenderData rd, RenderSettings rs, Transform t, double time)
        where TTexture : ITexture
    {
        rd.AssetDir = AssetDir;
        rd.DefaultNumFrames = NumFrames;
        rd.DefaultSlowMult = SlowMult;
        foreach(Background b in Backgrounds)
            b.ChallengeCurrentBackground(rd, rs);

        CameraBounds.DrawOn(canvas, rd, rs, t, time);
        SpawnBotBounds.DrawOn(canvas, rd, rs, t, time);
        foreach(Background b in Backgrounds)
            b.DrawOn(canvas, rd, rs, t, time);
        //foreach(LevelSound ls in LevelSounds)
        TeamScoreboard?.DrawOn(canvas, rd, rs, t, time);
        foreach(MovingPlatform mp in MovingPlatforms)
            mp.DrawOn(canvas, rd, rs, t, time);
        foreach(Platform p in Platforms)
            p.DrawOn(canvas, rd, rs, t, time);
        //foreach(LevelAnim la in LevelAnims)
        foreach(AbstractVolume v in Volumes)
            v.DrawOn(canvas, rd, rs, t, time);
        foreach(AbstractCollision c in Collisions)
            c.DrawOn(canvas, rd, rs, t, time);
        foreach(DynamicCollision dc in DynamicCollisions)
            dc.DrawOn(canvas, rd, rs, t, time);
        foreach(Respawn r in Respawns)
            r.DrawOn(canvas, rd, rs, t, time);
        foreach(DynamicRespawn dr in DynamicRespawns)
            dr.DrawOn(canvas, rd, rs, t, time);
        foreach(AbstractItemSpawn i in ItemSpawns)
            i.DrawOn(canvas, rd, rs, t, time);
        foreach(DynamicItemSpawn di in DynamicItemSpawns)
            di.DrawOn(canvas, rd, rs, t, time);
        foreach(NavNode n in NavNodes)
            n.DrawOn(canvas, rd, rs, t, time);
        foreach(DynamicNavNode dn in DynamicNavNodes)
            dn.DrawOn(canvas, rd, rs, t, time);
        //foreach(WaveData wd in WaveDatas)
        //foreach(AnimatedBackground ab in AnimatedBackgrounds)
    }
}
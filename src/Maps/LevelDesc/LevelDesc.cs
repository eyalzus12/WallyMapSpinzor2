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
        e.Add(CameraBounds.SerializeToXElement());
        e.Add(SpawnBotBounds.SerializeToXElement());
        foreach(Background b in Backgrounds)
            e.Add(b.SerializeToXElement());
        foreach(LevelSound ls in LevelSounds)
            e.Add(ls.SerializeToXElement());
        if(TeamScoreboard is not null)
            e.Add(TeamScoreboard.SerializeToXElement());
        foreach(AbstractAsset a in Assets)
            e.Add(a.SerializeToXElement());
        foreach(LevelAnim la in LevelAnims)
            e.Add(la.SerializeToXElement());
        foreach(AbstractVolume v in Volumes)
            e.Add(v.SerializeToXElement());
        foreach(AbstractCollision c in Collisions)
            e.Add(c.SerializeToXElement());
        foreach(DynamicCollision dc in DynamicCollisions)
            e.Add(dc.SerializeToXElement());
        foreach(Respawn r in Respawns)
            e.Add(r.SerializeToXElement());
        foreach(DynamicRespawn dr in DynamicRespawns)
            e.Add(dr.SerializeToXElement());
        foreach(AbstractItemSpawn i in ItemSpawns)
            e.Add(i.SerializeToXElement());
        foreach(DynamicItemSpawn di in DynamicItemSpawns)
            e.Add(di.SerializeToXElement());
        foreach(NavNode n in NavNodes)
            e.Add(n.SerializeToXElement());
        foreach(DynamicNavNode dn in DynamicNavNodes)
            e.Add(dn.SerializeToXElement());
        foreach(WaveData wd in WaveDatas)
            e.Add(wd.SerializeToXElement());
        foreach(AnimatedBackground ab in AnimatedBackgrounds)
            e.Add(ab.SerializeToXElement());
    }

    
    public void DrawOn<TTexture>
    (ICanvas<TTexture> canvas, GlobalRenderData rd, RenderSettings rs, Transform t, TimeSpan time)
        where TTexture : ITexture
    {
        rd.AssetDir = AssetDir;
        rd.DefaultNumFrames = NumFrames;
        rd.DefaultSlowMult = SlowMult;
        foreach(Background b in Backgrounds)
            b.ChallengeCurrentBackground(rd, rs);
        rd.PlatIDDynamicOffset = new();
        rd.PlatIDMovingPlatformOffset = new();
        foreach(AbstractAsset a in Assets) if(a is MovingPlatform mp)
            mp.StoreOffset(rd, time);
        foreach(AbstractCollision c in Collisions)
            c.CalculateCurve(0, 0);

        CameraBounds.DrawOn(canvas, rd, rs, t, time);
        SpawnBotBounds.DrawOn(canvas, rd, rs, t, time);
        foreach(Background b in Backgrounds)
            b.DrawOn(canvas, rd, rs, t, time);
        //foreach(LevelSound ls in LevelSounds)
        TeamScoreboard?.DrawOn(canvas, rd, rs, t, time);
        foreach(AbstractAsset a in Assets)
            a.DrawOn(canvas, rd, rs, t, time);
        foreach(LevelAnim la in LevelAnims)
            la.DrawOn(canvas, rd, rs, t, time);
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


        //Gamemode stuff
        if(rs.ScoringType == Enum.GetName(ScoringTypeEnum.RING))
        {
            if(rs.ShowRingRopes)
            {
                TTexture rope = canvas.LoadTextureFromSWF("bones/Bones_GameModes.swf", "a_DefaultRopes");
                canvas.DrawTexture(521, 1293, rope, t, DrawPriorityEnum.FOREGROUND);
                canvas.DrawTexture(2934, 1293, rope, t * Transform.CreateScale(-1, 1), DrawPriorityEnum.FOREGROUND);
            }
        }
        if(rs.ScoringType == Enum.GetName(ScoringTypeEnum.ZOMBIE))
        {
            if(rs.ShowZombieSpawns)
            {
                canvas.DrawCircle(230, 390, rs.RadiusZombieSpawn, rs.ColorZombieSpawns, t, DrawPriorityEnum.DATA);
                canvas.DrawCircle(180, 900, rs.RadiusZombieSpawn, rs.ColorZombieSpawns, t, DrawPriorityEnum.DATA);
                canvas.DrawCircle(-1160, 900, rs.RadiusZombieSpawn, rs.ColorZombieSpawns, t, DrawPriorityEnum.DATA);
                canvas.DrawCircle(-1990, 390, rs.RadiusZombieSpawn, rs.ColorZombieSpawns, t, DrawPriorityEnum.DATA);
            }
        }
        if(rs.ScoringType == Enum.GetName(ScoringTypeEnum.BOMBSKETBALL))
        {
            if(rs.ShowBombsketballTargets)
            {
                TTexture red = canvas.LoadTextureFromSWF("bones/Bones_GameModes.swf", "a_TargetAnchoredRed");
                TTexture blue = canvas.LoadTextureFromSWF("bones/Bones_GameModes.swf", "a_TargetAnchoredBlue");
                Goal? goalred = Volumes.OfType<Goal>().Where(g => g.Team == 2).FirstOrDefault();
                Goal? goalblue = Volumes.OfType<Goal>().Where(g => g.Team == 1).FirstOrDefault();
                if(goalred is not null)
                    canvas.DrawTexture((goalred.X+goalred.W)/2, (goalred.Y+goalred.H)/2, red, t, DrawPriorityEnum.FOREGROUND);
                if(goalblue is not null)
                    canvas.DrawTexture((goalblue.X+goalblue.W)/2, (goalblue.Y+goalblue.H)/2, blue, t, DrawPriorityEnum.FOREGROUND);
            }
        }
        if(rs.ScoringType == Enum.GetName(ScoringTypeEnum.HORDE))
        {
            if(rs.ShowHordeDoors)
            {
                int i = 0;
                foreach(Goal g in Volumes.OfType<Goal>())
                {
                    int hits = (i >= rs.DamageHordeDoors.Length)?0:rs.DamageHordeDoors[i];

                    if(hits < 24)
                    {
                        TTexture door = hits switch
                        {
                            <= 6 => canvas.LoadTextureFromSWF("bones/Bones_GameModes.swf", "a_ValhallaDoor_000"),
                            <= 12 => canvas.LoadTextureFromSWF("bones/Bones_GameModes.swf", "a_ValhallaDoor_025"),
                            // <=23
                            _ => canvas.LoadTextureFromSWF("bones/Bones_GameModes.swf", "a_ValhallaDoor_050"),
                        };

                        canvas.DrawTexture((g.X+g.W)/2, (g.Y+g.H)/2, door, t, DrawPriorityEnum.FOREGROUND);
                    }

                    ++i;
                }
            }
        }
    }
}
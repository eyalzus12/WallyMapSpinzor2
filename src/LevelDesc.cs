using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelDesc : IDeserializable
{
    public string AssetDir{get; set;} = null!;
    public string LevelName{get; set;} = null!;
    public int NumFrames{get; set;}
    public double SlowMult{get; set;}
    public CameraBounds? CameraBounds{get; set;}
    public SpawnBotBounds? SpawnBotBounds{get; set;}
    public List<AbstractCollision> Collisions{get; set;} = null!;
    public List<Respawn> Respawns{get; set;} = null!;
    public List<AbstractItemSpawn> ItemSpawns{get; set;} = null!;
    public List<NavNode> NavNodes{get; set;} = null!;
    public List<DynamicCollision> DynamicCollisions{get; set;} = null!;
    public List<DynamicItemSpawn> DynamicItemSpawns{get; set;} = null!;
    public List<DynamicRespawn> DynamicRespawns{get; set;} = null!;
    public List<DynamicNavNode> DynamicNavNodes{get; set;} = null!;
    public List<Background> Backgrounds{get; set;} = null!;
    public List<Platform> Platforms{get; set;} = null!;
    public List<WaveData> WaveDatas{get; set;} = null!;
    public List<AbstractVolume> Volumes{get; set;} = null!;
    public TeamScoreboard? TeamScoreboard{get; set;}
    public List<LevelAnim> LevelAnims{get; set;} = null!;
    public List<LevelSound> LevelSounds{get; set;} = null!;
    public List<MovingPlatform> MovingPlatforms{get; set;} = null!;
    public List<AnimatedBackground> AnimatedBackgrounds{get; set;} = null!;

    public virtual void Deserialize(XElement element)
    {
        AssetDir = element.GetAttribute("AssetDir");
        LevelName = element.GetAttribute("LevelName");
        NumFrames = element.GetIntAttribute("NumFrames", 0);
        SlowMult = element.GetFloatAttribute("SlowMult", 1);
        CameraBounds = element.DeserializeChildOfType<CameraBounds>();
        SpawnBotBounds = element.DeserializeChildOfType<SpawnBotBounds>();
        Collisions = element.DeserializeCollisionChildren();
        Respawns = element.DeserializeChildrenOfType<Respawn>();
        ItemSpawns = element.DeserializeItemSpawnChildren();
        NavNodes = element.DeserializeChildrenOfType<NavNode>();
        DynamicCollisions = element.DeserializeChildrenOfType<DynamicCollision>();
        DynamicItemSpawns = element.DeserializeChildrenOfType<DynamicItemSpawn>();
        DynamicRespawns = element.DeserializeChildrenOfType<DynamicRespawn>();
        DynamicNavNodes = element.DeserializeChildrenOfType<DynamicNavNode>();
        Backgrounds = element.DeserializeChildrenOfType<Background>();
        Platforms = element.DeserializeChildrenOfType<Platform>();
        WaveDatas = element.DeserializeChildrenOfType<WaveData>();
        Volumes = element.DeserializeVolumeChildren();
        TeamScoreboard = element.DeserializeChildOfType<TeamScoreboard>();
        LevelAnims = element.DeserializeChildrenOfType<LevelAnim>();
        LevelSounds = element.DeserializeChildrenOfType<LevelSound>();
        MovingPlatforms = element.DeserializeChildrenOfType<MovingPlatform>();
        AnimatedBackgrounds = element.DeserializeChildrenOfType<AnimatedBackground>();
    }
}
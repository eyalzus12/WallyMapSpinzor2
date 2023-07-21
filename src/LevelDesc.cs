using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelDesc : IDeserializable
{
    public string AssetDir{get; set;} = "";
    public string LevelName{get; set;} = "";
    public int NumFrames{get; set;}
    public double SlowMult{get; set;}
    public CameraBounds? CameraBounds{get; set;}
    public SpawnBotBounds? SpawnBotBounds{get; set;}
    public List<CollisionBase> Collisions{get; set;} = new();
    public List<Respawn> Respawns{get; set;} = new();
    public List<ItemSpawnBase> ItemSpawns{get; set;} = new();
    public List<NavNode> NavNodes{get; set;} = new();
    public List<DynamicCollision> DynamicCollisions{get; set;} = new();
    public List<DynamicItemSpawn> DynamicItemSpawns{get; set;} = new();
    public List<DynamicRespawn> DynamicRespawns{get; set;} = new();
    public List<DynamicNavNode> DynamicNavNodes{get; set;} = new();
    public List<Background> Backgrounds{get; set;} = new();
    public List<Platform> Platforms{get; set;} = new();

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

    }
}
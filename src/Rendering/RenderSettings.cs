namespace WallyMapSpinzor2;

public class RenderSettings
{
    public bool RespawnShow{get; set;} = true;
    public bool ItemSpawnShow{get; set;} = true;

    public double RadiusRespawn{get; set;} = 30;

    public Color ColorRespawn{get; set;} = new(255, 127, 0, 127);
    public Color ColorInitialRespawn{get; set;} = new(255, 127, 0, 127);
    public Color ColorExpandedInitRespawn{get; set;} = new(255, 127, 0, 127);
    public Color ColorItemSpawn{get; set;} = new(0, 127, 255, 127);
    public Color ColorItemInitSpawn{get; set;} = new(127, 0, 127, 127);
    public Color ColorItemSet{get; set;} = new(0, 0, 127, 127);
    public Color ColorTeamItemInitSpawn{get; set;} = new(0, 127, 0, 127);
}
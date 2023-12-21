namespace WallyMapSpinzor2;

public class TeamItemInitSpawn : AbstractItemSpawn
{
    public override double DefaultX => 1.79769313486231e+308;
    public override double DefaultY => 1.79769313486231e+308;
    public override double DefaultW => 50;
    public override double DefaultH => 50;
    
    public override Color GetColor(RenderConfig config) => config.ColorTeamItemInitSpawn;
}

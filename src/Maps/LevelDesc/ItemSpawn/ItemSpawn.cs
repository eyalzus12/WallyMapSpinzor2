namespace WallyMapSpinzor2;

public class ItemSpawn : AbstractItemSpawn
{
    public override double DefaultX => 1.79769313486231e+308;
    public override double DefaultY => 1.79769313486231e+308;
    public override double DefaultW => 1.79769313486231e+308;
    public override double DefaultH => 10;

    public override Color GetColor(RenderConfig config) => config.ColorItemSpawn;
}
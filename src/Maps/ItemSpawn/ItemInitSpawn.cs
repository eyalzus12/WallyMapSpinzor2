namespace WallyMapSpinzor2;

public class ItemInitSpawn : AbstractItemSpawn
{
    public override double DefaultX => 1.79769313486231e+308;
    public override double DefaultY => 1.79769313486231e+308;
    public override double DefaultW => 50;
    public override double DefaultH => 50;

    public override Color GetColor(RenderSettings rs) => rs.ColorItemInitSpawn;
}
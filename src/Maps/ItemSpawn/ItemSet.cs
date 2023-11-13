namespace WallyMapSpinzor2;

public class ItemSet : AbstractItemSpawn
{
    public override double DefaultX => 1.79769313486231e+308;
    public override double DefaultY => 1.79769313486231e+308;
    public override double DefaultW => 40;
    public override double DefaultH => 40;

    public override Color GetColor(RenderSettings rs) => rs.ColorItemSet;
}
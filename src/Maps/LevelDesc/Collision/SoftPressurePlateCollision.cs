namespace WallyMapSpinzor2;

public class SoftPressurePlateCollision : AbstractPressurePlateCollision
{
    public override Color GetColor(RenderConfig config) => config.ColorSoftPressurePlateCollision;
    public override CollisionTypeFlags CollisionType => CollisionTypeFlags.SOFT | CollisionTypeFlags.PRESSURE_PLATE;
}
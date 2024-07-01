namespace WallyMapSpinzor2;

public class PressurePlateCollision : AbstractPressurePlateCollision
{
    public override Color GetColor(RenderConfig config) => config.ColorPressurePlateCollision;
    public override CollisionTypeFlags CollisionType => CollisionTypeFlags.HARD | CollisionTypeFlags.PRESSURE_PLATE;
}
namespace WallyMapSpinzor2;

public class PressurePlateCollision : AbstractPressurePlateCollision
{
    public override Color GetColor(RenderConfig config) => config.ColorPressurePlateCollision;
    public override CollisionTypeEnum CollisionType => CollisionTypeEnum.HARD | CollisionTypeEnum.PRESSURE_PLATE;
}
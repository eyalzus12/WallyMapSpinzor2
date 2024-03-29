namespace WallyMapSpinzor2;

public class HardCollision : AbstractCollision
{
    public override Color GetColor(RenderConfig config) => config.ColorHardCollision;
    public override CollisionTypeEnum CollisionType => CollisionTypeEnum.HARD;
}
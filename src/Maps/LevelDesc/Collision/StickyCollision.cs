namespace WallyMapSpinzor2;

public class StickyCollision : AbstractCollision
{
    public override Color GetColor(RenderConfig config) => config.ColorStickyCollision;
    public override CollisionTypeEnum CollisionType => CollisionTypeEnum.HARD | CollisionTypeEnum.STICKY;
}

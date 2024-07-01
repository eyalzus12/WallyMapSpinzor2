namespace WallyMapSpinzor2;

public class StickyCollision : AbstractCollision
{
    public override Color GetColor(RenderConfig config) => config.ColorStickyCollision;
    public override CollisionTypeFlags CollisionType => CollisionTypeFlags.HARD | CollisionTypeFlags.STICKY;
}
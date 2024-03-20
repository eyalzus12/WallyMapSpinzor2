namespace WallyMapSpinzor2;

public class LavaCollision : AbstractCollision
{
    public override Color GetColor(RenderConfig config) => config.ColorLavaCollision;
    public override CollisionTypeEnum CollisionType => CollisionTypeEnum.HARD | CollisionTypeEnum.GAMEMODE | CollisionTypeEnum.LAVA;
}
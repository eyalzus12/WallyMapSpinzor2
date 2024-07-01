namespace WallyMapSpinzor2;

public class GameModeHardCollision : AbstractCollision
{
    public override Color GetColor(RenderConfig config) => config.ColorGameModeHardCollision;
    public override CollisionTypeFlags CollisionType => CollisionTypeFlags.HARD | CollisionTypeFlags.GAMEMODE;
}
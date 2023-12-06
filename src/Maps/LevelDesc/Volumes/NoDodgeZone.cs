namespace WallyMapSpinzor2;

public class NoDodgeZone : AbstractVolume
{
    public override bool ShouldShow(RenderConfig config) => config.ShowNoDodgeZone;
}
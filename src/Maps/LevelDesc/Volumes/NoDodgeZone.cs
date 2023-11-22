namespace WallyMapSpinzor2;

public class NoDodgeZone : AbstractVolume
{
    public override bool ShouldShow(RenderSettings rs) => rs.ShowNoDodgeZone;
}
namespace WallyMapSpinzor2;

public class Volume : AbstractVolume
{
    public override bool ShouldShow(RenderSettings rs) => rs.ShowVolume;
}
namespace WallyMapSpinzor2;

public class Volume : AbstractVolume
{
    public override bool ShouldShow(RenderConfig config) => config.ShowVolume;
}
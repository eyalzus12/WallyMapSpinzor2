namespace WallyMapSpinzor2;

public class Goal : AbstractVolume
{
    public override bool ShouldShow(RenderConfig config) => config.ShowGoal;
}
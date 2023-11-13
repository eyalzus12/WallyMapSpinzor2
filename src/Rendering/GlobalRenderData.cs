namespace WallyMapSpinzor2;

public class GlobalRenderData
{
    public string? AssetDir{get; set;}
    public int? DefaultNumFrames{get; set;}
    public double? DefaultSlowMult{get; set;}

    public double? BackgroundRect_X{get; set;}
    public double? BackgroundRect_Y{get; set;}
    public double? BackgroundRect_W{get; set;}
    public double? BackgroundRect_H{get; set;}

    public Background? CurrentBackground{get; set;} = null;

    public Dictionary<string, (double, double)> PlatIDDict{get; set;} = new();
}
using System.Collections.Generic;

namespace WallyMapSpinzor2;

public class RenderData
{
    public string? AssetDir { get; set; }
    public int? DefaultNumFrames { get; set; }
    public double? DefaultSlowMult { get; set; }

    public double? BackgroundRect_X { get; set; }
    public double? BackgroundRect_Y { get; set; }
    public double? BackgroundRect_W { get; set; }
    public double? BackgroundRect_H { get; set; }

    public Background? CurrentBackground { get; set; } = null;

    public Dictionary<string, (double, double)> PlatIDDynamicOffset { get; set; } = [];
    public Dictionary<string, (double, double)> PlatIDMovingPlatformOffset { get; set; } = [];

    public Dictionary<int, (double, double)> NavIDDictionary { get; set; } = [];
}
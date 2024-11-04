using System.Collections.Generic;

namespace WallyMapSpinzor2;

public class RenderContext
{
    public string? AssetDir { get; set; }
    public uint? ExtraStartFrame { get; set; }
    public int? DefaultNumFrames { get; set; }
    public double? DefaultSlowMult { get; set; }

    public double? BackgroundRect_X { get; set; }
    public double? BackgroundRect_Y { get; set; }
    public double? BackgroundRect_W { get; set; }
    public double? BackgroundRect_H { get; set; }

    public Background? CurrentBackground { get; set; } = null;

    public Dictionary<string, (double, double)> PlatIDDynamicOffset { get; set; } = [];
    public Dictionary<string, Transform> PlatIDMovingPlatformTransform { get; set; } = [];
    /*
    DynamicCollision (and no other dynamics) have a "bug",
    where only the last DynamicCollision will actually get offset by the moving platform.
    This emulates that behavior.
    */
    public Dictionary<string, DynamicCollision> DynamicCollisionPlatIDOwner { get; set; } = [];

    public Dictionary<uint, (double, double)> NavIDDictionary { get; set; } = [];
}
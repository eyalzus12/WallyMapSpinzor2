namespace WallyMapSpinzor2;

public static class MapUtils
{
    public static DirEnum ParseDir(string? s) =>
        (s is null)?DirEnum.ANY:
        Enum.TryParse(s.ToUpperInvariant(), out DirEnum d)?d:
        DirEnum.ANY;

    public static PathEnum ParsePath(string? s) =>
        (s is null)?PathEnum.ANY:
        Enum.TryParse(s.ToUpperInvariant(), out PathEnum p)?p:
        (PathEnum)int.Parse(s);

    public static BehaviorEnum ParseBehavior(string? s) =>
        (s is null)?BehaviorEnum.NORMAL:
        Enum.TryParse(s.ToUpperInvariant(), out BehaviorEnum b)?b:
        BehaviorEnum.NORMAL;

    public static readonly HashSet<DirEnum> DEFAULT_SHARED_DIR = new(){DirEnum.LEFT, DirEnum.RIGHT, DirEnum.TOP, DirEnum.BOTTOM, DirEnum.SIDE};
    public static bool IsSharedDir(DirEnum d) => DEFAULT_SHARED_DIR.Contains(d);
    public static bool IsSharedPath(PathEnum p) => p < (PathEnum)1048575; //numeric path

    public static string FixBmg(string s) => s
        .Replace("PlatID=\"3\"X", "PlatID=\"3\" X"); //OneUpOneDownFFA3
}
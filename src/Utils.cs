namespace WallyMapSpinzor2;

public static class Utils
{
    public static double? ParseFloatOrNull(string? s) => (s is null)?null:double.Parse(s);
    public static bool? ParseBoolOrNull(string? s) => (s is null)?null:(s.ToUpper() == "TRUE");
    public static int? ParseIntOrNull(string? s) => (s is null)?null:int.Parse(s);
    public static uint? ParseUIntOrNull(string? s) => (s is null)?null:uint.Parse(s);

    public static Dir ParseDir(string? s) =>
        (s is null)?Dir.ANY:
        Enum.TryParse(s.ToUpper(), out Dir d)?d:
        Dir.ANY;

    public static Path ParsePath(string? s) =>
        (s is null)?Path.ANY:
        Enum.TryParse(s.ToUpper(), out Path p)?p:
        (Path)int.Parse(s);

    public static Behavior ParseBehavior(string? s) =>
        (s is null)?Behavior.NORMAL:
        Enum.TryParse(s.ToUpper(), out Behavior b)?b:
        Behavior.NORMAL;

    public static readonly HashSet<Dir> DEFAULT_SHARED_DIR = new(){Dir.LEFT, Dir.RIGHT, Dir.TOP, Dir.BOTTOM, Dir.SIDE};
    public static bool IsSharedDir(Dir d) => DEFAULT_SHARED_DIR.Contains(d);
    public static bool IsSharedPath(Path p) => p < (Path)1048575; //numeric path

    public static double DegToRad(double r) => r*Math.PI/180;

    public static void ForEach<T>(this IEnumerable<T> e, Action<T> a)
    {
        foreach(var ee in e)a(ee);
    }

    public static string FixBmg(string s) => s
        .Replace("PlatID=\"3\"X", "PlatID=\"3\" X") //OneUpOneDownFFA3
        .Replace("Path=\"CLOSE\"/>`", "Path=\"CLOSE\"/>") //HordeTwo
        .Replace("</FrameOffset>25", "</FrameOffset>"); //BP8ThreePlatformFFABig, BP8ThreePlatform
}
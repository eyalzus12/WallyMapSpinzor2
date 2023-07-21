namespace WallyMapSpinzor2;

public static class Utils
{
    public static double? ParseFloatOrNull(string? s)
    {
        if(s is null) return null;
        return double.Parse(s);
    }

    public static bool? ParseBoolOrNull(string? s)
    {
        if(s is null) return null;
        return s.ToUpper() == "TRUE";
    }

    public static int? ParseIntOrNull(string? s)
    {
        if(s is null) return null;
        return int.Parse(s);
    }

    public static Dir ParseDir(string? s)
    {
        if(s is null) return Dir.ANY;
        Dir d;
        if(!Enum.TryParse<Dir>(s.ToUpper(), out d)) d = Dir.ANY;
        return d;
    }

    public static Path ParsePath(string? s)
    {
        if(s is null) return Path.ANY;
        Path p;
        if(!Enum.TryParse<Path>(s.ToUpper(), out p)) p = (Path)int.Parse(s);
        return p;
    }

    public static Behavior ParseBehavior(string? s)
    {
        if(s is null) return (Behavior)0;
        Behavior b;
        if(!Enum.TryParse<Behavior>(s.ToUpper(), out b)) b = (Behavior)0;
        return b;
    }

    public static bool IsSharedDir(Dir d) => (d <= (Dir)3)||(d == Dir.SIDE);
    public static bool IsSharedPath(Path p) => (p < (Path)1048575);

    public static double DegToRad(double r) => r*Math.PI/180;

    public static string FixBmg(string s) => s
        .Replace("PlatID=\"3\"X", "PlatID=\"3\" X")
        .Replace("Path=\"CLOSE\"/>`", "Path=\"CLOSE\"/>");
}
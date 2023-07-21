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

    public static double DegToRad(double r) => r*Math.PI/180;
}
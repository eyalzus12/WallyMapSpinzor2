namespace WallyMapSpinzor2;

public static class Utils
{
    public static double? ParseFloatOrNull(string? s) => (s is null)?null:double.Parse(s);
    public static bool? ParseBoolOrNull(string? s) => (s is null)?null:(s.ToUpper() == "TRUE");
    public static int? ParseIntOrNull(string? s) => (s is null)?null:int.Parse(s);
    public static uint? ParseUIntOrNull(string? s) => (s is null)?null:uint.Parse(s);
}
namespace WallyMapSpinzor2;

public static class Utils
{
    public static float? ParseFloatOrNull(string? s)
    {
        if(s is null) return null;
        return float.Parse(s);
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

    public static NavNodeData ParseNavNodeData(string s)
    {
        int id;
        if(!int.TryParse(s, out id))
        {
            id = int.Parse(s[1..]);
            NavNodeData.NavNodeType type = 
                Enum.Parse<NavNodeData.NavNodeType>(s[0..1]);
            return new(id, type);
        }
        else
            return new NavNodeData(id, NavNodeData.NavNodeType.None);
    }

    public static string NavNodeTypeToString(this NavNodeData.NavNodeType type) => type switch
    {
        NavNodeData.NavNodeType.None => "",
        NavNodeData.NavNodeType.A => "A",
        NavNodeData.NavNodeType.D => "D",
        NavNodeData.NavNodeType.G => "G",
        NavNodeData.NavNodeType.L => "L",
        NavNodeData.NavNodeType.W => "W",
        _ => throw new ArgumentException($"Unknown navnode type {type} encountered")
    };
}
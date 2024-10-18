using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class NavNode : IDeserializable, ISerializable, IDrawable
{
    public int NavID { get; set; }
    public NavNodeTypeEnum Type { get; set; }
    // the given type in the path doesn't actually do anything
    public (int, NavNodeTypeEnum)[] Path { get; set; } = null!;
    public double X { get; set; }
    public double Y { get; set; }

    public DynamicNavNode? Parent { get; set; }

    public void Deserialize(XElement e)
    {
        (NavID, Type) = ParseNavID(e.GetAttribute("NavID"));
        //the "not empty" is a guard against an empty path, where an empty string would be passed to ParseNavID
        Path = [.. e.GetAttribute("Path").Split(',').Where(s => s != "").Select(ParseNavID)];
        X = e.GetDoubleAttribute("X", 0);
        Y = e.GetDoubleAttribute("Y", 0);
    }

    public static (int, NavNodeTypeEnum) ParseNavID(string navId)
    {
        return '0' <= navId[0] && navId[0] <= '9'
            ? (int.Parse(navId, CultureInfo.InvariantCulture), NavNodeTypeEnum._)
            : (int.Parse(navId[1..], CultureInfo.InvariantCulture), Enum.TryParse(
                navId[0].ToString(), out NavNodeTypeEnum type)
                    ? type
                    : NavNodeTypeEnum._
            );
    }

    public static string NavIDToString(int id, NavNodeTypeEnum type)
    {
        return type == NavNodeTypeEnum._
            ? id.ToString()
            : $"{type}{id}";
    }

    private static Color GetNavIDColor(NavNodeTypeEnum type, RenderConfig config) => type switch
    {
        NavNodeTypeEnum.W => config.ColorNavNodeW,
        NavNodeTypeEnum.A => config.ColorNavNodeA,
        NavNodeTypeEnum.L => config.ColorNavNodeL,
        NavNodeTypeEnum.G => config.ColorNavNodeG,
        NavNodeTypeEnum.T => config.ColorNavNodeT,
        NavNodeTypeEnum.S => config.ColorNavNodeS,
        _ => config.ColorNavNode
    };

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("NavID", NavIDToString(NavID, Type));
        e.SetAttributeValue("Path", string.Join(',', Path.Select(_ => NavIDToString(_.Item1, _.Item2))));
        if (X != 0)
            e.SetAttributeValue("X", X);
        if (Y != 0)
            e.SetAttributeValue("Y", Y);
    }

    public void RegisterNavNode(RenderContext data, double xOff = 0, double yOff = 0)
    {
        data.NavIDDictionary[NavID] = (X + xOff, Y + yOff);
    }

    public void DrawOn(ICanvas canvas, Transform trans, RenderConfig config, RenderContext context, RenderState state)
    {
        if (!config.ShowNavNode) return;
        (double x, double y) = trans * (X, Y);
        canvas.DrawCircle(x, y, config.RadiusNavNode, GetNavIDColor(Type, config), Transform.IDENTITY, DrawPriorityEnum.NAVNODE, this);
        foreach ((int id, _) in Path)
        {
            if (context.NavIDDictionary.TryGetValue(id, out (double, double) pos))
            {
                (double targetX, double targetY) = pos;
                canvas.DrawArrow(x, y, targetX, targetY, config.OffsetNavLineArrowSide, config.OffsetNavLineArrowBack, config.ColorNavPath, Transform.IDENTITY, DrawPriorityEnum.NAVLINE, this);
            }
        }
    }
}
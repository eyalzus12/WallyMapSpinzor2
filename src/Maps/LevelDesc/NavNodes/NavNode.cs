using System.Data;
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

    public void Deserialize(XElement e)
    {
        (NavID, Type) = ParseNavID(e.GetAttribute("NavID"));
        //the "not empty" is a guard against an empty path, where an empty string would be passed to ParseNavID
        Path = e.GetAttribute("Path").Split(',').Where(s => s != "").Select(ParseNavID).ToArray();
        X = e.GetFloatAttribute("X");
        Y = e.GetFloatAttribute("Y");
    }

    private static (int, NavNodeTypeEnum) ParseNavID(string navId)
    {
        return '0' <= navId[0] && navId[0] <= '9'
            ? (int.Parse(navId), NavNodeTypeEnum._)
            : (int.Parse(navId[1..]), Enum.TryParse(
                navId[0].ToString(), out NavNodeTypeEnum type)
                    ? type
                    : NavNodeTypeEnum._
            );
    }

    private static string NavIDToString(int id, NavNodeTypeEnum type)
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
            e.SetAttributeValue("X", X.ToString());
        if (Y != 0)
            e.SetAttributeValue("Y", Y.ToString());
    }

    public void RegisterNavNode(RenderData data, double xOff = 0, double yOff = 0)
    {
        data.NavIDDictionary[NavID] = (X + xOff, Y + yOff);
    }

    public void DrawOn<T>
    (ICanvas<T> canvas, RenderConfig config, Transform cameraTrans, Transform trans, TimeSpan time, RenderData data)
        where T : ITexture
    {
        if (!config.ShowNavNode) return;
        (double x, double y) = trans * (X, Y);
        canvas.DrawCircle(x, y, config.RadiusNavNode, GetNavIDColor(Type, config), cameraTrans, DrawPriorityEnum.NAVNODE);
        foreach ((int id, _) in Path)
        {
            if (data.NavIDDictionary.TryGetValue(id, out (double, double) pos))
            {
                (double targetX, double targetY) = pos;
                canvas.DrawLine(x, y, targetX, targetY, config.ColorNavPath, cameraTrans, DrawPriorityEnum.NAVNODE);
                // draw arrow parts
                // we start with an arrow pointing right
                // and we rotate it to match
                double length = BrawlhallaMath.Length(targetX - x, targetY - y); // arrow length
                (double dirX, double dirY) = BrawlhallaMath.Normalize(targetX - x, targetY - y); // arrow direction
                double angle = Math.Atan2(dirY, dirX); // arrow angle
                // calculate end points by applying the rotation to the arrow
                (double arrowEndX1, double arrowEndY1) = BrawlhallaMath.Rotated(length - config.OffsetNavLineArrowBack, config.OffsetNavLineArrowSide, angle);
                (double arrowEndX2, double arrowEndY2) = BrawlhallaMath.Rotated(length - config.OffsetNavLineArrowBack, -config.OffsetNavLineArrowSide, angle);
                // draw the lines
                canvas.DrawLine(targetX, targetY, x + arrowEndX1, y + arrowEndY1, config.ColorNavPath, cameraTrans, DrawPriorityEnum.NAVLINE);
                canvas.DrawLine(targetX, targetY, x + arrowEndX2, y + arrowEndY2, config.ColorNavPath, cameraTrans, DrawPriorityEnum.NAVLINE);
            }
        }
    }
}

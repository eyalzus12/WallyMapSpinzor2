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
        Path = e.GetAttribute("Path").Split(',').Select(ParseNavID).ToArray();
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

    public void RegisterNavNode(RenderData data)
    {
        data.NavIDDictionary[NavID] = (X, Y);
    }

    public void DrawOn<T>
    (ICanvas<T> canvas, RenderConfig config, Transform trans, TimeSpan time, RenderData data)
        where T : ITexture
    {
        if (!config.ShowNavNode) return;
        canvas.DrawCircle(X, Y, config.RadiusNavNode, GetNavIDColor(Type, config), trans, DrawPriorityEnum.NAVNODE);
        foreach ((int id, _) in Path)
        {
            if (data.NavIDDictionary.TryGetValue(id, out (double, double) pos))
            {
                (double x, double y) = pos;
                canvas.DrawLine(X, Y, x, y, config.ColorNavPath, trans, DrawPriorityEnum.NAVNODE);
                //draw arrow parts
                //we start with a right arrow ->
                //and we rotate it to match
                double length = BrawlhallaMath.Length(x - X, y - Y); // arrow length
                double angle = BrawlhallaMath.AngleBetween(length, 0, x - X, y - Y); // angle from right arrow to desired arrow
                Transform arrowTransform = trans * Transform.CreateFrom(rot: angle); //matching transformation
                //draw the lines
                canvas.DrawLine(x, y, x + config.OffsetArrowBack, y + config.OffsetArrowSide, config.ColorNavPath, arrowTransform, DrawPriorityEnum.NAVNODE);
                canvas.DrawLine(x, y, x + config.OffsetArrowBack, y - config.OffsetArrowSide, config.ColorNavPath, arrowTransform, DrawPriorityEnum.NAVNODE);
            }
        }
    }
}

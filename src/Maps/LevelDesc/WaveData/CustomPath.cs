using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class CustomPath : IDeserializable, ISerializable, IDrawable
{
    public List<Point> Points { get; set; } = null!;

    public void Deserialize(XElement e)
    {
        Points = e.DeserializeChildrenOfType<Point>();
    }

    public void Serialize(XElement e)
    {
        e.AddManySerialized(Points);
    }

    public void DrawOn(ICanvas canvas, RenderConfig config, Transform trans, TimeSpan time, RenderData data)
    {
        foreach (Point p in Points)
        {
            p.DrawOn(canvas, config, trans, time, data);
        }

        for (int i = 0; i < Points.Count - 1; ++i)
        {
            canvas.DrawArrow(
                Points[i].X, Points[i].Y,
                Points[i + 1].X, Points[i + 1].Y,
                config.OffsetHordePathArrowSide, config.OffsetHordePathArrowBack,
                config.ColorHordePath, trans, DrawPriorityEnum.DATA, this
            );
        }
    }
}
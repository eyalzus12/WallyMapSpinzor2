using System;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractVolume : IDeserializable, ISerializable, IDrawable
{
    public int X { get; set; }
    public int Y { get; set; }
    public int W { get; set; }
    public int H { get; set; }
    public int Team { get; set; }
    public int ID { get; set; }

    public virtual void Deserialize(XElement e)
    {
        // For some reason Horde stores one of those values as a float, despite the game converting it to an int.
        X = (int)e.GetFloatAttribute("X");
        Y = (int)e.GetFloatAttribute("Y");
        W = (int)e.GetFloatAttribute("W");
        H = (int)e.GetFloatAttribute("H");
        Team = e.GetIntAttribute("Team");
        ID = e.GetIntAttribute("ID", 0);
    }

    public virtual void Serialize(XElement e)
    {
        e.SetAttributeValue("H", H);
        e.SetAttributeValue("Team", Team);
        e.SetAttributeValue("W", W);
        e.SetAttributeValue("X", X);
        e.SetAttributeValue("Y", Y);
        if (ID != 0)
            e.SetAttributeValue("ID", ID);
    }

    public virtual void DrawOn<T>(ICanvas<T> canvas, RenderConfig config, Transform trans, TimeSpan time, RenderData data)
        where T : ITexture
    {
        if (!ShouldShow(config))
            return;

        if (Team >= config.ColorVolumeTeam.Length)
            throw new ArgumentOutOfRangeException($"Volume has team {Team}, which is larger than max available volume team color {config.ColorVolumeTeam.Length - 1}");
        canvas.DrawRect(X, Y, W, H, true, config.ColorVolumeTeam[Team], trans, DrawPriorityEnum.VOLUMES);
    }

    public abstract bool ShouldShow(RenderConfig config);
}
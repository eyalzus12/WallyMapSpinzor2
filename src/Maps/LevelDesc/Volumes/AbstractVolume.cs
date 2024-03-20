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
        X = e.GetIntAttribute("X");
        Y = e.GetIntAttribute("Y");
        W = e.GetIntAttribute("W");
        H = e.GetIntAttribute("H");
        Team = e.GetIntAttribute("Team");
        ID = e.GetIntAttribute("ID", 0);
    }

    public virtual void Serialize(XElement e)
    {
        e.SetAttributeValue("H", H.ToString());
        e.SetAttributeValue("Team", Team.ToString());
        e.SetAttributeValue("W", W.ToString());
        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());
        if (ID != 0)
            e.SetAttributeValue("ID", ID.ToString());
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
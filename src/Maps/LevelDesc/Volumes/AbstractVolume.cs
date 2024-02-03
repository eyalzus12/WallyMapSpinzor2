using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractVolume : IDeserializable, ISerializable, IDrawable
{
    public double H { get; set; }
    public int Team { get; set; }
    public double W { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public int ID { get; set; }

    public virtual void Deserialize(XElement e)
    {
        H = e.GetFloatAttribute("H");
        Team = e.GetIntAttribute("Team");
        W = e.GetFloatAttribute("W");
        X = e.GetFloatAttribute("X");
        Y = e.GetFloatAttribute("Y");
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

    public virtual void DrawOn<T>(ICanvas<T> canvas, RenderConfig config, Transform cameraTrans, Transform trans, TimeSpan time, RenderData data)
        where T : ITexture
    {
        if (!ShouldShow(config)) return;

        if (Team >= config.ColorVolumeTeam.Length)
            throw new ArgumentOutOfRangeException($"Volume has team {Team}, which is larger than max available volume team color {config.ColorVolumeTeam.Length - 1}");

        canvas.DrawRect((int)X, (int)Y, (int)W, (int)H, true, config.ColorVolumeTeam[Team], cameraTrans * trans, DrawPriorityEnum.VOLUMES);
    }

    public abstract bool ShouldShow(RenderConfig config);
}

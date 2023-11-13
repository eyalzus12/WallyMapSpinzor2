using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractVolume : IDeserializable, ISerializable, IDrawable
{
    public double H{get; set;}
    public int Team{get; set;}
    public double W{get; set;}
    public double X{get; set;}
    public double Y{get; set;}
    public int ID{get; set;}
    
    public virtual void Deserialize(XElement element)
    {
        H = element.GetFloatAttribute("H");
        Team = element.GetIntAttribute("Team");
        W = element.GetFloatAttribute("W");
        X = element.GetFloatAttribute("X");
        Y = element.GetFloatAttribute("Y");
        ID = element.GetIntAttribute("ID", 0);
    }

    public virtual XElement Serialize()
    {
        XElement e = new(GetType().Name);

        e.SetAttributeValue("H", H.ToString());
        e.SetAttributeValue("Team", Team.ToString());
        e.SetAttributeValue("W", W.ToString());
        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());
        if(ID != 0)
            e.SetAttributeValue("ID", ID.ToString());

        return e;
    }

    public virtual void DrawOn<TTexture>
    (ICanvas<TTexture> canvas, GlobalRenderData rd, RenderSettings rs, Transform t, double time)
        where TTexture : ITexture
    {
        if(!ShouldShow(rs)) return;

        if(Team >= rs.ColorVolumeTeam.Length)
            throw new ArgumentOutOfRangeException($"Volume has team {Team}, which is larger than max available volume team color {rs.ColorVolumeTeam.Length-1}");

        canvas.DrawRect(X, Y, W, H, true, rs.ColorVolumeTeam[Team], t, DrawPriorityEnum.VOLUMES);
    }

    public abstract bool ShouldShow(RenderSettings rs);
}
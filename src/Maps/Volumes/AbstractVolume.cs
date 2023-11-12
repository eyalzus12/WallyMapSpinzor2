using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractVolume : IDeserializable, ISerializable, IDrawable
{
    //yes, brawlhalla defines those as ints.
    public int X{get; set;}
    public int Y{get; set;}
    public int W{get; set;}
    public int H{get; set;}
    public int Team{get; set;}
    public int ID{get; set;}
    
    public virtual void Deserialize(XElement element)
    {
        X = element.GetIntAttribute("X");
        Y = element.GetIntAttribute("Y");
        W = element.GetIntAttribute("W");
        H = element.GetIntAttribute("H");
        Team = element.GetIntAttribute("Team");
        ID = element.GetIntAttribute("ID", 0);
    }

    public virtual XElement Serialize()
    {
        XElement e = new(GetType().Name);

        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());
        e.SetAttributeValue("W", W.ToString());
        e.SetAttributeValue("H", H.ToString());
        e.SetAttributeValue("Team", Team.ToString());
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
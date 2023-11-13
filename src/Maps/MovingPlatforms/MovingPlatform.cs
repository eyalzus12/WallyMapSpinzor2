using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class MovingPlatform : IDeserializable, ISerializable, IDrawable
{
    public double X{get; set;}
    public double Y{get; set;}
    public string PlatID{get; set;} = null!;
    public Animation Animation{get; set;} = null!;
    public List<Platform> Platforms{get; set;} = null!;

    public void Deserialize(XElement element)
    {
        X = element.GetFloatAttribute("X", 0);
        Y = element.GetFloatAttribute("Y", 0);
        PlatID = element.GetAttribute("PlatID");
        //Animation is always supposed to exist
        //The game technically supports it not existing
        //In which case the moving platform doesn't exist
        Animation = element.DeserializeChildOfType<Animation>()!;
        Platforms = element.DeserializeChildrenOfType<Platform>();
    }

    public XElement Serialize()
    {
        XElement e = new("MovingPlatform");

        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());
        e.SetAttributeValue("PlatID", PlatID);
        e.Add(Animation.Serialize());
        foreach(Platform p in Platforms)
            e.Add(p.Serialize());

        return e;
    }

    public void DrawOn<TTexture>
    (ICanvas<TTexture> canvas, GlobalRenderData rd, RenderSettings rs, Transform t, double time) 
        where TTexture : ITexture
    {
        rd.PlatIDDict ??= new();
        (double _X, double _Y) = Animation.GetOffset(rd, time);
        //for some reason, dynamics need the first keyframe position of the animation removed
        (double _AX, double _AY) = Animation.KeyFrames[0].GetPosition();
        rd.PlatIDDict[PlatID] = (_X - _AX, _Y - _AY);

        Transform tt = t * Transform.CreateTranslate(X + _X, Y + _Y);
        foreach(Platform p in Platforms)
            p.DrawOn(canvas, rd, rs, tt, time);
    }
}
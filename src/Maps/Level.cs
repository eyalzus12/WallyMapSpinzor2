using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Level : IDeserializable, ISerializable, IDrawable
{
    public LevelDesc Desc{get; set;}
    public LevelType Type{get; set;}
    public HashSet<string> Playlists{get; set;} = null!;

    public Level(LevelDesc ld, LevelTypes lt, LevelSetTypes lst)
    {
        Desc = ld;
        Type = lt.Levels.Find(l => l.LevelName == Desc.LevelName) ?? new();
        Playlists = lst.Playlists
            .FindAll(l => l.LevelTypes.Contains(Desc.LevelName))
            .ConvertAll(l => l.LevelSetName)
            .ToHashSet();
    }

    public void SetLevelName(string name)
    {
        Desc.LevelName = name; 
        Type.LevelName = name;
    }

    public void Deserialize(XElement e)
    {
        Desc = e.DeserializeChildOfType<LevelDesc>() ?? new();
        Type = e.DeserializeChildOfType<LevelType>() ?? new();
        Playlists = e.Element("Playlists")?.Value.Split(",").ToHashSet() ?? new();
    }

    public void Serialize(XElement e)
    {
        e.Add(Desc.SerializeToXElement());
        e.Add(Type.SerializeToXElement());
        e.Add(new XElement("Playlists", string.Join(",", Playlists)));
    }

    public void DrawOn<TTexture>
    (ICanvas<TTexture> c, GlobalRenderData rd, RenderSettings rs, Transform t, double time) 
        where TTexture : ITexture
    {
        Desc.DrawOn(c, rd, rs, t, time);

        if(!rs.ShowKillBounds) return;
        double killX = Desc.CameraBounds.X - Type.LeftKill ?? 0;
        double killY = Desc.CameraBounds.Y - Type.TopKill ?? 0;
        double killW = Desc.CameraBounds.W + Desc.CameraBounds.X + Type.RightKill + Type.LeftKill ?? 0;
        double killH = Desc.CameraBounds.H + Desc.CameraBounds.Y + Type.BottomKill + Type.TopKill ?? 0;
        
        c.DrawRect(killX, killY, killW, killH, false, rs.ColorKillBounds, t, DrawPriorityEnum.DATA);
    }
}
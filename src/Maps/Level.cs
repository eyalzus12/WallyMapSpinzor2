using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Level : IDeserializable, ISerializable, IDrawable
{
    //saved so we can update the playlists
    private LevelSetTypes _types;

    public LevelDesc Desc{get; set;}
    public LevelType Type{get; set;}
    public HashSet<string> Playlists{get; set;} = null!;

    public Level(LevelDesc ld, LevelTypes lt, LevelSetTypes lst)
    {
        _types = lst;

        Desc = ld;
        Type = lt.Levels
            .Where(l => l.LevelName == Desc.LevelName)
            .FirstOrDefault() ?? throw new KeyNotFoundException($"Cannot find LevelType {Desc.LevelName} in given LevelTypes");
        Playlists = lst.Playlists
            .Where(l => l.LevelTypes.Contains(Desc.LevelName))
            .Select(l => l.LevelSetName)
            .ToHashSet();
    }

    public void SetLevelName(string name)
    {
        //update in playlists
        foreach(LevelSetType lst in _types.Playlists)
        {
            if(!Playlists.Contains(lst.LevelSetName))
                continue;
            int idx = lst.LevelTypes.IndexOf(Desc.LevelName);
            lst.LevelTypes[idx] = name;
        }
        //update desc and type
        Desc.LevelName = name; 
        Type.LevelName = name;
    }

    public void Deserialize(XElement e)
    {
        Desc = e.DeserializeChildOfType<LevelDesc>() ?? throw new ArgumentException("Given XML file does not contain a LevelDesc element. Invalid save format.");
        Type = e.DeserializeChildOfType<LevelType>() ?? throw new ArgumentException("Given XML file does not contain a LevelType element. Invalid save format.");
        Playlists = e.Element("Playlists")?.Value.Split(",").ToHashSet() ?? throw new ArgumentException("Given XML file does not contain a Playlists element. Invalid save format.");
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
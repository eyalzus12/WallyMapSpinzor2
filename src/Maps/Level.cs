using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

// Not a real brawlhalla object. Just a way for us to store all of the data in the same place.
public class Level : IDeserializable, ISerializable, IDrawable
{
    public LevelDesc Desc { get; set; }
    public LevelType? Type { get; set; }
    public HashSet<string> Playlists { get; set; } = null!;

    public Level(LevelDesc ld, LevelTypes lt, LevelSetTypes lst)
    {
        Desc = ld;
        Type = lt.Levels
            .Where(l => l.LevelName == Desc.LevelName)
            .FirstOrDefault();// ?? throw new KeyNotFoundException($"Cannot find LevelType {Desc.LevelName} in given LevelTypes");
        Playlists = lst.Playlists
            .Where(l => l.LevelTypes.Contains(Desc.LevelName))
            .Select(l => l.LevelSetName)
            .ToHashSet();
    }

    public void SetLevelName(LevelSetTypes types, string name)
    {
        //update in playlists
        foreach (LevelSetType lst in types.Playlists)
        {
            if (!Playlists.Contains(lst.LevelSetName))
                continue;
            int idx = Array.FindIndex(lst.LevelTypes, l => l == Desc.LevelName);
            if (idx != -1)
                lst.LevelTypes[idx] = name;
            else
                lst.LevelTypes = [.. lst.LevelTypes, name];
        }
        //update desc and type
        Desc.LevelName = name;
        if (Type is not null) Type.LevelName = name;
    }

    public void Deserialize(XElement e)
    {
        Desc = e.DeserializeChildOfType<LevelDesc>() ?? throw new ArgumentException("Given XML file does not contain a LevelDesc element. Invalid save format.");
        Type = e.DeserializeChildOfType<LevelType>();// ?? throw new ArgumentException("Given XML file does not contain a LevelType element. Invalid save format.");
        Playlists = e.GetElementValue("Playlists")?.Split(",").ToHashSet() ?? throw new ArgumentException("Given XML file does not contain a Playlists element. Invalid save format.");
    }

    public void Serialize(XElement e)
    {
        e.AddSerialized(Desc);
        if (Type is not null) e.AddSerialized(Type);
        e.Add(new XElement("Playlists", string.Join(",", Playlists)));
    }

    public void DrawOn(ICanvas canvas, Transform trans, RenderConfig config, RenderContext context, RenderState state)
    {
        Desc.DrawOn(canvas, trans, config, context, state);

        if (Type is null || !config.ShowKillBounds) return;
        double killX = Desc.CameraBounds.X - Type.LeftKill ?? 0;
        double killY = Desc.CameraBounds.Y - Type.TopKill ?? 0;
        double killW = Desc.CameraBounds.W + Type.RightKill + Type.LeftKill ?? 0;
        double killH = Desc.CameraBounds.H + Type.BottomKill + Type.TopKill ?? 0;

        canvas.DrawRect(killX, killY, killW, killH, false, config.ColorKillBounds, trans, DrawPriorityEnum.DATA, this);
    }
}
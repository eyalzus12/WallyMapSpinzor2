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
    public HashSet<string> Playlists { get; set; }

    // this exists to be able to use Deserialize<T>
    public Level() { Desc = null!; Playlists = []; }

    public Level(LevelDesc ld, LevelType? lt, HashSet<string> playlists) =>
        (Desc, Type, Playlists) = lt is not null && ld.LevelName != lt.LevelName
            ? throw new ArgumentException("LevelName's of given LevelDesc and LevelType don't match")
            : (ld, lt, playlists);

    public Level(LevelDesc ld, LevelTypes lt, LevelSetTypes lst)
    {
        Desc = ld;
        Type = Array.Find(lt.Levels, l => l.LevelName == Desc.LevelName);
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
        Type = e.DeserializeChildOfType<LevelType>();
        Playlists = e.GetElementOrNull("Playlists")?.Split(",").ToHashSet() ?? throw new ArgumentException("Given XML file does not contain a Playlists element. Invalid save format.");
    }

    public void Serialize(XElement e)
    {
        e.AddSerialized(Desc);
        if (Type is not null) e.AddSerialized(Type);
        e.Add(new XElement("Playlists", string.Join(",", Playlists)));
    }

    public void DrawOn(ICanvas canvas, Transform trans, RenderConfig config, RenderContext context, RenderState state)
    {
        context.ExtraStartFrame = Type?.StartFrame;
        Desc.DrawOn(canvas, trans, config, context, state);
        if (Type is null) return;

        double killX = Desc.CameraBounds.X - Type.LeftKill ?? 0;
        double killY = Desc.CameraBounds.Y - Type.TopKill ?? 0;
        double killW = Desc.CameraBounds.W + Type.RightKill + Type.LeftKill ?? 0;
        double killH = Desc.CameraBounds.H + Type.BottomKill + Type.TopKill ?? 0;

        if (config.ShowKillBounds)
        {
            canvas.DrawRect(killX, killY, killW, killH, false, config.ColorKillBounds, trans, DrawPriorityEnum.DATA, this);
        }

        if (config.ShowBotPanicLine)
        {
            double finalPanicLine = Math.Max(
                Type.AIPanicLine ?? 0,
                Desc.NavNodes
                    .Where(n => n.Type == NavNodeTypeEnum.W || n.Type == NavNodeTypeEnum.A)
                    .Select(n => n.Y)
                    .DefaultIfEmpty(double.NegativeInfinity)
                    .Max()
            );

            canvas.DrawLine(killX, finalPanicLine, killX + killW, finalPanicLine, config.ColorBotPanicLine, Transform.IDENTITY, DrawPriorityEnum.NAVLINE, this);
        }

        if (config.ShowBotGroundLine)
        {
            double finalGroundLine = Type.AIGroundLine ?? 0;

            Position len = new(0, 0), pos = new(0, 0);
            Position? NULL = null;
            foreach (NavNode n in Desc.NavNodes)
            {
                if (n.Type != NavNodeTypeEnum._ && n.Type != NavNodeTypeEnum.L) continue;
                if (n.Y <= finalGroundLine - 150) continue;
                len = len with { Y = 150 };
                // TODO: need to integrate dynamic collisions?
                // TODO: need to correctly handle Anchors
                AbstractCollision? col = BrawlhallaMath.Raycast(Desc.Collisions, 0, n.X, n.Y, ref len, ref pos, null, ref NULL, ref NULL, CollisionTypeFlags.HARD | CollisionTypeFlags.SOFT, 0);

                if (col is not null && col.ToY > finalGroundLine)
                {
                    finalGroundLine = col.ToY;
                }
                else
                {
                    finalGroundLine = n.Y;
                }
            }

            canvas.DrawLine(killX, finalGroundLine, killX + killW, finalGroundLine, config.ColorBotGroundLine, Transform.IDENTITY, DrawPriorityEnum.NAVLINE, this);
        }
    }
}
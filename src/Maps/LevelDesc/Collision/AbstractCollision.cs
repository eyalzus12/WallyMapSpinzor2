using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractCollision : IDeserializable, ISerializable, IDrawable
{
    public enum FlagEnum
    {
        DEFAULT = 0,
        WATER = 1,
        WATERBLUE = 2,
        ICE = 3,
        METAL = 4,
        WOOD = 5,
        PUDDLE = 6,
        ROPEBRIDGE = 7,
        SAND = 8
    }

    //brawlhalla doesn't define anything for this lol
    public enum ColorFlagEnum
    {
        DEFAULT = 0
    }

    public string? TauntEvent{get; set;}
    public int Team{get; set;}
    public double? AnchorX{get; set;}
    public double? AnchorY{get; set;}
    public double NormalX{get; set;}
    public double NormalY{get; set;}
    public double X1{get; set;}
    public double X2{get; set;}
    public double Y1{get; set;}
    public double Y2{get; set;}
    public FlagEnum? Flag{get; set;}
    public ColorFlagEnum? ColorFlag{get; set;}
    
    public virtual void Deserialize(XElement e)
    {
        TauntEvent = e.GetNullableAttribute("TauntEvent");
        
        Team = e.GetIntAttribute("Team", 0);

        //brawlhalla requires both attributes to exist for an anchor
        AnchorX = AnchorY = null;
        if(e.HasAttribute("AnchorX") && e.HasAttribute("AnchorY"))
        {
            AnchorX = e.GetFloatAttribute("AnchorX");
            AnchorY = e.GetFloatAttribute("AnchorY");
        }

        //a collision with an anchor can't be a pressure plate or have a normal
        //but we don't bother implementing that
        NormalX = e.GetFloatAttribute("NormalX", 0);
        NormalY = e.GetFloatAttribute("NormalY", 0);

        X2 = X1 = 0;
        if(e.HasAttribute("X"))
        {
            X2 = X1 = e.GetFloatAttribute("X");
        }
        else if(e.HasAttribute("X1") && e.HasAttribute("X2"))
        {
            X1 = e.GetFloatAttribute("X1");
            X2 = e.GetFloatAttribute("X2");
        }
        
        Y2 = Y1 = 0;
        if(e.HasAttribute("Y"))
        {
            Y2 = Y1 = e.GetFloatAttribute("Y");
        }
        else if(e.HasAttribute("Y1") && e.HasAttribute("Y2"))
        {
            Y1 = e.GetFloatAttribute("Y1");
            Y2 = e.GetFloatAttribute("Y2");
        }

        Flag =
            e.HasAttribute("Flag")
            ?Enum.TryParse(e.GetAttribute("Flag").ToUpper(), out FlagEnum _Flag)
                    ?_Flag
                    :FlagEnum.DEFAULT
            :null;
        
        ColorFlag  =
            e.HasAttribute("ColorFlag")
            ?Enum.TryParse(e.GetAttribute("ColorFlag")?.ToUpper(), out ColorFlagEnum _ColorFlag)
                ?_ColorFlag
                :ColorFlagEnum.DEFAULT
            :null;
    }

    public virtual void Serialize(XElement e)
    {
        if(TauntEvent is not null)
            e.SetAttributeValue("TauntEvent", TauntEvent);
        
        if(Team != 0)
            e.SetAttributeValue("Team", Team.ToString());

        if(AnchorX is not null && AnchorY is not null)
        {
            e.SetAttributeValue("AnchorX", AnchorX.ToString());
            e.SetAttributeValue("AnchorY", AnchorY.ToString());
        }

        if(NormalX != 0)
            e.SetAttributeValue("NormalX", NormalX.ToString());
        if(NormalY != 0)
            e.SetAttributeValue("NormalY", NormalY.ToString());
        
        if(X1 == X2)
        {
            e.SetAttributeValue("X", X1.ToString());
        }
        else
        {
            e.SetAttributeValue("X1", X1.ToString());
            e.SetAttributeValue("X2", X2.ToString());
        }

        if(Y1 == Y2)
        {
            e.SetAttributeValue("Y", Y1.ToString());
        }
        else
        {
            e.SetAttributeValue("Y1", Y1.ToString());
            e.SetAttributeValue("Y2", Y2.ToString());
        }
        
        if(Flag is not null)
            e.SetAttributeValue("Flag", Flag?.ToString().ToLower());
        
        if(ColorFlag is not null)
            e.SetAttributeValue("ColorFlag", ColorFlag?.ToString().ToLower());
    }
    
    private List<(double, double)>? _curve = null;
    
    public void CalculateCurve(double XOff, double YOff)
    {
        //no anchor
        if(AnchorX is null || AnchorY is null)
        {
            _curve = null;
            return;
        }
        
        //already calculated anchor
        if(_curve is not null)
            return;
        
        _curve = BrawlhallaMath.CollisionQuad(X1, Y1, X2, Y2, (AnchorX??0) - XOff, (AnchorY??0) - YOff).ToList();
    }

    public virtual void DrawOn<T>(ICanvas<T> canvas, RenderConfig config, Transform trans, TimeSpan time, RenderData data)
        where T : ITexture
    {
        if(!config.ShowCollision) return;
        
        if((AnchorX is not null && AnchorY is not null) && _curve is null)
            throw new InvalidOperationException("Collision has non null anchor, but cached curve is null. Make sure CalculateCurve is called.");
        if((AnchorX is null || AnchorY is null) && _curve is not null)
            throw new InvalidOperationException("Collision has null anchor, but cached curve is non null. Make sure CalculateCurve is called.");

        (double startX, double startY) = (X1, Y1);

        IEnumerator<(double,double)>? enumer = _curve?.GetEnumerator();
        
        bool finished = false;
        //we use a hack here to get anchors to work
        while((enumer?.MoveNext() ?? false) || !finished)
        {
            (double nextX, double nextY) = enumer?.Current ?? (X2, Y2);
            //draw current line
            if(Team == 0)
                canvas.DrawLine(startX, startY, nextX, nextY, GetColor(config), trans, DrawPriorityEnum.DATA);
            else
            {
                if(Team-1 >= config.ColorCollisionTeam.Length)
                    throw new ArgumentOutOfRangeException($"Collision has team {Team} which is larger than max available collision team color {config.ColorCollisionTeam.Length}");
                canvas.DrawLineMultiColor(
                        startX, startY, nextX, nextY,
                        new[]{config.ColorCollisionTeam[Team-1], GetColor(config), config.ColorCollisionTeam[Team-1]},
                        trans, DrawPriorityEnum.DATA
                    );
            }
            
            //draw normal line
            if(config.ShowCollisionNormalOverride)
            {
                if(NormalX != 0 || NormalY != 0)
                {
                    //Normal overrides are NOT NORMALIZED
                    //This detail only affects SpongebobMap
                    double normalStartX = (startX+nextX)/2;
                    double normalStartY = (startY+nextY)/2;
                    double normalEndX = normalStartX + config.LengthCollisionNormal * NormalX;
                    double normalEndY = normalStartY + config.LengthCollisionNormal * NormalY;

                    canvas.DrawLine(
                        normalStartX, normalStartY, normalEndX, normalEndY,
                        config.ColorCollisionNormal, trans, DrawPriorityEnum.DATA
                    );
                }
                //Otherwise normals are auto generated
                //However this logic requires raycasting
                //We'll add this later if we need it
            }
            
            (startX, startY) = (nextX, nextY);
            finished = true;
        }
        
        //mandate calling CalculateCurve
        _curve = null;
    }

    public abstract Color GetColor(RenderConfig rs);

    [Flags]
    public enum CollisionTypeEnum
    {
        HARD = 1 << 0,
        SOFT = 1 << 1,
        TRIGGER = 1 << 2,
        STICKY = 1 << 3,
        NO_SLIDE = 1 << 4,
        ITEM_IGNORE = 1 << 5,
        BOUNCY = 1 << 6,
        GAMEMODE = 1 << 7,
        PRESSURE_PLATE = 1 << 8
    }

    public abstract CollisionTypeEnum CollisionType{get;}
}
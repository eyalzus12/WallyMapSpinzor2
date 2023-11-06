using System.Net.Http.Headers;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractCollision : IDeserializable, ISerializable
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

    public double X1{get; set;}
    public double X2{get; set;}
    public double Y1{get; set;}
    public double Y2{get; set;}
    public string? TauntEvent{get; set;}
    public FlagEnum? Flag{get; set;}
    public ColorFlagEnum? ColorFlag{get; set;}
    public double? AnchorX{get; set;}
    public double? AnchorY{get; set;}
    public double NormalX{get; set;}
    public double NormalY{get; set;}
    public int Team{get; set;}
    
    public virtual void Deserialize(XElement element)
    {
        X2 = X1 = 0;
        if(element.HasAttribute("X"))
        {
            X2 = X1 = element.GetFloatAttribute("X");
        }
        else if(element.HasAttribute("X1") && element.HasAttribute("X2"))
        {
            X1 = element.GetFloatAttribute("X1");
            X2 = element.GetFloatAttribute("X2");
        }
        
        Y2 = Y1 = 0;
        if(element.HasAttribute("Y"))
        {
            Y2 = Y1 = element.GetFloatAttribute("Y");
        }
        else if(element.HasAttribute("Y1") && element.HasAttribute("Y2"))
        {
            Y1 = element.GetFloatAttribute("Y1");
            Y2 = element.GetFloatAttribute("Y2");
        }
        
        //swap
        //im not 100% sure why brawlhalla does this
        if(X1 > X2) (X2, X1) = (X1, X2);

        TauntEvent = element.GetNullableAttribute("TauntEvent");

        Flag =
            element.HasAttribute("Flag")
            ?Enum.TryParse(element.GetAttribute("Flag").ToUpper(), out FlagEnum _Flag)
                    ?_Flag
                    :FlagEnum.DEFAULT
            :null;
        
        ColorFlag  =
            element.HasAttribute("ColorFlag")
            ?Enum.TryParse(element.GetAttribute("ColorFlag")?.ToUpper(), out ColorFlagEnum _ColorFlag)
                ?_ColorFlag
                :ColorFlagEnum.DEFAULT
            :null;
        
        //brawlhalla requires both attributes to exist for an anchor
        AnchorX = AnchorY = null;
        if(element.HasAttribute("AnchorX") && element.HasAttribute("AnchorY"))
        {
            AnchorX = element.GetFloatAttribute("AnchorX");
            AnchorY = element.GetFloatAttribute("AnchorY");
        }

        //a collision with an anchor can't be a pressure plate or have a normal
        //but we don't bother implementing that
        NormalX = element.GetFloatAttribute("NormalX", 0);
        NormalY = element.GetFloatAttribute("NormalY", 0);

        Team = element.GetIntAttribute("Team", 0);
    }

    public virtual XElement Serialize()
    {
        XElement e = new(GetType().Name);

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
            e.SetAttributeValue("Y", X1.ToString());
        }
        else
        {
            e.SetAttributeValue("Y1", Y1.ToString());
            e.SetAttributeValue("Y2", Y2.ToString());
        }

        if(TauntEvent is not null)
            e.SetAttributeValue("TauntEvent", TauntEvent);
        
        if(Flag is not null)
            e.SetAttributeValue("Flag", Flag?.ToString().ToLower());
        
        if(ColorFlag is not null)
            e.SetAttributeValue("ColorFlag", ColorFlag?.ToString().ToLower());
        
        if(AnchorX is not null && AnchorY is not null)
        {
            e.SetAttributeValue("AnchorX", AnchorX.ToString());
            e.SetAttributeValue("AnchorY", AnchorY.ToString());
        }

        if(NormalX != 0)
            e.SetAttributeValue("NormalX", NormalX.ToString());
        if(NormalY != 0)
            e.SetAttributeValue("NormalY", NormalY.ToString());
        
        if(Team != 0)
            e.SetAttributeValue("Team", Team.ToString());

        return e;
    }
}
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractCollision : IDeserializable
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

        //im not 100% sure why brawlhalla does this
        if(X1 > X2)
        {
            var temp = X1;
            X1 = X2;
            X2 = temp;
        }

        TauntEvent = element.GetNullableAttribute("TauntEvent");

        Flag = null;
        if(element.HasAttribute("Flag"))
        {
            FlagEnum _Flag;
            if(!Enum.TryParse<FlagEnum>(element.GetAttribute("Flag").ToUpper(), out _Flag))
                Flag = 0;
            else
                Flag = _Flag;
        }
        
        ColorFlag = null;
        if(element.HasAttribute("ColorFlag"))
        {
            ColorFlagEnum _ColorFlag;
            if(!Enum.TryParse<ColorFlagEnum>(element.GetNullableAttribute("ColorFlag")?.ToUpper(), out _ColorFlag))
                ColorFlag = 0;
            else
                ColorFlag = _ColorFlag;
        }
        
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
}
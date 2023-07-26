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

    public enum ColorFlagEnum
    {
        DEFAULT = 0
    }

    public double X1{get; set;}
    public double X2{get; set;}
    public double Y1{get; set;}
    public double Y2{get; set;}
    public string? TauntEvent{get; set;}
    public FlagEnum Flag{get; set;}
    public ColorFlagEnum ColorFlag{get; set;}
    public double? AnchorX{get; set;}
    public double? AnchorY{get; set;}
    public double NormalX{get; set;}
    public double NormalY{get; set;}
    public int Team{get; set;}
    public virtual void Deserialize(XElement element)
    {
        double X = element.GetFloatAttribute("X", 0);
        X1 = element.GetFloatAttribute("X1", X);
        X2 = element.GetFloatAttribute("X2", X);
        double Y = element.GetFloatAttribute("Y", 0);
        Y1 = element.GetFloatAttribute("Y1", Y);
        Y2 = element.GetFloatAttribute("Y2", Y);

        //im not 100% sure why brawlhalla does this
        if(X1 > X2)
        {
            var temp = X1;
            X1 = X2;
            X2 = temp;
        }

        TauntEvent = element.GetNullableAttribute("TauntEvent");

        FlagEnum _Flag;
        if(!Enum.TryParse<FlagEnum>(element.GetNullableAttribute("Flag")?.ToUpper(), out _Flag))
            Flag = 0;
        else 
            Flag = _Flag;
        
        ColorFlagEnum _ColorFlag;
        if(!Enum.TryParse<ColorFlagEnum>(element.GetNullableAttribute("ColorFlag")?.ToUpper(), out _ColorFlag))
            ColorFlag = 0;
        else 
            ColorFlag = _ColorFlag;
        
        //brawlhalla requires both attributes to exist for an anchor
        //NOTE: a collision with an anchor can't be a pressure plate or have a normal
        //NOTE: we don't make note of that here
        if(element.HasAttribute("AnchorX") && element.HasAttribute("AnchorY"))
        {
            AnchorX = element.GetNullableFloatAttribute("AnchorX");
            AnchorY = element.GetNullableFloatAttribute("AnchorY");
        }
        NormalX = element.GetFloatAttribute("NormalX", 0);
        NormalY = element.GetFloatAttribute("NormalY", 0);
        Team = element.GetIntAttribute("Team", 0);
    }
}
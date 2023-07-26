using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class TeamScoreboard : IDeserializable
{
    //yes, brawlhalla defines those as ints
    public int RedTeamX{get; set;}
    public int RedTeamY{get; set;}
    public int Y{get; set;}
    public int DoubleDigitsOnesX{get; set;}
    public int DoubleDigitsTensX{get; set;}
    public double DoubleDigitsScale{get; set;}
    public double DoubleDigitsY{get; set;}
    public string RedDigitFont{get; set;} = "";
    public string BlueDigitFont{get; set;} = "";

    public void Deserialize(XElement element)
    {
        RedTeamX = element.GetIntAttribute("RedTeamX", 0);
        RedTeamY = element.GetIntAttribute("RedTeamY", 0);
        Y = element.GetIntAttribute("Y", 0);
        DoubleDigitsOnesX = element.GetIntAttribute("DoubleDigitsOnesX", 0);
        DoubleDigitsTensX = element.GetIntAttribute("DoubleDigitsTensX", 0);
        //yes, this actually defaults to 0
        DoubleDigitsScale = element.GetFloatAttribute("DoubleDigitsScale", 0);
        DoubleDigitsY = element.GetFloatAttribute("DoubleDigitsY", 0);
        RedDigitFont = element.GetAttribute("RedDigitFont");
        BlueDigitFont = element.GetAttribute("BlueDigitFont");
    }
}
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class TeamScoreboard : IDeserializable, ISerializable, IDrawable
{
    //yes, brawlhalla defines those as ints
    public int RedTeamX{get; set;}
    public int BlueTeamX{get; set;}
    public int Y{get; set;}
    public int DoubleDigitsOnesX{get; set;}
    public int DoubleDigitsTensX{get; set;}
    public double DoubleDigitsY{get; set;}
    public double DoubleDigitsScale{get; set;}
    public string RedDigitFont{get; set;} = null!;
    public string BlueDigitFont{get; set;} = null!;

    public void Deserialize(XElement e)
    {
        RedTeamX = e.GetIntAttribute("RedTeamX", 0);
        BlueTeamX = e.GetIntAttribute("BlueTeamX", 0);
        Y = e.GetIntAttribute("Y", 0);
        DoubleDigitsOnesX = e.GetIntAttribute("DoubleDigitsOnesX", 0);
        DoubleDigitsTensX = e.GetIntAttribute("DoubleDigitsTensX", 0);
        DoubleDigitsY = e.GetFloatAttribute("DoubleDigitsY", 0);
        //yes, this actually defaults to 0
        DoubleDigitsScale = e.GetFloatAttribute("DoubleDigitsScale", 0);
        RedDigitFont = e.GetAttribute("RedDigitFont");
        BlueDigitFont = e.GetAttribute("BlueDigitFont");
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("RedTeamX", RedTeamX.ToString());
        e.SetAttributeValue("BlueTeamX", BlueTeamX.ToString());
        e.SetAttributeValue("Y", Y.ToString());
        e.SetAttributeValue("DoubleDigitsOnesX", DoubleDigitsOnesX.ToString());
        e.SetAttributeValue("DoubleDigitsTensX", DoubleDigitsTensX.ToString());
        e.SetAttributeValue("DoubleDigitsY", DoubleDigitsY.ToString());
        e.SetAttributeValue("DoubleDigitsScale", DoubleDigitsScale.ToString());
        e.SetAttributeValue("RedDigitFont", RedDigitFont);
        e.SetAttributeValue("BlueDigitFont", BlueDigitFont);
    }

    public void DrawOn<TTexture>
    (ICanvas<TTexture> canvas, GlobalRenderData rd, RenderSettings rs, Transform t, TimeSpan time)
        where TTexture : ITexture
    {
        if(!rs.ShowAssets) return;

        //red
        if(rs.RedScore < 10)
        {
            string fontName = $"a_Digit{rs.RedScore}" + (RedDigitFont==""?"":"_") + RedDigitFont;
            TTexture texture = canvas.LoadTextureFromSWF("bones/Bones_GameModes.swf", fontName);
            canvas.DrawTexture(RedTeamX, Y, texture, t, DrawPriorityEnum.FOREGROUND);
        }
        else
        {
            int redOne = rs.RedScore % 10;
            int redTen = rs.RedScore / 10;
            if(redTen >= 10)
            {
                redOne = 9;
                redTen = 9;
            }

            string fontNameOnes = $"a_Digit{redOne}" + (RedDigitFont==""?"":"_") + RedDigitFont;
            string fontNameTens = $"a_Digit{redTen}" + (RedDigitFont==""?"":"_") + RedDigitFont;
            TTexture textureOnes = canvas.LoadTextureFromSWF("bones/Bones_GameModes.swf", fontNameOnes);
            TTexture textureTens = canvas.LoadTextureFromSWF("bones/Bones_GameModes.swf", fontNameTens);
            Transform tt = t * Transform.CreateScale(DoubleDigitsScale, DoubleDigitsScale);
            canvas.DrawTexture(RedTeamX + DoubleDigitsOnesX, DoubleDigitsY, textureOnes, tt, DrawPriorityEnum.FOREGROUND);
            canvas.DrawTexture(RedTeamX + DoubleDigitsTensX, DoubleDigitsY, textureTens, tt, DrawPriorityEnum.FOREGROUND);
        }

        //blue
        if(rs.BlueScore < 10)
        {
            string fontName = $"a_Digit{rs.BlueScore}" + (BlueDigitFont ==""?"":"_") + BlueDigitFont;
            TTexture texture = canvas.LoadTextureFromSWF("bones/Bones_GameModes.swf", fontName);
            canvas.DrawTexture(BlueTeamX, Y, texture, t, DrawPriorityEnum.FOREGROUND);
        }
        else
        {
            int blueOne = rs.BlueScore % 10;
            int blueTen = rs.BlueScore / 10;
            if(blueTen >= 10)
            {
                blueOne = 9;
                blueTen = 9;
            }

            string fontNameOnes = $"a_Digit{blueOne}" + (BlueDigitFont==""?"":"_") + BlueDigitFont;
            string fontNameTens = $"a_Digit{blueTen}" + (BlueDigitFont==""?"":"_") + BlueDigitFont;
            TTexture textureOnes = canvas.LoadTextureFromSWF("bones/Bones_GameModes.swf", fontNameOnes);
            TTexture textureTens = canvas.LoadTextureFromSWF("bones/Bones_GameModes.swf", fontNameTens);
            Transform tt = t * Transform.CreateScale(DoubleDigitsScale, DoubleDigitsScale);
            canvas.DrawTexture(BlueTeamX + DoubleDigitsOnesX, DoubleDigitsY, textureOnes, tt, DrawPriorityEnum.FOREGROUND);
            canvas.DrawTexture(BlueTeamX + DoubleDigitsTensX, DoubleDigitsY, textureTens, tt, DrawPriorityEnum.FOREGROUND);
        }
    }
}
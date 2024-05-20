using System;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class TeamScoreboard : IDeserializable, ISerializable, IDrawable
{
    private const string DIGIT_PREFIX = "a_Digit";

    //yes, brawlhalla defines those as ints
    public int RedTeamX { get; set; }
    public int BlueTeamX { get; set; }
    public int Y { get; set; }
    public int DoubleDigitsOnesX { get; set; }
    public int DoubleDigitsTensX { get; set; }
    public double DoubleDigitsY { get; set; }
    public double DoubleDigitsScale { get; set; }
    public string RedDigitFont { get; set; } = null!;
    public string BlueDigitFont { get; set; } = null!;

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
        e.SetAttributeValue("RedTeamX", RedTeamX);
        e.SetAttributeValue("BlueTeamX", BlueTeamX);
        e.SetAttributeValue("Y", Y);
        e.SetAttributeValue("DoubleDigitsOnesX", DoubleDigitsOnesX);
        e.SetAttributeValue("DoubleDigitsTensX", DoubleDigitsTensX);
        e.SetAttributeValue("DoubleDigitsY", DoubleDigitsY);
        e.SetAttributeValue("DoubleDigitsScale", DoubleDigitsScale);
        e.SetAttributeValue("RedDigitFont", RedDigitFont);
        e.SetAttributeValue("BlueDigitFont", BlueDigitFont);
    }

    public void DrawOn<T>(ICanvas<T> canvas, RenderConfig config, Transform trans, TimeSpan time, RenderData data)
        where T : ITexture
    {
        if (!config.ShowAssets) return;

        //red
        if (config.RedScore < 10)
        {
            string fontName = $"{DIGIT_PREFIX}{config.RedScore}" + (RedDigitFont == "" ? "" : "_") + RedDigitFont;
            Transform redOnesTrans = trans * Transform.CreateTranslate(RedTeamX, Y);
            canvas.DrawSwf(LevelDesc.GAMEMODE_BONES, fontName, 0, 0, 0, 1, redOnesTrans, DrawPriorityEnum.FOREGROUND, this);
        }
        else
        {
            int redOne = config.RedScore % 10;
            int redTen = config.RedScore / 10;
            if (redTen >= 10)
            {
                redOne = 9;
                redTen = 9;
            }

            string fontNameOnes = $"{DIGIT_PREFIX}{redOne}" + (RedDigitFont == "" ? "" : "_") + RedDigitFont;
            Transform redOnesTrans = trans * Transform.CreateFrom(x: RedTeamX + DoubleDigitsOnesX, y: DoubleDigitsY, scaleX: DoubleDigitsScale, scaleY: DoubleDigitsScale);
            canvas.DrawSwf(LevelDesc.GAMEMODE_BONES, fontNameOnes, 0, 0, 0, 1, redOnesTrans, DrawPriorityEnum.FOREGROUND, this);

            string fontNameTens = $"{DIGIT_PREFIX}{redTen}" + (RedDigitFont == "" ? "" : "_") + RedDigitFont;
            Transform redTensTrans = trans * Transform.CreateFrom(x: RedTeamX + DoubleDigitsTensX, y: DoubleDigitsY, scaleX: DoubleDigitsScale, scaleY: DoubleDigitsScale);
            canvas.DrawSwf(LevelDesc.GAMEMODE_BONES, fontNameTens, 0, 0, 0, 1, redTensTrans, DrawPriorityEnum.FOREGROUND, this);
        }

        //blue
        if (config.BlueScore < 10)
        {
            string fontName = $"{DIGIT_PREFIX}{config.BlueScore}" + (BlueDigitFont == "" ? "" : "_") + BlueDigitFont;
            Transform blueOnesTrans = trans * Transform.CreateTranslate(BlueTeamX, Y);
            canvas.DrawSwf(LevelDesc.GAMEMODE_BONES, fontName, 0, 0, 0, 1, blueOnesTrans, DrawPriorityEnum.FOREGROUND, this);
        }
        else
        {
            int blueOne = config.BlueScore % 10;
            int blueTen = config.BlueScore / 10;
            if (blueTen >= 10)
            {
                blueOne = 9;
                blueTen = 9;
            }

            string fontNameOnes = $"{DIGIT_PREFIX}{blueOne}" + (BlueDigitFont == "" ? "" : "_") + BlueDigitFont;
            Transform blueOnesTrans = trans * Transform.CreateFrom(x: BlueTeamX + DoubleDigitsOnesX, y: DoubleDigitsY, scaleX: DoubleDigitsScale, scaleY: DoubleDigitsScale);
            canvas.DrawSwf(LevelDesc.GAMEMODE_BONES, fontNameOnes, 0, 0, 0, 1, blueOnesTrans, DrawPriorityEnum.FOREGROUND, this);

            string fontNameTens = $"{DIGIT_PREFIX}{blueTen}" + (BlueDigitFont == "" ? "" : "_") + BlueDigitFont;
            Transform blueTensTrans = trans * Transform.CreateFrom(x: BlueTeamX + DoubleDigitsTensX, y: DoubleDigitsY, scaleX: DoubleDigitsScale, scaleY: DoubleDigitsScale);
            canvas.DrawSwf(LevelDesc.GAMEMODE_BONES, fontNameTens, 0, 0, 0, 1, blueTensTrans, DrawPriorityEnum.FOREGROUND, this);
        }
    }
}
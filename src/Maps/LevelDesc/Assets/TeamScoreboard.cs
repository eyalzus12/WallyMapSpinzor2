using System;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class TeamScoreboard : IDeserializable, ISerializable, IDrawable
{
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

    private static Gfx CreateGfx(string font) => new()
    {
        AnimFile = "Animation_GameModes.swf",
        AnimClass = "a__AnimationScore",
        AnimScale = 2,
        BaseAnim = "Ready",
        CustomArts = font == "" ? [] : [new CustomArt() { FileName = "Animation_GameModes.swf", Name = font }]
    };

    public void DrawOn(ICanvas canvas, RenderConfig config, Transform trans, TimeSpan time, RenderData data)
    {
        if (!config.ShowAssets) return;

        Gfx redGfx = CreateGfx(RedDigitFont);
        Gfx blueGfx = CreateGfx(BlueDigitFont);

        //red
        if (config.RedScore < 10)
        {
            Transform redOnesTrans = trans * Transform.CreateTranslate(RedTeamX, Y);
            canvas.DrawAnim(redGfx, $"{config.RedScore}", 0, redOnesTrans, DrawPriorityEnum.FOREGROUND, this);
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
            Transform redOnesTrans = trans * Transform.CreateFrom(x: RedTeamX + DoubleDigitsOnesX, y: DoubleDigitsY, scaleX: DoubleDigitsScale, scaleY: DoubleDigitsScale);
            canvas.DrawAnim(redGfx, $"{redOne}", 0, redOnesTrans, DrawPriorityEnum.FOREGROUND, this);
            Transform redTensTrans = trans * Transform.CreateFrom(x: RedTeamX + DoubleDigitsTensX, y: DoubleDigitsY, scaleX: DoubleDigitsScale, scaleY: DoubleDigitsScale);
            canvas.DrawAnim(redGfx, $"{redTen}", 0, redTensTrans, DrawPriorityEnum.FOREGROUND, this);
        }

        //blue
        if (config.BlueScore < 10)
        {
            Transform blueOnesTrans = trans * Transform.CreateTranslate(BlueTeamX, Y);
            canvas.DrawAnim(blueGfx, $"{config.BlueScore}", 0, blueOnesTrans, DrawPriorityEnum.FOREGROUND, this);
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
            Transform blueOnesTrans = trans * Transform.CreateFrom(x: BlueTeamX + DoubleDigitsOnesX, y: DoubleDigitsY, scaleX: DoubleDigitsScale, scaleY: DoubleDigitsScale);
            canvas.DrawAnim(blueGfx, $"{blueOne}", 0, blueOnesTrans, DrawPriorityEnum.FOREGROUND, this);
            Transform blueTensTrans = trans * Transform.CreateFrom(x: BlueTeamX + DoubleDigitsTensX, y: DoubleDigitsY, scaleX: DoubleDigitsScale, scaleY: DoubleDigitsScale);
            canvas.DrawAnim(blueGfx, $"{blueTen}", 0, blueTensTrans, DrawPriorityEnum.FOREGROUND, this);
        }
    }
}
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

    public void DrawOn<T>(ICanvas<T> canvas, RenderConfig config, Transform trans, TimeSpan time, RenderData data)
        where T : ITexture
    {
        if (!config.ShowAssets) return;

        //red
        if (config.RedScore < 10)
        {
            string fontName = $"{DIGIT_PREFIX}{config.RedScore}" + (RedDigitFont == "" ? "" : "_") + RedDigitFont;
            T texture = canvas.LoadTextureFromSWF(LevelDesc.GAMEMODE_BONES, fontName);
            Transform redOnesTrans = Transform.CreateTranslate(RedTeamX, Y);
            canvas.DrawTexture(0, 0, texture, redOnesTrans, DrawPriorityEnum.FOREGROUND);
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
            T textureOnes = canvas.LoadTextureFromSWF(LevelDesc.GAMEMODE_BONES, fontNameOnes);
            Transform redOnesTrans = trans * Transform.CreateFrom(x: RedTeamX + DoubleDigitsOnesX, y: DoubleDigitsY, scaleX: DoubleDigitsScale, scaleY: DoubleDigitsScale);
            canvas.DrawTexture(0, 0, textureOnes, redOnesTrans, DrawPriorityEnum.FOREGROUND);
            
            string fontNameTens = $"{DIGIT_PREFIX}{redTen}" + (RedDigitFont == "" ? "" : "_") + RedDigitFont;
            T textureTens = canvas.LoadTextureFromSWF(LevelDesc.GAMEMODE_BONES, fontNameTens);
            Transform redTensTrans = trans * Transform.CreateFrom(x: RedTeamX + DoubleDigitsTensX, y: DoubleDigitsY, scaleX: DoubleDigitsScale, scaleY: DoubleDigitsScale);
            canvas.DrawTexture(0, 0, textureTens, redTensTrans, DrawPriorityEnum.FOREGROUND);
        }

        //blue
        if (config.BlueScore < 10)
        {
            string fontName = $"{DIGIT_PREFIX}{config.BlueScore}" + (BlueDigitFont == "" ? "" : "_") + BlueDigitFont;
            T texture = canvas.LoadTextureFromSWF(LevelDesc.GAMEMODE_BONES, fontName);
            Transform blueOnesTrans = trans * Transform.CreateTranslate(BlueTeamX, Y);
            canvas.DrawTexture(0, 0, texture, blueOnesTrans, DrawPriorityEnum.FOREGROUND);
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
            T textureOnes = canvas.LoadTextureFromSWF(LevelDesc.GAMEMODE_BONES, fontNameOnes);
            Transform blueOnesTrans = trans * Transform.CreateFrom(x: BlueTeamX + DoubleDigitsOnesX, y: DoubleDigitsY, scaleX: DoubleDigitsScale, scaleY: DoubleDigitsScale);
            canvas.DrawTexture(0, 0, textureOnes, blueOnesTrans, DrawPriorityEnum.FOREGROUND);
            
            string fontNameTens = $"{DIGIT_PREFIX}{blueTen}" + (BlueDigitFont == "" ? "" : "_") + BlueDigitFont;
            T textureTens = canvas.LoadTextureFromSWF(LevelDesc.GAMEMODE_BONES, fontNameTens);
            Transform blueTensTrans = trans * Transform.CreateFrom(x: BlueTeamX + DoubleDigitsTensX, y: DoubleDigitsY, scaleX: DoubleDigitsScale, scaleY: DoubleDigitsScale);
            canvas.DrawTexture(0, 0, textureTens, blueTensTrans, DrawPriorityEnum.FOREGROUND);
        }
    }
}

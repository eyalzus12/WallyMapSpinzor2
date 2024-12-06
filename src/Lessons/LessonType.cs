using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LessonType : IDeserializable, ISerializable
{
    private const string TEMPLATE_LESSON_TYPE = "Template";

    public string LessonName { get; set; } = null!;
    public uint LessonID { get; set; }
    public string? Prerequisite { get; set; }
    public string? TitleKey { get; set; }
    public string? DescriptionKey { get; set; }
    public string? Category { get; set; }
    public string? LevelType { get; set; }
    public int TimeLimit { get; set; } // should be multiple of 15 and <= 300
    public WinConditionEnum WinCondition { get; set; }
    public uint GoldReward { get; set; }
    public ComboModeEnum ComboMode { get; set; }
    public int Difficulty { get; set; }
    public int Priority { get; set; }
    public string? PromptStrings { get; set; }
    // not in template. so placed at end arbitrarly
    public string[]? CustomDeathMessage { get; set; }
    public string? IntroCutscene { get; set; }
    public int MessagePosition_X { get; set; }
    public int MessagePosition_Y { get; set; }
    public uint NodePathLength { get; set; }
    public uint NodeSpread { get; set; }

    public LessonEntityType[] Entities { get; set; } = []; // exactly one can be Player, and exactly one can be Sensei
    public LessonItem[] Items { get; set; } = [];
    public LessonMarker[] Markers { get; set; } = [];
    public LessonMessageTrigger[] MessageTriggers { get; set; } = [];
    public LessonWorldHotkey[] WorldHotkeys { get; set; } = [];

    public void Deserialize(XElement e)
    {
        LessonName = e.GetAttribute("LessonName");
        LessonID = e.GetUIntElement("LessonID");
        Prerequisite = e.GetElementOrNull("Prerequisite");
        TitleKey = e.GetElementOrNull("TitleKey");
        DescriptionKey = e.GetElementOrNull("DescriptionKey");
        Category = e.GetElementOrNull("Category");
        LevelType = e.GetElementOrNull("LevelType");
        TimeLimit = e.GetIntElement("TimeLimit", 0);
        WinCondition = LessonName == TEMPLATE_LESSON_TYPE ? default : e.GetEnumElement<WinConditionEnum>("WinCondition");
        GoldReward = e.GetUIntElement("GoldReward", 10);
        ComboMode = e.GetEnumElementOrDefault<ComboModeEnum>("ComboMode");
        Difficulty = e.GetIntElement("Difficulty", 0);
        Priority = LessonName == TEMPLATE_LESSON_TYPE ? 0 : e.GetIntElement("Priority", 0);
        PromptStrings = e.GetElementOrNull("PromptStrings");

        CustomDeathMessage = e.GetElementOrNull("CustomDeathMessage")?.Split(',');
        IntroCutscene = e.GetElementOrNull("IntroCutscene");
        string[]? MessagePosition = e.GetElementOrNull("MessagePosition")?.Split(',');
        if (MessagePosition is not null)
        {
            MessagePosition_X = int.Parse(MessagePosition[0]);
            MessagePosition_Y = int.Parse(MessagePosition[1]);
        }
        NodePathLength = e.GetUIntElement("NodePathLength", 0);
        NodeSpread = e.GetUIntElement("NodeSpread", 0);

        Entities = e.DeserializeChildrenOfType<LessonEntityType>("Entity");
        Items = e.DeserializeChildrenOfType<LessonItem>("Item");
        Markers = e.DeserializeChildrenOfType<LessonMarker>("Marker");
        MessageTriggers = e.DeserializeChildrenOfType<LessonMessageTrigger>("MessageTrigger");
        WorldHotkeys = e.DeserializeChildrenOfType<LessonWorldHotkey>("WorldHotkey");
    }

    public void Serialize(XElement e)
    {
        throw new System.NotImplementedException();
    }
}
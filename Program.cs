using System.Xml;
using System.Xml.Linq;
using WallyMapSpinzor2;

string fromPath = args[0];
string toPath = args[1];

FileStream fromFile = new(fromPath, FileMode.Open, FileAccess.Read);
using StreamReader fsr = new(fromFile);
XDocument document = XDocument.Parse(MapUtils.FixBmg(fsr.ReadToEnd()));
XElement? element = document.FirstNode as XElement;
if(element is not null)
{
    LevelDesc levelDesc = element.DeserializeTo<LevelDesc>();
    FileStream toFile = new(toPath, FileMode.OpenOrCreate, FileAccess.Write);
    using XmlWriter xmlw = XmlWriter.Create(toFile, new(){OmitXmlDeclaration = true, IndentChars = "    ", Indent = true});
    levelDesc.Serialize().Save(xmlw);
}


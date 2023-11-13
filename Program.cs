using System.Text;
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
    FileStream toFile = new(toPath, FileMode.Create, FileAccess.Write);
    using XmlWriter xmlw = XmlWriter.Create(toFile, new(){
        OmitXmlDeclaration = true, //no xml header
        IndentChars = "    ", Indent = true, //ident with four spaces
        NewLineChars = "\n", //use UNIX line endings
        Encoding = new UTF8Encoding(false) //use UTF8 (no BOM) encoding
    });
    levelDesc.SerializeToXElement().Save(xmlw);
}


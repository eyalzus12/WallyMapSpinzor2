using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public static class Sample
{
    public static void DeserializeThenSerialize(string fromPath, string toPath)
    {
        //read. use MapUtils.FixBmg on the content to fix xml non-compliances on a few maps
        FileStream fromFile = new(fromPath, FileMode.Open, FileAccess.Read);
        using StreamReader fsr = new(fromFile);
        XDocument document = XDocument.Parse(MapUtils.FixBmg(fsr.ReadToEnd()));
        if (document.FirstNode is not XElement element) return;
        //write to file.
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
}
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public static class Sample
{
    public static void DeserializeThenSerialize(string ldFromPath, string ltFromPath, string ldDestPath, string ltDestPath)
    {
        //LevelDesc
        {
            //read. use MapUtils.FixBmg on the content to fix xml non-compliances on a few maps
            using FileStream fromFile = new(ldFromPath, FileMode.Open, FileAccess.Read);
            using StreamReader fsr = new(fromFile);
            XDocument document = XDocument.Parse(MapUtils.FixBmg(fsr.ReadToEnd()));
            if (document.FirstNode is not XElement element) return;
            //write to file.
            LevelDesc levelDesc = element.DeserializeTo<LevelDesc>();
            using FileStream toFile = new(ldDestPath, FileMode.Create, FileAccess.Write);
            using XmlWriter xmlw = XmlWriter.Create(toFile, new()
            {
                OmitXmlDeclaration = true, //no xml header
                IndentChars = "    ",
                Indent = true, //indent with four spaces
                NewLineChars = "\n", //use UNIX line endings
                Encoding = new UTF8Encoding(false) //use UTF8 (no BOM) encoding
            });
            levelDesc.SerializeToXElement().Save(xmlw);
        }

        //LevelTypes
        {
            using FileStream fromFile = new(ltFromPath, FileMode.Open, FileAccess.Read);
            using StreamReader fsr = new(fromFile);
            XDocument doc = XDocument.Parse(fsr.ReadToEnd());
            if (doc.FirstNode is not XElement element) return;

            LevelTypes levelTypes = element.DeserializeTo<LevelTypes>();
            using FileStream toFile = new(ltDestPath, FileMode.Create, FileAccess.Write);
            using XmlWriter xmlw = XmlWriter.Create(toFile, new()
            {
                OmitXmlDeclaration = true,
                IndentChars = "    ",
                Indent = true,
                NewLineChars = "\n",
                Encoding = new UTF8Encoding(false)
            });
            levelTypes.SerializeToXElement().Save(xmlw);
        }
    }
}

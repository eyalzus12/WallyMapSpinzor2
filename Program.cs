using System.Xml;
using System.Xml.Linq;
using WallyMapSpinzor2;

string path = args[0];

FileStream file = new(path, FileMode.Open, FileAccess.Read);
using(StreamReader sr = new(file))
{
    XDocument document = XDocument.Parse(sr.ReadToEnd());
    XElement? element = document.FirstNode as XElement;
    if(element is not null)
    {
        LevelDesc levelDesc = element.DeserializeTo<LevelDesc>();
        
    }
}


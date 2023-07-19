using System.Xml.Serialization;
using WallyMapSpinzor2;

string path = args[0];

XmlSerializer serializer = new(typeof(LevelDesc));

using(FileStream file = new(path, FileMode.Open, FileAccess.Read))
{
    LevelDesc? level = (LevelDesc?)serializer.Deserialize(file);
    
}


using System.IO;
using System.Xml.Serialization;

namespace AdvApp;

public class XmlDataManager
{
    public static List<T> LoadData<T>(string filePath) where T : new()
    {
        if (!File.Exists(filePath)) return new List<T>();

        XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
        using FileStream fileStream = new FileStream(filePath, FileMode.Open);

        return (List<T>)serializer.Deserialize(fileStream);
    }

    public static void SaveData<T>(string filePath, List<T> data)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
        using FileStream fileStream = new FileStream(filePath, FileMode.Create);

        serializer.Serialize(fileStream, data);
    }
}
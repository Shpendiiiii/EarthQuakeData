using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace EarthQuakeData;

public class DataConverter : IDataConverter
{
    public void Convert(dynamic data)
    {
        XElement xml = new XElement("root");

        // Populate the XML element with JSON data
        AddJsonToXml(xml, data);
        
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xml.ToString());
        doc.Save("test.xml");
        Console.WriteLine("Converted successfully");
    }
    
    
    static void AddJsonToXml(XElement parent, JToken json)
    {
        if (json is JObject)
        {
            foreach (var property in ((JObject)json).Properties())
            {
                XElement element = new XElement(property.Name);
                parent.Add(element);
                AddJsonToXml(element, property.Value);
            }
        }
        else if (json is JArray)
        {
            foreach (var item in ((JArray)json).Children())
            {
                XElement element = new XElement("item");
                parent.Add(element);
                AddJsonToXml(element, item);
            }
        }
        else
        {
            parent.Add(new XText(json.ToString()));
        }
    }
    
}
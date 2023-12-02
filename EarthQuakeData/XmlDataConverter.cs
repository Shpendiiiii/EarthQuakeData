using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using static EarthQuakeData.Utils;
namespace EarthQuakeData;

//Concrete implementor that converts data into XML
public class XmlDataConverter : IDataConverter
{
    //After calling the AddJsonToXml method, the xml variable is 
    //converted into an xml string which saved to a file
    //The UDPath is a static field in the Utils class which provides
    //the base output path to output to Outputs/Xml/
    //the that line uses a UNIX stamp to name the files uniquely 
    public void Convert(dynamic data)
    {
            XElement xml = new XElement("root");

            // Populate the XML string with JSON data
            AddJsonToXml(xml, data);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.ToString());

            string outputPath = UDPath + $"/Outputs/Xml/test{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}.xml";
            doc.Save(outputPath);
            Console.WriteLine("Converted JSON to XML successfully");
    }

    //A recursive method that converts the JSON structure into XML 
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
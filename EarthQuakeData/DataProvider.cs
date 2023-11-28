using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace EarthQuakeData;
using RestSharp; 
public abstract class DataProvider
{
    public IDataConverter DataConverter { get; set; }
    public abstract string Url { get; init; }
    protected RestClient HttpClient { get; init; }

    protected RestClient InitHttpClient()
    {
        RestClient client = new RestClient(Url);
        return client;
    }

    
    public abstract void GetMostRecentData();
    public abstract void GetDataByLocation(string longitude, string latitude);

    public abstract void GetDataByTimeRange(string startTime, string endTime);
    public abstract dynamic GetDataByOtherQualifiers(string inputData);

    public dynamic XmlConversion(dynamic data)
    {
        // JObject json = JObject.Parse(data);
        XElement xml = new XElement("root");

        // Populate the XML element with JSON data
        AddJsonToXml(xml, data);
        
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xml.ToString());
        doc.Save("test.xml");
        
        // Convert the XML element to string
        return xml.ToString();
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


// TODO: WriteToFile to JSON File method;
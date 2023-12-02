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
    
    public abstract JObject GetMostRecentData();
    public abstract JObject GetDataByLocation(string longitude, string latitude);
    public abstract dynamic GetDataByTimeRange(string startTime, string endTime);
    public abstract dynamic GetDataByOtherQualifiers(string inputData);
    public abstract void FormatConversion(dynamic data);
}

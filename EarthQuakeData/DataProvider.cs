using Newtonsoft.Json.Linq;
using RestSharp; 

namespace EarthQuakeData;

//Abstraction
public abstract class DataProvider
{
    //Reference to the implementor
    public IDataConverter DataConverter { get; set; }
    //Prop to hold the base url of the APIs
    public abstract string Url { get; init; }
    //HTTP Client to make requests with
    protected RestClient HttpClient { get; init; }
    public abstract JObject GetMostRecentData();
    public abstract JObject GetDataByLocation(string longitude, string latitude);
    public abstract dynamic GetDataByTimeRange(string startTime, string endTime);
    //Qualifiers are unique to the specific APIs
    public abstract dynamic GetDataByOtherQualifiers(string inputData);
    //This will use the object of IDataConverter to make converision
    public abstract void FormatConversion(dynamic data);
}

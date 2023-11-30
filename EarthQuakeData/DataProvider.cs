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

    //TODO Apply Dependecy Inversion
    protected RestClient InitHttpClient()
    {
        RestClient client = new RestClient(Url);
        return client;
    }

    
    public abstract void GetMostRecentData();
    public abstract void GetDataByLocation(string longitude, string latitude);

    public abstract void GetDataByTimeRange(string startTime, string endTime);
    public abstract dynamic GetDataByOtherQualifiers(string inputData);
    public abstract void XmlConversion(dynamic data);


    public abstract void YmlConversion(dynamic data);
    // public abstract void YmlConversion(string data);
}


// TODO: WriteToFile to JSON File method;

//TODO change the signature of the methods;
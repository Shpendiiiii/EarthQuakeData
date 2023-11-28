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
    public abstract void GetDataByOtherQualifiers(string inputData);
}


// TODO: WriteToFile to JSON File method;
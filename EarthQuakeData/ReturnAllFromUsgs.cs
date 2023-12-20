using Newtonsoft.Json.Linq;
using RestSharp;

namespace EarthQuakeData;

public class ReturnAllFromUsgs : ICommand
{
    protected RestClient httpClient;
    protected dynamic dataConverter;
    
    public ReturnAllFromUsgs(RestClient _httpClient, dynamic _dataConverter)
    {
        httpClient = _httpClient;
        dataConverter = _dataConverter;
    }

    public void Execute()
    {
        ReturnAll();
    }

    public JObject ReturnAll()
    {
        DataProvider firstUsgs = new UsgsApi(dataConverter, httpClient);
        JObject info = firstUsgs.GetMostRecentData();
        firstUsgs.FormatConversion(info);
        return info;
    }
}